using AutoMapper;
using OnlineEducationPlatform.BLL.Dtos;
using OnlineEducationPlatform.DAL.Data.Models;
using OnlineEducationPlatform.DAL.Repo.AnswerResultRepo;

public class AnswerResultManager : IAnswerResultManager
{
    private readonly IAnswerResultRepo _answerResultRepo;
    private readonly IMapper _mapper;

    public AnswerResultManager(IAnswerResultRepo answerResultRepo, IMapper mapper)
    {
        _answerResultRepo = answerResultRepo;
        _mapper = mapper;
    }

    public async Task<List<AnswerResultReadVm>> GetAllAsync()
    {
        var answerResults = await _answerResultRepo.GetAllAsync();
        return _mapper.Map<List<AnswerResultReadVm>>(answerResults);
    }

    public async Task<AnswerResultReadVm> GetByIdAsync(int id)
    {
        var answerResult = await _answerResultRepo.GetByIdAsync(id);
        return _mapper.Map<AnswerResultReadVm>(answerResult);
    }

    public async Task AddAsync(AnswerResultAddVm answerResultAddVm)
    {
        var answerResult = _mapper.Map<AnswerResult>(answerResultAddVm);
        await _answerResultRepo.AddAsync(answerResult);
    }

    public async Task UpdateAsync(AnswerResultUpdateVm answerResultUpdateVm)
    {
        var answerResult = _mapper.Map<AnswerResult>(answerResultUpdateVm);
        await _answerResultRepo.UpdateAsync(answerResult);
    }

    public async Task DeleteAsync(AnswerResult answerResult)
    {
        await _answerResultRepo.DeleteAsync(answerResult);
    }
    public bool IdExist(int answerResultId)
    {
        return _answerResultRepo.IdExist(answerResultId);
    }
}
