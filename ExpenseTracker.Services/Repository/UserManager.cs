using ExpenseTracker.Data.Models;
using ExpenseTracker.Resources.Constants;
using Microsoft.AspNetCore.Identity;

namespace ExpenseTracker.Services.Repository
{
    public class UserManager
    {
        private readonly BaseRepository<User> _userRepo;
        private readonly BaseRepository<UserInformation> _userInfo;
        private readonly BaseRepository<PasswordResetToken> _passwordResetTokenRepo;
        public UserManager()
        {
            _userRepo = new BaseRepository<User>();
            _userInfo = new BaseRepository<UserInformation>();
            _passwordResetTokenRepo = new BaseRepository<PasswordResetToken>();
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

        public ErrorCode SignIn(string username, string password, ref string errMsg)
        {
            var userSignIn = GetUserByUsername(username);
            if (userSignIn == null)
            {
                errMsg = "Invalid username or password.";
                return ErrorCode.Error;
            }

            // Hash the entered password and compare it with the stored hashed password
            var passwordHasher = new PasswordHasher<User>();  // Assuming User is your user class
            var result = passwordHasher.VerifyHashedPassword(userSignIn, userSignIn.Password, password);

            if (result == PasswordVerificationResult.Failed)
            {
                errMsg = "Invalid username or password.";
                return ErrorCode.Error;
            }

            errMsg = "Login Successful";
            return ErrorCode.Success;
        }

        //public ErrorCode SignIn(String username, String password, ref String errMsg)
        //{
        //    var userSignIn = GetUserByUsername(username);
        //    if (userSignIn == null || !userSignIn.Password.Equals(password))
        //    {
        //        errMsg = "Invalid username or password.";
        //        return ErrorCode.Error;
        //    }

        //    errMsg = "Login Successful";
        //    return ErrorCode.Success;
        //}

        public ErrorCode SignUp(User u, ref string errMsg)
        {
            u.CreatedDate = DateTime.Now;
            u.isVerify = false;

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

        public User GetUserByGuidPassword(String password)
        {
            return _userRepo._table.Where(m => m.Password == password).FirstOrDefault();
        }

        public PasswordResetToken GetTokenByUserId (int userId)
        {
            return _passwordResetTokenRepo.Get(userId);
        }

        public PasswordResetToken GetActiveTokenByUserId(int? activUserId)
        {
            return _passwordResetTokenRepo._table
                   .FirstOrDefault(b => b.UserId == activUserId && b.IsActive == true);
        }

        public ErrorCode UpdateUserToken(PasswordResetToken pass, ref String errMsg)
        {
            return _passwordResetTokenRepo.Update(pass.Id, pass, out errMsg);
        }


    }
}
