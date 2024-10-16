using ExpenseTracker.Data.Models;
using ExpenseTracker.Resources.Constants;
using ExpenseTracker.Data.Utils;

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

        public ErrorCode SignUp(User u, ref String errMsg)
        {
            u.Code = Utilities.code.ToString();
            u.CreatedDate = DateTime.Now;
            u.Status = (Int32)Status.InActive;
            u.Agree = true;

            if(GetUserByUsername(u.Username) != null)
            {
                errMsg = "Username already exist";
                return ErrorCode.Error;
            }

            if(GetUserByEmail(u.Email) != null)
            {
                errMsg = "Email already exist!";
                return ErrorCode.Error;
            }

            if(_userRepo.Create(u, out errMsg) != ErrorCode.Success)
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
