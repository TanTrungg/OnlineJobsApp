using BusinessObject.Models;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace OnlineJobsApp.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public User userRegister { get; set; }

        [BindProperty]
        public string confirmPass { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            //check duplicate email
            IUserRepository userRepository = new UserRepository();
            var flag = userRepository.getUserByEmail(userRegister.Email);
            if(flag != null)
            {
                ModelState.AddModelError("userRegister.Email", "Email has been used");
            }

            //check password and confirm password
            if(userRegister.Password != confirmPass)
            {
                ModelState.AddModelError("confirmPass", "Not match with password");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            //register user
            userRegister.IsDeleted = false;
            userRegister.CreatedAt = DateTime.Now;

            userRepository.addNewUser(userRegister);
            return RedirectToPage("/Login");
        }
    }
}
