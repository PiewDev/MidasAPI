
using log4net;
using log4net.Config;


namespace Midas.Net.Log
{
    public static class Log4NetConfiguration
    {
        public static void Configure()
        {
            var loggerRepository = LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(loggerRepository, new System.IO.FileInfo("log4net.config"));
        }
    }
}
