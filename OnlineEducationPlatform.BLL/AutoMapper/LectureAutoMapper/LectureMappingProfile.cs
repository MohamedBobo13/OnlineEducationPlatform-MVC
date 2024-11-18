using AutoMapper;
using OnlineEducationPlatform.BLL.Dto.LectureDto;
using OnlineEducationPlatform.DAL.Data.Models;

namespace OnlineEducationPlatform.BLL.AutoMapper.LectureAutoMapper
{
    public class LectureMappingProfile : Profile
    {
        public LectureMappingProfile()
        {
            CreateMap<Lecture, LectureAddVm>().ReverseMap();
            CreateMap<Lecture, LectureReadVm>().ReverseMap();
            CreateMap<Lecture, LectureUpdateVm>().ReverseMap();
        }
    }
}
