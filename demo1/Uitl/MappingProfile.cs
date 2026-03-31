using AutoMapper;
using demo1.Controllers;
using demo1.Mode;
using DTO.UserControllerDTO;

namespace demo1.Uitl
{
    /// <summary>
    /// 用来省略同名赋值的
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // 将 UserDto 映射到 Users
            CreateMap<UserDtoAdd, Users>();

            // 如果需要反向映射（实体→DTO）
            // CreateMap<Users, UserDto>(); 
            // 如果属性名不一致，可以手动配置,交叉
            // CreateMap<UserDto, Users>()
            //     .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name))
            //     .ForMember(dest => dest.Id, opt => opt.Ignore()); // 忽略自增ID
        }
    }
}
