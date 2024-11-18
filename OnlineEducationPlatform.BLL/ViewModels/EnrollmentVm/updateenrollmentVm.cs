using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.BLL.ViewModels.EnrollmentDto
{
    public class updateenrollmentVm
    {
        [Required(ErrorMessage = "ID is required.")]
        public int Id { get; set; }
       
        public DateTime ?EnrollmentDate { get; set; } 
        [Required(ErrorMessage = "Student Id is required.")]

        public string StudentId { get; set; }
        [Required(ErrorMessage = "Course Id is required.")]

        public int CourseId { get; set; }
        public EnrollmentStatus Status { get; set; }

    }
}
