using OnlineEducationPlatform.BLL.ViewModels.Quizresultsdto;
using OnlineEducationPlatform.BLL.ViewModels.StudentDto;
using OnlineEducationPlatform.BLL.handleresponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineEducationPlatform.BLL.ViewModels.EnrollmentDto;

namespace OnlineEducationPlatform.BLL.Manager.StudentManager
{
    public interface Istudentmanager
    {
        Task<ServiceResponse<List<studentreadVm>>> GetAllStudentsAsync();

        Task<ServiceResponse<studentreadVm>> Getstudentbyid(string studentid);
        Task<ServiceResponse<bool>> softdeleteStudent(string studentId);
        Task<studentreadVm> GetstudenttByStudentId(string studentId);
        bool IdExist(string studentId);
    }
}
