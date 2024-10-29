using ExpenseTracker.Data.Models;
using ExpenseTracker.Resources.Constants;
using ExpenseTracker.Data.Utils;
using System.Text.RegularExpressions;

namespace ExpenseTracker.Data.Repository
{
    public class UserManager
    {
        private readonly BaseRepository<User> _userRepo;
        private readonly BaseRepository<UserInformation> _userInfo;

        public UserManager()
        {
            _userRepo = new BaseRepository<User>();
            _userInfo = new BaseRepository<UserInformation>();
        }

        #region Get User By -
        public User GetUserById(int userId)
        {
            return _userRepo.Get(userId);
        }

        public User GetUserByUsername(String username)
        {
            return _userRepo._table.Where(m => m.Username == username).FirstOrDefault();
        }

        public User GetUserByEmail(String email)
        {
            return _userRepo._table.Where(m => m.Email == email).FirstOrDefault();
        }
        #endregion

        public ErrorCode SignIn(String username, String password, ref String errMsg)
        {
            var userSignIn = GetUserByUsername(username);
            if (userSignIn == null || !userSignIn.Password.Equals(password))
            {
                errMsg = "Invalid username or password.";
                return ErrorCode.Error;
            }

            errMsg = "Login Successful";
            return ErrorCode.Success;
        }

        public ErrorCode SignUp(User u, ref string errMsg)
        {
            var allowedEmailDomains = new[] { "gmail.com", "yahoo.com", "ymail.com" };
            var errorMessages = new List<string>();  // List to hold all error messages

            u.Code = Utilities.code.ToString();
            u.CreatedDate = DateTime.Now;
            u.Status = (int)Status.InActive;
            u.Agree = true;

            // Username validation
            if (GetUserByUsername(u.Username) != null)
            {
                errorMessages.Add("•Username already exists.");
            }
            if (!Regex.IsMatch(u.Username, @"^[A-Za-z][A-Za-z0-9]{2,}$"))
            {
                errorMessages.Add("•Username must start with a letter and be at least 3 characters long.");
            }

            // Email validation
            if (GetUserByEmail(u.Email) != null)
            {
                errorMessages.Add("•Email already exists!");
            }
            var emailDomain = u.Email.Split('@').Last();
            if (!allowedEmailDomains.Contains(emailDomain))
            {
                errorMessages.Add("•Please enter a valid email.");
            }

            // Password validation
            if (u.Password != u.ConfirmPassword)
            {
                errorMessages.Add("•Passwords do not match.");
            }
            if (!Regex.IsMatch(u.Password, @"^(?=.*[A-Z])(?=.*\W).{8,}$"))
            {
                errorMessages.Add("•Password must be at least 8 characters long, contain at least one uppercase letter, and one special character.");
            }

            // Check if there were any errors and return them
            if (errorMessages.Any())
            {
                errMsg = string.Join("\n", errorMessages);  // Join errors with line breaks
                return ErrorCode.Error;
            }

            // Create User if no errors
            if (_userRepo.Create(u, out errMsg) != ErrorCode.Success)
            {
                return ErrorCode.Error;
            }

            return ErrorCode.Success;
        }


        public ErrorCode UpdateUser(User u, ref String errMsg)
        {
            return _userRepo.Update(u.UserId, u, out errMsg);
        }

        public ErrorCode UpdateUserInformation(UserInformation u, ref String errMsg)
        {
            return _userInfo.Update(u.UserId, u, out errMsg);
        }

        public UserInformation GetUserInfoById(int id)
        {
            return _userInfo.Get(id);
        }
    }
}
