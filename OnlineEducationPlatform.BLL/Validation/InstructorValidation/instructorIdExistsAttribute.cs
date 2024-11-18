using OnlineEducationPlatform.DAL.Repo.InstructorRepo;
using OnlineEducationPlatform.DAL.Repo.StudentRepo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.BLL.Validation.InstructorValidation
{
    public class instructorIdExistsAttribute:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string instructorId)
            {
                var instructorRepo = validationContext.GetService(typeof(IInstructorRepo)) as IInstructorRepo;

                if (instructorRepo != null && !instructorRepo.IdExist(instructorId))
                {
                    return new ValidationResult(ErrorMessage ?? "The specified Instructor ID does not exist.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
