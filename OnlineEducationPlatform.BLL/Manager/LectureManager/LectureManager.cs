using AutoMapper;
using OnlineEducationPlatform.DAL.Data.Models;
using OnlineEducationPlatform.DAL.Repositories;
using OnlineEducationPlatform.BLL.Dto.LectureDto;

namespace OnlineEducationPlatform.BLL.Manager
{
    public class LectureManager : ILectureManager
    {
        private readonly ILectureRepo _lectureRepo;
        private readonly IMapper _mapper;

        public LectureManager(ILectureRepo lectureRepo,IMapper mapper)
        {
            _lectureRepo = lectureRepo;
            _mapper = mapper;
        }
        public async Task AddAsync(LectureAddVm lectureAddDto)  
        {
            var lecture = _mapper.Map<Lecture>(lectureAddDto);
            await _lectureRepo.AddAsync(lecture);

        }


        public async Task<bool> DeleteAsync(int id)
        {
            var lecture = await _lectureRepo.GetByIdAsync(id);
            if (lecture != null)
            {
                var result = await _lectureRepo.DeleteAsync(lecture.Id);
                //SaveChangesAsync();
                if (result == true)
                {
                    return true;
                }
                return false;
            }
            return false;
        }


        public async Task<IEnumerable<LectureReadVm>> GetAllAsync()
        {
            var lectures = await _lectureRepo.GetAllAsync();
            return _mapper.Map<List<LectureReadVm>>(lectures);
        }



        public async Task<LectureReadVm> GetByIdAsync(int id)
        {
            var lecture = await _lectureRepo.GetByIdAsync(id);
            if (lecture != null)
            {
                return _mapper.Map<LectureReadVm>(lecture);
            }
            return null;
        }


        public async Task<LectureUpdateVm> UpdateAsync(LectureUpdateVm lectureUpdateDto)
        {
            var lecture = await _lectureRepo.GetByIdAsync(lectureUpdateDto.Id);
            if (lecture != null)
            {
                lecture.Id = lectureUpdateDto.Id;
                lecture.Order = lectureUpdateDto.Order;
               
                lecture.Title = lectureUpdateDto.Title;

                await _lectureRepo.UpdateAsync(lecture);
            }
            return null;
        }
        public bool IdExist(int lectureid)
        {
            return _lectureRepo.IdExist(lectureid);
        }
    }
}
