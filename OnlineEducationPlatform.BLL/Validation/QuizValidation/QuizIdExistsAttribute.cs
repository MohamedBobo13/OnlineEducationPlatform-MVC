using OnlineEducationPlatform.DAL.Repo.AnswerRepo;
using OnlineEducationPlatform.DAL.Repo.QuizRepo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.BLL.Validation.QuizValidation
{
    public class QuizIdExistsAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is int quizId)
            {
                var quizRepo = validationContext.GetService(typeof(IQuizRepo)) as IQuizRepo;

                if (quizRepo != null && !quizRepo.IdExist(quizId))
                {
                    return new ValidationResult(ErrorMessage ?? "The specified Quiz ID does not exist.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
