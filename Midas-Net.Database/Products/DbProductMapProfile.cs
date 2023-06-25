using AutoMapper;
using Midas.Net.Database.Sales;
using Midas.Net.Domain.Products;
using Midas.Net.Domain.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midas.Net.Database.Products
{
    public class DbProductMapProfile : Profile
    {
        public DbProductMapProfile()
        {
            CreateMap<DbProduct, Product>();

            CreateMap<Product, DbProduct>();
        }
    }
}
