using AutoMapper;
using OnlineEducationPlatform.DAL.Data.Models;
using OnlineEducationPlatform.DAL.Repositories;
using OnlineEducationPlatform.BLL.Dto.CourseDto;

namespace OnlineEducationPlatform.BLL.Manager
{
    public class CourseManager : ICourseManager
    {
        private readonly ICourseRepo _courseRepo;
        private readonly IMapper _mapper;

        public CourseManager(ICourseRepo courseRepo,IMapper mapper)
        {
           _courseRepo = courseRepo;
            _mapper = mapper;
        }
        
        public async Task AddAsync(CourseAddVm courseAddDto) /// Done 
        {
            var course = _mapper.Map<Course>(courseAddDto);
            await _courseRepo.AddAsync(course);
           
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var course =await _courseRepo.GetByIdAsync(id);
            if (course != null)
            {
                var result= await _courseRepo.DeleteAsync(course.Id);
                //SaveChangesAsync();
                if (result == true)
                {
                    return true;
                }
                return false;
            }
            return false;
        }


        public async Task<IEnumerable<CourseReadVm>> GetAllAsync()
        {
            var courses =await _courseRepo.GetAllAsync();
            return _mapper.Map<List<CourseReadVm>>(courses);
        }

        

        public async Task<CourseReadVm> GetByIdAsync(int id)
        {
            var course=await _courseRepo.GetByIdAsync(id);
            if (course != null )
            {
                return _mapper.Map<CourseReadVm>(course);
            }
            return null;
        }

        
        public async Task<CourseUpdateVm> UpdateAsync(CourseUpdateVm courseUpdateDto)
        {
            var course = await _courseRepo.GetByIdAsync(courseUpdateDto.Id);
            if (course != null)
            {
                course.Id = courseUpdateDto.Id; 
                course.TotalHours = courseUpdateDto.TotalHours;
                course.CreatedDate = courseUpdateDto.CreatedDate;   
                course.Description = courseUpdateDto.Description;
                course.Title = courseUpdateDto.Title;
                course.InstructorId = courseUpdateDto.InstructorId;

                await _courseRepo.UpdateAsync(course);
            }
            return null;
        }

        public bool IdExist(int courseId)
        {
            return _courseRepo.IdExist(courseId);
        }
    }
}
