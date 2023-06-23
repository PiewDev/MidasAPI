using Autofac;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Midas.Net.Database.Crud;
using Midas.Net.Domain;
using Midas.Net.Log;
using Midas.Net.Service;
using System.Security.Cryptography;

namespace Midas.Net.Crud
{
    public class CrudAutofacModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(CrudService<,>))
                .As(typeof(ICrudService<,>))
                .InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(CrudRepository<,>))
               .As(typeof(IRepository<,>))
               .InstancePerLifetimeScope();
            builder.RegisterType<CrudSupportFilter>().As<IActionFilter>();
        }
    }
}
