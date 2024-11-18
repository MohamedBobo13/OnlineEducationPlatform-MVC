using AutoMapper;
using OnlineEducationPlatform.BLL.Dto.VideoDto;
using OnlineEducationPlatform.DAL.Data.Models;

namespace OnlineEducationPlatform.BLL.AutoMapper.VideoAutoMapper
{
    public class VedioMappingProfile : Profile
    {
        public VedioMappingProfile()
        {
            CreateMap<Video, VedioAddVm>().ReverseMap();
            CreateMap<Video, VedioReadVm>().ReverseMap();
            CreateMap<Video, VedioUpdateVm>().ReverseMap();
        }
    }
}
