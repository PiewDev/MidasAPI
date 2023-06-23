using Autofac;
using AutoMapper;
using Midas.Net.Database.Log;
using Midas.Net.Domain.Log;
using Midas.Net.Services;

namespace Midas.Net.Log
{
    public class LogAutofacModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LogService>().As<ILogService>().SingleInstance();
            builder.RegisterType<LogRepository>().As<ILogRepository>().SingleInstance();
        }
    }
}
