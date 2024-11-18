using AutoMapper;
using OnlineEducationPlatform.BLL.Dtos;
using OnlineEducationPlatform.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.BLL.AutoMapper
{
    public class PdfFileMappingProfile:Profile
    {
        public PdfFileMappingProfile()
        {
            CreateMap<PdfFile, PdfFileAddVm>().ReverseMap();
            CreateMap<PdfFile, PdfFileReadVm>().ReverseMap();
            CreateMap<PdfFile, PdfFileUpdateVm>().ReverseMap();

        }
    }
}
