using OnlineEducationPlatform.DAL.Repo.QuestionRepo;
using System.ComponentModel.DataAnnotations;

public class QuestionIdExistsAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is int questionId)
        {
            var questionRepo = validationContext.GetService(typeof(IQuestionRepo)) as IQuestionRepo;

            if (questionRepo != null && !questionRepo.IdExist(questionId))
            {
                return new ValidationResult(ErrorMessage ?? "The specified Question ID does not exist.");
            }
        }

        return ValidationResult.Success;
    }
}
