using OnlineEducationPlatform.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.BLL.Dto.LectureDto
{
    public class LectureUpdateVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Order { get; set; }
        //[ForeignKey("Course")]
        //public int CourseId { get; set; }
        //public Course Course { get; set; }
        //public ICollection<Quiz> Quizzes { get; set; } = new HashSet<Quiz>();
        // public ICollection<PdfFile> PdfFiles { get; set; } = new HashSet<PdfFile>();
        //public ICollection<Video> Videos { get; set; } = new HashSet<Video>();
    }
}
