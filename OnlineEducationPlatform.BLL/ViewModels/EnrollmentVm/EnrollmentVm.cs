using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OnlineEducationPlatform.BLL.ViewModels.EnrollmentDto
{
    public class EnrollmentVm
	{
        //  public int EnrollmentId { get; set; }
        [Required(ErrorMessage = "The StudentId field is required.")]
        public string StudentId { get; set; }
        [Required(ErrorMessage = "The CourseId field is required.")]

        public int CourseId { get; set; }
        //  public DateTime EnrollmentDate { get; set; }= DateTime.Now;
        //public EnrollmentStatus Status { get; set; }
    }
    public class enrollmentvmwithdate : EnrollmentVm
    {
       // [Required(ErrorMessage = "The EnrollmentDate field is required.")]
        [Required(ErrorMessage ="The EnrollmentDate Field is required")]
        public DateTime EnrollmentDate { get; set; } 


    }
    public class EnrollmentDtowWithStatusanddDate : EnrollmentVm
	{
        [Required(ErrorMessage = "The Status field is required.")]

        public string? Status { get; set; }
        [Required(ErrorMessage = "The EnrollmentDate field is required.")]

        public DateTime? EnrollmentDate { get; set; }
    }
    public class EnrollmentDtoForRetriveAllEnrollmentsInCourse : EnrollmentVm
	{
        public int Id { get; set; }
        public DateTime? EnrollmentDate { get; set; }
        public string? status { get; set; }



    }
}
