using AutoMapper;
using Midas.Net.Domain.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midas.Net.Database.Log
{
    public class DbLogMappingProfile : Profile
    {
        public DbLogMappingProfile()
        {
            CreateMap<HttpToLog, DbLog>()
            .ForMember(dest => dest.Guid, opt => opt.MapFrom(src => src.Guid ?? Guid.NewGuid().ToString()))
            .ForMember(dest => dest.LogText, opt => opt.MapFrom(src => src.ToString()))
            .ForMember(dest => dest.Datetime, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.Origin, opt => opt.MapFrom(src => src.Origin ?? "default"))
            .ForMember(dest => dest.Body, opt => opt.MapFrom(src => src.Body))
            .ForMember(dest => dest.Header, opt => opt.MapFrom(src => src.Header))
            .ForMember(dest => dest.StatusCode, opt => opt.MapFrom(src => src.StatusCode.ToString()))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Uri, opt => opt.MapFrom(src => src.Uri))
            .ForMember(dest => dest.UserOrigin, opt => opt.MapFrom(src => src.UserOrigin));
        }
    }
}
