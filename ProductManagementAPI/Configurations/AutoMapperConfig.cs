using AutoMapper;
using ProductManagementAPI.DTO;
using ProductManagementAPI.Model;
using ProductManagementAPI.Models;

namespace ProductManagementAPI.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig() //program.cs
        {
            CreateMap<ApplicationUser, SignUpDTO>().ReverseMap()
            .ForMember(f => f.UserName, t2 => t2.MapFrom(src => src.Email)); // maps the "UserName" property of the destination object to the "Email" property of the source object
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}
