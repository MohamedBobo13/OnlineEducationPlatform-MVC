using Microsoft.AspNetCore.Identity;
using OnlineEducationPlatform.BLL.ViewModels.AcountVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.BLL.Manager.Account_Manager
{
    public interface IAccountManager
    {
        Task<IdentityResult> RegisterStudent(RegisterStudentVm registervm);
        Task<IdentityResult> RegisterAdmin(RegesterAdminVm registervm);

        Task LogoutUser();
        Task<string> LoginUser(LoginVm loginDto, bool rememberMe);
    }
}
