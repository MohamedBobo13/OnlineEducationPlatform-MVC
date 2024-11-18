using AutoMapper;
using OnlineEducationPlatform.BLL.ViewModels.EnrollmentDto;
using OnlineEducationPlatform.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.BLL.AutoMapper.EnrollmnentAutoMapper
{
    public class EnrollmentMappingProfile : Profile
    {
        public EnrollmentMappingProfile()
        {
            CreateMap<Enrollment, EnrollmentDtoForRetriveAllEnrollmentsInCourse>().ReverseMap();
            CreateMap<Enrollment, EnrollmentDtowWithStatusanddDate>().ReverseMap();
            CreateMap<Enrollment, enrollmentvmwithdate>().ReverseMap();




        }
    }
}
