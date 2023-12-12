using BusinessObject.Models;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace OnlineJobsApp.Pages.JobSeeker
{
    [Authorize(Policy = "MustBelongToJobSeeker")]
    public class EditProfileModel : PageModel
    {
        [BindProperty]
        public User jobSeeker { get; set; }

        public IActionResult OnGet(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            int userId = int.Parse(id);
            IUserRepository userRepository = new UserRepository();
            jobSeeker = userRepository.getUserByID(userId);
            return Page();
        }

        public IActionResult OnPost()
        {
            //check duplicate email
            IUserRepository userRepository = new UserRepository();
            var flag = userRepository.getUserByEmail(jobSeeker.Email);
            if (flag != null && flag.Id != jobSeeker.Id)
            {
                ModelState.AddModelError("jobSeeker.Email", "Email has been used");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if(jobSeeker != null)
            {
                //set time update
                DateTime updateTime = DateTime.Now;
                jobSeeker.UpdatedAt = updateTime;
                userRepository.updateUser(jobSeeker);

            }
            return RedirectToPage("./Profile");
        }
    }
}
