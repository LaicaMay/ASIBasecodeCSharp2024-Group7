using ExpenseTracker.Data.Models;
using ExpenseTracker.Data.Models.CustomModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using ExpenseTracker.Resources.Constants;
using System.Text.RegularExpressions;
using ExpenseTracker.Data.Utils;
using System.Net.Mail;
using System.Net;

namespace ExpenseTrackerWeb.Controllers
{
    public class AccountController : BaseController
    {
        IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AccountController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Login(string ReturnUrl)
        {
            var existUser = _userManager.GetUserById(UserId);

            if (User.Identity.IsAuthenticated)
            {
                if (existUser.isVerify == false)
                {
                    return RedirectToAction("Verify");
                }

                return RedirectToAction("Overview", "Expense");
            }

            ViewBag.Error = string.Empty;
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(string username, string password, string ReturnUrl)
        {
            if (_userManager.SignIn(username, password, ref ErrorMessage) == ErrorCode.Success)
            {
                var user = _userManager.GetUserByUsername(username);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Username),
                    new Claim(ClaimsIdentity.DefaultNameClaimType, Convert.ToString(user.UserId)),
                    new Claim("isVerify", user.isVerify.ToString()) // Add isVerify claim
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var properties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    IsPersistent = true,
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), properties);

                // Redirect to Verify if not verified
                if (user.isVerify == false)
                {
                    return RedirectToAction("Verify");
                }

                // Handle ReturnUrl
                if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }

                return RedirectToAction("Overview", "Expense");
            }

            ViewBag.Error = ErrorMessage;
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public IActionResult SignUp()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Overview", "Expense");

            return View();
                
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult SignUp(User u)
        {
            var allowedEmailDomains = new[] { "gmail.com", "yahoo.com", "ymail.com" };

            if (_userManager.GetUserByEmail(u.Email) != null)
            {
                ModelState.AddModelError("Email", "Email is already taken.");
            }

            var emailDomain = u.Email.Split('@').Last();
            if (!allowedEmailDomains.Contains(emailDomain))
            {
                ModelState.AddModelError("Email", "Please enter a valid email.");
            }

            if (_userManager.GetUserByUsername(u.Username) != null)
            {
                ModelState.AddModelError("Username", "Username is already taken.");
            }

            if (!Regex.IsMatch(u.Username, @"^[A-Za-z][A-Za-z0-9]{2,}$"))
            {
                ModelState.AddModelError("Username", "Please enter a valid username");
            }

            if (u.Password != u.ConfirmPassword)
            {
                ModelState.AddModelError("Password", "Password does not match.");
            }

            if (!Regex.IsMatch(u.Password, @"^(?=.*[A-Z])(?=.*\W).{8,}$"))
            {
                ModelState.AddModelError("Password", "Please enter a valid password.");         
            }

            if (!ModelState.IsValid)
            {
                return View(u);
            }

            u.Code = Utilities.code.ToString();

            if (_userManager.SignUp(u, ref ErrorMessage) == ErrorCode.Success)
            {                  
                Balance balance = new Balance { UserId = u.UserId };
                if (_balanceMgr.DefaultBalance(balance, ref ErrorMessage) == ErrorCode.Success)
                {                 
                    if (u.Code != null)
                    {
                        List<Claim> claims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.NameIdentifier, u.Username),
                            new Claim(ClaimsIdentity.DefaultNameClaimType, Convert.ToString(u.UserId))
                        };

                        ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        AuthenticationProperties properties = new AuthenticationProperties()
                        {
                            AllowRefresh = true,
                            IsPersistent = true,
                        };

                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), properties);

                        var sendersEmail = _configuration["EmailSettings:SendersEmail"];
                        var sendersPassword = _configuration["EmailSettings:SendersPassword"];
                        var noreplyEmail = "no-reply@ecofridge.com";
                        var subject = "Verification Code";

                        var body = $@"
                            <div style='font-family: Arial, sans-serif; padding: 20px; background-color: #f4f4f4;'>
                                <div style='max-width: 600px; margin: 0 auto; background-color: white; padding: 20px; border-radius: 10px; box-shadow: 0 2px 5px rgba(0,0,0,0.1);'>
                                    <h2 style='color: #333;'>Verification Code</h2>
                                    <p>Hello,</p>
                                    <p>Your Verification is:</p>
                                    <p style='font-size: 18px; font-weight: bold; color: #307a59;'>{u.Code}</p>
                                    <p>You can change it in your profile settings once you log in.</p>
                                    <hr style='border: none; border-top: 1px solid #eee; margin: 20px 0;' />
                                    <p>If you didn't request this, please ignore this email or contact support.</p>
                                    <p>Thank you,</p>
                                    <p><strong>Team Snackers</strong></p>
                                </div>
                            </div>";

                        using (MailMessage message = new MailMessage())
                        {
                            message.From = new MailAddress(noreplyEmail);
                            message.To.Add(u.Email);
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
                    }
                    return RedirectToAction("Verify");

                } else
                {
                    ModelState.AddModelError(String.Empty, ErrorMessage);
                    return View(u);
                }

            } else
            {
                ModelState.AddModelError(String.Empty, ErrorMessage);
                return View(u);
            }

            TempData["Username"] = u.Username;
            return RedirectToAction("Login");
        }

        public IActionResult Verify()
        {                                
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Verify(VerifyViewModel code)
        {

            var existUser = _userManager.GetUserById(UserId);

            if (existUser == null)
            {
                ModelState.AddModelError(String.Empty, "User does not exist.");
            }

            if (existUser.Code != code.ConfirmCode)
            {
                ModelState.AddModelError("ConfirmCode", "Please enter your valid code.");
            }

            if (!ModelState.IsValid)
            {
                return View(code);
            }

            if (existUser.Code == code.ConfirmCode)
            {
                existUser.isVerify = true;
                if (_userManager.UpdateUser(existUser, ref ErrorMessage) == ErrorCode.Success)
                {
                    return RedirectToAction("Overview", "Expense");
                }
            }

            return View();
        }

        [Authorize]
        public IActionResult Update()
        {
            return View();
        }

        public IActionResult UpdateUserInfo()
        {
            return View();
        }

    }
}
