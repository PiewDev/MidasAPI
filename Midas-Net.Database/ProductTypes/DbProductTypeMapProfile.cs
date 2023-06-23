using AutoMapper;
using Midas.Net.Database.Sales;
using Midas.Net.Domain.Products.DTO;
using Midas.Net.Domain.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midas.Net.Database.ProductTypes
{
    public class DbProductTypeMapProfile : Profile
    {
        public DbProductTypeMapProfile()
        {
            CreateMap<DbProductType, ProductType>();

            CreateMap<ProductType, DbProductType>();
        }
    }
}
