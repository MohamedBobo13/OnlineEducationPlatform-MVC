using OnlineEducationPlatform.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.BLL.Validation.LectureValidation
{
    public class LectureExistAttribute : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is int lectureid)
            {
                var LectureRepo = validationContext.GetService(typeof(ILectureRepo)) as ILectureRepo;

                if (LectureRepo != null && !LectureRepo.IdExist(lectureid))
                {
                    return new ValidationResult(ErrorMessage ?? "The specified Lecture ID does not exist.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
     
   