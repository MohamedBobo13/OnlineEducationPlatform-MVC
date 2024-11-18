using OnlineEducationPlatform.BLL.ViewModels.InstructorDto;
using OnlineEducationPlatform.BLL.ViewModels.StudentDto;
using OnlineEducationPlatform.BLL.handleresponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.BLL.Manager.InstructorManager
{
    public interface IInstructorManager


    {


        Task<ServiceResponse<List<InstructorReadVm>>> GetAllInstructorsAsync();

        Task<ServiceResponse<InstructorReadVm>> GetInstructorbyid(string InstructorId);
        Task<ServiceResponse<bool>> softdeleteInstructor(string InstructorId);
        Task<InstructorReadVm> GetinstructorByinstructortId(string instructorId);

    }
}
