using AutoMapper;
using Midas.Net.Database.SaleDetails;
using Midas.Net.Domain.Sales;

namespace Midas.Net.Database.Sales
{
    public class DbSaleMapProfile : Profile
    {
        public DbSaleMapProfile()
        {

            CreateMap<DbSale, Sale>()
               .ForMember(dest => dest.SaleId, opt => opt.MapFrom(src => src.SaleId))
               .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
               .ForMember(dest => dest.IsCancelled, opt => opt.MapFrom(src => src.IsCancelled))
               .ForMember(dest => dest.SaleDetails, opt => opt.MapFrom(src => src.SaleDetails))
               .ReverseMap();

            CreateMap<SaleDetail, DbSaleDetail>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
                .ReverseMap();
        }
    }

}
