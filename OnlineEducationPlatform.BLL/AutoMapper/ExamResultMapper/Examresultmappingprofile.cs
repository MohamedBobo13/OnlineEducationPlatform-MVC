using AutoMapper;
using OnlineEducationPlatform.BLL.ViewModels.Quizresultsdto;
using OnlineEducationPlatform.DAL.Data.Models;

namespace OnlineEducationPlatform.BLL.AutoMapper.ExamResultMapper
{
	public class Examresultmappingprofile :Profile
    {
        public Examresultmappingprofile()
        {
            
            
                CreateMap<ExamResult, Examresultreadvm>().ReverseMap();

                CreateMap<ExamResult, Examresultwithoutidvm>().ReverseMap();

            
        }
    }
}
