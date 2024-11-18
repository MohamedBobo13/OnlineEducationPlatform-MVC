using AutoMapper;
using OnlineEducationPlatform.BLL.ViewModels.Quizresultsdto;
using OnlineEducationPlatform.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.BLL.AutoMapper.QuizResultAutoMapper
{
    public class QuizResultMappingProfile : Profile
    {
        public QuizResultMappingProfile()
        {
            CreateMap<QuizResult, quizresultreadvm>().ReverseMap();

            CreateMap<QuizResult, quizresultwithoutidvm>().ReverseMap();

        }
    }
}
