using AutoMapper;
using OnlineEducationPlatform.BLL.ViewModels.ExamDto;
using OnlineEducationPlatform.BLL.ViewModels.QuizDto;
using OnlineEducationPlatform.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.BLL.AutoMapper.ExamMappingProfile
{
    public class ExamMappingProfile:Profile
    {
        public ExamMappingProfile()
        {
            CreateMap<Exam, ExamAddVm>().ReverseMap();
            CreateMap<Exam, ExamReadVm>().ReverseMap();
            CreateMap<Exam, ExamUpdateVm>().ReverseMap();
        }

    }
}
