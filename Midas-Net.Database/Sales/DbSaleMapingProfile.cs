using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Midas.Net.Database.SaleDetails;
using Midas.Net.Domain.Sales;

namespace Midas.Net.Database.Sales
{
    public class DbSaleMapProfile : Profile
    {
        public DbSaleMapProfile()
        {
            CreateMap<Sale, DbSale>()
                .ForMember(dest => dest.SaleId, opt => opt.Ignore())
                .ForMember(dest => dest.SaleDetails, opt => opt.MapFrom(src => src.SaleDetails));
        }
    }

}
