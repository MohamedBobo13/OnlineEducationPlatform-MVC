using OnlineEducationPlatform.BLL.Dto.VideoDto;

namespace OnlineEducationPlatform.BLL.Manager
{
    public interface IVedioManager
    {
        Task AddAsync(VedioAddVm vedioAddDto);
        Task<IEnumerable<VedioReadVm>> GetAllAsync();
        Task<VedioReadVm> GetByIdAsync(int id);
        Task<VedioUpdateVm> UpdateAsync(VedioUpdateVm vedioUpdateDto);
        Task<bool> DeleteAsync(int id);

    }
}
