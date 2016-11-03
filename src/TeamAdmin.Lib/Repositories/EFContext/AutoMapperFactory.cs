using System;
using AutoMapper;
using TeamAdmin.Core;

namespace TeamAdmin.Lib.Repositories.EFContext
{
    internal static class AutoMapperFactory
    {
        private static IMapper mapper;

        static AutoMapperFactory()
        {
            mapper = AutoMapperConfiguration.CreateMapper();
        }

        private static MapperConfiguration AutoMapperConfiguration{
            get
            {
                return new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Core.Club, EFContext.Club>()
                        .ForMember(d => d.ClubId, opt => opt.MapFrom(src => src.ClubId))
                        .ForMember(d => d.City, opt => opt.MapFrom(src => src.Address.City))
                        .ForMember(d => d.Country, opt => opt.MapFrom(src => src.Address.Country))
                        .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Name))
                        .ForMember(d => d.Province, opt => opt.MapFrom(src => src.Address.Province))
                        .ForMember(d => d.Street, opt => opt.MapFrom(src => src.Address.Street))
                        .ForMember(d => d.PostalCode, opt => opt.MapFrom(src => src.Address.PostalCode));

                    cfg.CreateMap<Core.Team, EFContext.Team>();                        
                });
            }
        }

        internal static IMapper GetMapper()
        {
            return mapper;
        }
    }
}