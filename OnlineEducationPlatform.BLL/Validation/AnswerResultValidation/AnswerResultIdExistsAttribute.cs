using OnlineEducationPlatform.DAL.Repo.AnswerResultRepo;
using OnlineEducationPlatform.DAL.Repo.QuestionRepo;
using System.ComponentModel.DataAnnotations;

public class AnswerResultIdExistsAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is int answerResultId)
        {
            var AnswerResultRepo = validationContext.GetService(typeof(IAnswerResultRepo)) as IAnswerResultRepo;

            if (AnswerResultRepo != null && !AnswerResultRepo.IdExist(answerResultId))
            {
                return new ValidationResult(ErrorMessage ?? "The specified Answer Result ID does not exist.");
            }
        }
        return ValidationResult.Success;
    }
}
