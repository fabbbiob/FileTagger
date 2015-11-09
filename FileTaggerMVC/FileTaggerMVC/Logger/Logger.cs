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
                log4net.Config.XmlConfigurator.Configure();
                logger = LogManager.GetLogger(typeof(Logger));
            }

            return logger;
        }
    }
}