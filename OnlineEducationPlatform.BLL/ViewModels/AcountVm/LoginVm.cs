using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.BLL.ViewModels.AcountVm
{
    public class LoginVm
    {
        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
        public string Password { get; set; }
    }
}
