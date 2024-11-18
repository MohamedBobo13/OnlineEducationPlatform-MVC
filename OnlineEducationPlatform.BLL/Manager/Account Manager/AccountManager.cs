
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using OnlineEducationPlatform.BLL.ViewModels.AcountVm;
using OnlineEducationPlatform.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.BLL.Manager.Account_Manager
{
    public class AccountManager:IAccountManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AccountManager(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;

        }

        public async Task<IdentityResult> RegisterAdmin(RegesterAdminVm registervm)
        {
            ApplicationUser user = null;
            if (registervm.UserType == TypeUser.Student)
            {
                user = new Student();
                user.Email = registervm.Email;
                user.UserName = registervm.Name;
                user.PhoneNumber = registervm.PhoneNumber;
                user.UserType = TypeUser.Student;

            }
            else if (registervm.UserType == TypeUser.Instructor)
            {
                user = new Instructor();
               
                user.Email = registervm.Email;
                user.UserName = registervm.Name;
                user.PhoneNumber = registervm.PhoneNumber;
                user.UserType = TypeUser.Instructor;
            }
            else if (registervm.UserType == TypeUser.Admin)
            {

                user = new Admin();
                user.Email = registervm.Email;
                user.UserName = registervm.Name;
                user.PhoneNumber = registervm.PhoneNumber;
                user.UserType = TypeUser.Admin;
            }
         

            // Create the user with the provided password
            var result = await _userManager.CreateAsync(user, registervm.Password);
            if (result.Succeeded)
            {
                string usertype = user.UserType.ToString();
                // Assign the role
                if (await _roleManager.RoleExistsAsync(usertype))
                      {
                          await _userManager.AddToRoleAsync(user, usertype);
                     }
                else
                {
                    return IdentityResult.Failed(new IdentityError { Description = $"Role '{usertype}' does not exist." });
                }



            }
            return result;
        }
            public async Task<IdentityResult> RegisterStudent(RegisterStudentVm registervm)
            {
                ApplicationUser user = new Student
                {
                    UserName = registervm.Name,
                    Email = registervm.Email,

                    PhoneNumber = registervm.PhoneNumber,

                    UserType = TypeUser.Student
                };

                // Create the user with the provided password
                var result = await _userManager.CreateAsync(user, registervm.Password);
                if (result.Succeeded)
                {
                    // Assign the role

                    string userrole = TypeUser.Student.ToString();
                    await _userManager.AddToRoleAsync(user, userrole);
                }


                //if (result.Succeeded)
                //{
                //    // Assign the role
                //    if (await _roleManager.RoleExistsAsync(registervm.UserType))
                //    {
                //        await _userManager.AddToRoleAsync(user, registervm.UserType);
                //    }
                //    else
                //    {
                //        return IdentityResult.Failed(new IdentityError { Description = $"Role '{registerDto.UserType}' does not exist." });
                //    }
                //}
                return result;
        }
        public async Task<string> LoginUser(LoginVm loginDto, bool rememberMe)
        {
            var userModel = await _userManager.FindByEmailAsync(loginDto.Email);
            if (userModel != null)
            {
                // Check if the provided password is valid
                var result = await _signInManager.PasswordSignInAsync(userModel.UserName, loginDto.Password, rememberMe == false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    // Successful login
                    return "Login successful";
                }
            }

            return "Invalid Email or Password";
        }
        public async Task LogoutUser()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
