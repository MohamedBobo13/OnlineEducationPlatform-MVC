using OnlineEducationPlatform.DAL.Repo.AnswerRepo;
using OnlineEducationPlatform.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.BLL.Validation.CourseValidation
{
    public class CourseIdExistsAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is int courseId)
            {
                var courseRepo = validationContext.GetService(typeof(ICourseRepo)) as ICourseRepo;

                if (courseRepo != null && !courseRepo.IdExist(courseId))
                {
                    return new ValidationResult(ErrorMessage ?? "The specified Course ID does not exist.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
