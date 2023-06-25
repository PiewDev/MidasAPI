using Autofac;
using Midas.Net.Database.Log;
using Midas.Net.Database.Sales;
using Midas.Net.Domain.Log;
using Midas.Net.Domain.Sales;
using Midas.Net.Service.Sales;
using Midas.Net.Services;
using Midas.Net.Domain;
namespace Midas.Net.Sales
{
    public class SaleAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SaleService>().As<ISaleService>().SingleInstance();
            builder.RegisterType<SaleRepository>().As<ISaleRepository>().SingleInstance();
        }
    }
}
