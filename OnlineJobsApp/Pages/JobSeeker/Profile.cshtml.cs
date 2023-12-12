using BusinessObject.Models;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace OnlineJobsApp.Pages.JobSeeker
{
    [Authorize(Policy = "MustBelongToJobSeeker")]
    public class ProfileModel : PageModel
    {
        
        [BindProperty]
        public User jobSeeker { get; set; }

        public void OnGet()
        {
            User user = GetUserLogin();
            if(user != null)
            {
                jobSeeker = user;
            }
        }

        private User GetUserLogin()
        {
            //getUserlogin
            ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
            string emailLogin = identity.FindFirst(ClaimTypes.Email)?.Value;
            IUserRepository userRepository = new UserRepository();
            return userRepository.getUserByEmail(emailLogin);
        }
        
        public IActionResult OnPostChangePassword(string currentPassword, string newPassword)
        {
            User user = GetUserLogin();
            if(user.Password != currentPassword)
            {
                return new JsonResult(new { success = false });
            }
            IUserRepository userRepository = new UserRepository();
            user.Password = newPassword;
            userRepository.updatePassword(user);

            return new JsonResult(new { success = true });
        }
    }
}
