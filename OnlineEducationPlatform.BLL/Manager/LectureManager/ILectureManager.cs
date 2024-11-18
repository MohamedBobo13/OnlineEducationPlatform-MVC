using OnlineEducationPlatform.BLL.Dto.LectureDto;

namespace OnlineEducationPlatform.BLL.Manager
{
    public interface ILectureManager
    {
        Task AddAsync(LectureAddVm lectureAddDto);
        Task<IEnumerable<LectureReadVm>> GetAllAsync();
        Task<LectureReadVm> GetByIdAsync(int id);
        Task<LectureUpdateVm> UpdateAsync(LectureUpdateVm lectureUpdateDto);
        Task<bool> DeleteAsync(int id);
        public bool IdExist(int lectureid);
       
    }
}
