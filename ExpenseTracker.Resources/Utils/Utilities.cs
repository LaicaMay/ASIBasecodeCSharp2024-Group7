namespace ExpenseTracker.Resources.Utils
{
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
