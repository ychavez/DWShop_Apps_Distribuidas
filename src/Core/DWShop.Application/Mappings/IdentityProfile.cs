using AutoMapper;
using DWShop.Application.Features.Identity.Commands.Register;
using DWShop.Application.Responses.Identity;
using DWShop.Domain.Entities;

namespace DWShop.Application.Mappings
{
    public class IdentityProfile : Profile
    {
        public IdentityProfile()
        {
            CreateMap<DWUser, LoginResponse>()
                .ForMember(dest => dest.UserName, user => user.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Gafette, user => user.MapFrom(src => src.Gafette));

            CreateMap<RegisterUserCommand, DWUser>().ReverseMap();
        }
    }
}
