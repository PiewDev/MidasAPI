using Autofac;
using AutoMapper;

namespace Midas.Net.Mapper
{
    public class AutoMapperAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context =>
            {
                return AutoMapperConfiguration.Configure();
            }).As<IMapper>().InstancePerLifetimeScope();
        }
    }
}
