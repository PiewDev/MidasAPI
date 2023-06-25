using Autofac;
using Midas.Net.Crud;
using Midas.Net.Log;
using Midas.Net.Mapper;
using Midas.Net.Product;
using Midas.Net.Sales;
using System.Configuration;

namespace Midas.Net.Autofac
{
    public class AutofacModuleRegister : Module
    {

        public AutofacModuleRegister()
        {
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new LogAutofacModule());
            builder.RegisterModule(new CrudAutofacModule());
            builder.RegisterModule(new SaleAutofacModule());
            builder.RegisterModule(new ProductAutofacModule());
        }
    }
}
