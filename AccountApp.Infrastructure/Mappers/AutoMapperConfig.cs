using AccountApp.Core.Domain;
using AccountApp.Infrastructure.Dto;
using AutoMapper;

namespace AccountApp.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
                { 
                    cfg.CreateMap<Account, AccountDto>();
                })
                .CreateMapper();
    }
}