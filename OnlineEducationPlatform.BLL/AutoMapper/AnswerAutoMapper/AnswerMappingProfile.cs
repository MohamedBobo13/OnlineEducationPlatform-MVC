using AutoMapper;
using OnlineEducationPlatform.BLL.Dtos;
using OnlineEducationPlatform.BLL.ViewModels.AmswerVm;
using OnlineEducationPlatform.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.BLL.AutoMapper.AnswerAutoMapper
{
    public class AnswerMappingProfile : Profile
    {
        public AnswerMappingProfile()
        {
            CreateMap<Answer, AnswerAddVm>().ReverseMap();
            CreateMap<Answer, AnswerReadVm>().ReverseMap();
            CreateMap<Answer, AnswerUpdateVm>().ReverseMap();
        }
    }
}
