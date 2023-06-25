using Autofac;
using Midas.Net.Database.Products;
using Midas.Net.Domain.Products;

namespace Midas.Net.Product
{
    public class ProductAutofacModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductRepository>().As<IProductRepository>();
        }
    }
}
