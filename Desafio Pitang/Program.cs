using Microsoft.AspNetCore;
using System.Reflection;
using log4net;
using log4net.Config;
using DesafioPitang.Utils.Messages;

namespace DesafioPitang.WebApi
{
    public static class Program
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(Program));

        public static void Main(string[] args)
        {
            try
            {
                var logRepository = LogManager.GetRepository(Assembly.GetCallingAssembly());
                var configFile = new FileInfo("log4net.config");

                XmlConfigurator.Configure(logRepository, configFile);

                _log.Info(InfraMessages.ApplicationStartup);

                var webHost = WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();

                webHost.Build().Run();
            }
            catch (Exception ex)
            {
                _log.Fatal(InfraMessages.FatalError, ex);
                throw;
            }
        }
    }
}
