using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Resources.Constants;
using ExpenseTracker.Data.Models.CustomModels;
using ExpenseTracker.Data.Utils;
using System.Net.Mail;
using System.Net;
using ExpenseTracker.Data.Models;
using ExpenseTracker.Services.Controllers;

namespace ExpenseTrackerWeb.Controllers
{
    public class MailController : BaseController
    {
        IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public MailController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult ForgotPassword([FromBody] ForgotPasswordModel model)
        {
            var user = _db.Users.Where(m => m.Email == model.Email).FirstOrDefault();

            if (user == null || user.isVerify == false || user.isVerify == null)
            {
                return Json(new { success = false, message = "Email not found or user is not Verified." });
            }

            var activeToken = _userManager.GetActiveTokenByUserId(user.UserId);

            if (activeToken != null)
            {
                activeToken.IsActive = false;
                _userManager.UpdateUserToken(activeToken, ref ErrorMessage);
            }     

            try
            {
                var sendersEmail = _configuration["EmailSettings:SendersEmail"];
                var sendersPassword = _configuration["EmailSettings:SendersPassword"];
                var noreplyEmail = "no-reply@expensetracker.com";
                var subject = "Forgot Password";

                string token = Guid.NewGuid().ToString("N");

                var userToken = new PasswordResetToken()
                {
                    UserId = user.UserId,
                    Token = token,
                    ExpiryDate = DateTime.UtcNow.AddHours(1),
                    IsActive = true
                };          
                        
                if (_userPasswordToken.Create(userToken, out ErrorMessage) != ErrorCode.Success)
                {
                    ModelState.AddModelError(String.Empty, ErrorMessage);
                    return BadRequest(new { message = "Failed to add Token.", errors = ModelState });
                }
            
                var changePasswordUrl = Url.Action("ResetPassword", "Account", new { token }, Request.Scheme);

                var body = $@"
                            <div style='font-family: Arial, sans-serif; padding: 20px; background-color: #f4f4f4;'>
                                <div style='max-width: 600px; margin: 0 auto; background-color: white; padding: 20px; border-radius: 10px; box-shadow: 0 2px 5px rgba(0,0,0,0.1);'>
                                    <h2 style='color: #333;'>Password Reset Request</h2>
                                    <p>Hello {user.Username},</p>
                                    <p>Click the button below to reset your password:</p>
                                    <a href='{changePasswordUrl}' style='background-color: #68BB69; border: 2px solid white; padding: 10px 20px; color: white; text-decoration: none; border-radius: 5px; font-size: 16px; display: inline-block;'>
                                        Change your password
                                    </a>
                                    <p>This link will expire in 1 hour.</p>
                                    <p>If you didn't request this, please ignore this email or contact support.</p>
                                    <p>Thank you,</p>
                                    <p><strong>Team Alliance Group7</strong></p>
                                </div>
                            </div>";

                if (token != null)
                {
                    using (MailMessage message = new MailMessage())
                    {
                        message.From = new MailAddress(noreplyEmail);
                        message.To.Add(user.Email);
                        message.Subject = subject;
                        message.Body = body;
                        message.IsBodyHtml = true;

                        using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                        {
                            smtp.Credentials = new NetworkCredential(sendersEmail, sendersPassword);
                            smtp.EnableSsl = true;
                            smtp.Send(message);
                        }
                    }         
                    return Json(new { success = true, message = "Email sent please check your inbox." });
                }
           
                return Json(new { success = false, message = "Error updating password." });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "An error occurred while processing your request." });
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult ResendVerification([FromBody] ResendNewCodeModel resend)
        {
            var user = _db.Users.Where(m => m.Email == resend.Email).FirstOrDefault();

            if (user == null)
            {
                return Json(new { success = false, message = "Email does exist." });
            }

            user.ExpiryCodeDate = DateTime.UtcNow.AddHours(1);
            user.Code = Utilities.code.ToString();
            user.Status = true;
            if (_userManager.UpdateUser(user, ref ErrorMessage) != ErrorCode.Success)
            {
                ModelState.AddModelError(String.Empty, ErrorMessage);
                return BadRequest(new { message = "Failed to update code.", errors = ModelState });
            }
            try
            {
                var sendersEmail = _configuration["EmailSettings:SendersEmail"];
                var sendersPassword = _configuration["EmailSettings:SendersPassword"];
                var noreplyEmail = "no-reply@expensetracker.com";
                var subject = "Resend Code";

                var body = $@"
                            <div style='font-family: Arial, sans-serif; padding: 20px; background-color: #f4f4f4;'>
                                <div style='max-width: 600px; margin: 0 auto; background-color: white; padding: 20px; border-radius: 10px; box-shadow: 0 2px 5px rgba(0,0,0,0.1);'>
                                    <h2 style='color: #333;'>New Code Request</h2>
                                    <p>Hello {user.Username},</p>
                                    <p>This is your new code: {user.Code}</p>                             
                                    <p>This code will expire in 1 hour.</p>
                                    <p>If you didn't request this, please ignore this email or contact support.</p>
                                    <p>Thank you,</p>
                                    <p><strong>Team Alliance Group7</strong></p>
                                </div>
                            </div>";

                if (user.Code != null)
                {
                    using (MailMessage message = new MailMessage())
                    {
                        message.From = new MailAddress(noreplyEmail);
                        message.To.Add(user.Email);
                        message.Subject = subject;
                        message.Body = body;
                        message.IsBodyHtml = true;

                        using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                        {
                            smtp.Credentials = new NetworkCredential(sendersEmail, sendersPassword);
                            smtp.EnableSsl = true;
                            smtp.Send(message);
                        }
                    }
                    return Json(new { success = true, message = "New code has sent to your email." });
                }

                return Json(new { success = false, message = "Error updating code." });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "An error occurred while processing your request." });
            }
        }
    }
}
