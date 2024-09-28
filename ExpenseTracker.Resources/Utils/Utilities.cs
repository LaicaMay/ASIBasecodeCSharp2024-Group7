namespace ExpenseTracker.Resources.Utils
{
    public enum ErrorCode
    {
        Success,
        Error
    }
    public enum Status
    {
        InActive,
        Active
    }

    public class Constant
    {
        public const int ERROR = 1;
        public const int SUCCESS = 0;
    }
    public class Utilities
    {
        public static int code
        {
            get
            {
                Random r = new Random();
                return r.Next(100000, 999999);
            }
        }
    }
}
