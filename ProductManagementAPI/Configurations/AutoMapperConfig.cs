using AutoMapper;
using ProductManagementAPI.DTO;
using ProductManagementAPI.Models;

namespace ProductManagementAPI.Configurations
{
    public class AutoMapperConfig : Profile
    {
        // SignupDTO = ApplicationUser
        public AutoMapperConfig()
        {
            CreateMap<ApplicationUser, SignUpDTO>().ReverseMap()
            .ForMember(f => f.UserName, t2 => t2.MapFrom(src => src.Email));
        }
    }
}
