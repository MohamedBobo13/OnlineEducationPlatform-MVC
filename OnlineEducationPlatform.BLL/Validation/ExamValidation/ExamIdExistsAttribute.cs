using OnlineEducationPlatform.DAL.Repo.AnswerRepo;
using OnlineEducationPlatform.DAL.Repo.Iexamrepo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.BLL.Validation.ExamValidation
{
    public class ExamIdExistsAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is int examId)
            {
                var examRepo = validationContext.GetService(typeof(Iexamrepo)) as Iexamrepo;

                if (examRepo != null && !examRepo.IdExist(examId))
                {
                    return new ValidationResult(ErrorMessage ?? "The specified Exam ID does not exist.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
