using AutoMapper;
using CryptoManager.Domain.DTOs;
using CryptoManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoManager.Domain.Mapper
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
            : this("MyProfile")
        {
        }

        protected AutoMapperProfileConfiguration(string profileName)
        : base(profileName)
        {
            CreateMap<Exchange, ExchangeDTO>();
            CreateMap<ExchangeDTO, Exchange>();
            //CreateMap<ProductViewModel, SimpleKeyValueViewModel>().ForMember(dest => dest.Name,
            //                                                                    opt => opt.MapFrom(src => src.UnitofMeasureName))
            //                                                                    .ForMember(dest => dest.Id,
            //                                                                    opt => opt.MapFrom(src => src.UnitofMeasureId));
            //CreateMap<ProductUnitofMeasureConversion, SimpleKeyValueViewModel>().ForMember(dest => dest.Name,
            //                                                                    opt => opt.MapFrom(src => src.UnitofMeasure.Name))
            //                                                                    .ForMember(dest => dest.Id,
            //                                                                    opt => opt.MapFrom(src => src.UnitofMeasureId));
        }
    }
}
