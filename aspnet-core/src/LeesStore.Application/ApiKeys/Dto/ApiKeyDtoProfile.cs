using AutoMapper;
using LeesStore.Authorization.ApiKeys;

namespace LeesStore.ApiKeys.Dto
{
    public class ApiKeyDtoProfile : Profile
    {
        public ApiKeyDtoProfile()
        {
            CreateMap<ApiKey, ApiKeyDto>()
                .ForMember(i => i.ApiKey, opt => opt.MapFrom(i => i.UserName));
        }
    }
}