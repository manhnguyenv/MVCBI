using log4net;

namespace VASJ.BI.Helpers
{
    public class MyLogger
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MyLogger));

        public static ILog Log
        {
            get { return MyLogger.log; }
        }
    }
}