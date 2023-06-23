using AutoMapper;
using Midas.Net.Crud;
using Midas.Net.Database.Log;
using Midas.Net.Database.Products;
using Midas.Net.Database.ProductTypes;
using Midas.Net.Database.Sales;
using Midas.Net.Sales;

namespace Midas.Net.Mapper
{
    public static class AutoMapperConfiguration
    {
        public static IMapper Configure()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.AddProfile<DbLogMappingProfile>();
                config.AddProfile<DbProductMapProfile>();
                config.AddProfile<DbProductTypeMapProfile>();
                config.AddProfile<DbSaleMapProfile>();
                config.AddProfile<SaleMapProfile>(); 

            });

            return mappingConfig.CreateMapper();
        }
    }
}