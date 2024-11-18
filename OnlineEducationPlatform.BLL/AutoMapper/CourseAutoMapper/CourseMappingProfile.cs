using AutoMapper;
using OnlineEducationPlatform.BLL.Dto.CourseDto;
using OnlineEducationPlatform.DAL.Data.Models;

namespace OnlineEducationPlatform.BLL.AutoMapper.CourseAutoMapper
{
    public class CourseMappingProfile : Profile
    {
        public CourseMappingProfile()
        {
            CreateMap<Course, CourseAddVm>().ReverseMap();
            CreateMap<Course, CourseReadVm>().ReverseMap();
            CreateMap<Course, CourseUpdateVm>().ReverseMap();
        }
    }
}
