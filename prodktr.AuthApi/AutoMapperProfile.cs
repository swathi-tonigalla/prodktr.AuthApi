using AutoMapper;
using prodktr.AuthApi.Models;

namespace prodktr.AuthApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserResponseDto>()
                .ForMember(dest=>dest.userName,opt=>opt.MapFrom(src=>src.name))
                .ForMember(dest=>dest.email_id,opt=>opt.MapFrom(src=>src.email))
                .ForMember(dest=>dest.password,opt=>opt.MapFrom(src=>src.password))
                .ForMember(dest=>dest.password_confirmation,opt=>opt.MapFrom(src=>src.primary_role))
                .ForMember(dest=>dest.createdAt,opt=>opt.MapFrom(src=>src.created_at))
                .ForMember(dest=>dest.updatedat,opt=>opt.MapFrom(src=>src.updated_at))
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.unique_id));
        }
    }
}
