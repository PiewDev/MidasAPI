using AutoMapper;
using Midas.Net.Domain.Sales;

namespace Midas.Net.Sales
{
    public class SaleMapProfile: Profile
    {
        public SaleMapProfile()
        {
            CreateMap<CreateSaleRequest, Sale>()
            .ForMember(dest => dest.Date, opt => opt.Ignore())
            .ForMember(dest => dest.SaleDetails, opt => opt.MapFrom(src => src.SaleDetails));

            CreateMap<CreateSaleDetail, SaleDetail>();
        }
    }
}
