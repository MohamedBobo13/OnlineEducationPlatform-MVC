using OnlineEducationPlatform.DAL.Repo.AnswerRepo;
using OnlineEducationPlatform.DAL.Repo.StudentRepo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.BLL.Validation.StudentValidation
{
    public class StudentIdExistsAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string studentId)
            {
                var studentRepo = validationContext.GetService(typeof(IStudentRepo)) as IStudentRepo;

                if (studentRepo != null && !studentRepo.IdExist(studentId))
                {
                    return new ValidationResult(ErrorMessage ?? "The specified Student ID does not exist.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
