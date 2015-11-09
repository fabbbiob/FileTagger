using log4net;

namespace FileTaggerMVC.Logger
{
    internal class Logger
    {
        private static ILog logger;

        public static ILog Get()
        {
            if (logger == null)
            {
                logger = LogManager.GetLogger(typeof(Logger));
            }

            return logger;
        }
    }
}