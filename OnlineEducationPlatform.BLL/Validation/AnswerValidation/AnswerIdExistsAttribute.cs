using OnlineEducationPlatform.DAL.Repo.AnswerRepo;
using OnlineEducationPlatform.DAL.Repo.QuestionRepo;
using System.ComponentModel.DataAnnotations;

public class AnswerIdExistsAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is int answerId)
        {
            var answerRepo = validationContext.GetService(typeof(IAnswerRepo)) as IAnswerRepo;

            if (answerRepo != null && !answerRepo.IdExist(answerId))
            {
                return new ValidationResult(ErrorMessage ?? "The specified Answer ID does not exist.");
            }
        }
        return ValidationResult.Success;
    }
}
