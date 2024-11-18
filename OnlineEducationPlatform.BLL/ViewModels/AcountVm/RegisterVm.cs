using OnlineEducationPlatform.BLL.ViewModels.AcountVm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.BLL.ViewModels.AcountVm
{
    public class RegisterStudentVm
    {
        [Required(ErrorMessage = "Username is required"), StringLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email Format")]

        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "Password must be at least 6 characters long", MinimumLength = 6)]
        [DataType(DataType.Password)]
    //        [Required(ErrorMessage = "Password is required")]
    //[DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Password confirmation is required")]

        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [DataType(DataType.Password)]

        public string ConfirmPassword { get; set; }
        // public TypeUser UserType { get; set; } = TypeUser.Student;

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }

    public class RegesterAdminVm : RegisterStudentVm
    {
        [Required(ErrorMessage = "UserType is required")]

        public TypeUser UserType { get; set; } = TypeUser.Student;

    }

}
