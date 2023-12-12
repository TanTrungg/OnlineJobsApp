using BusinessObject.Models;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineJobsApp.Pages.Employer
{
    [Authorize(Policy = "MustBelongToCompany")]
    public class PostCreateModel : PageModel
    {
        IEmployerRepository employerRepository = new EmployerRepository();

        public IActionResult OnGet()
        {
            ViewData["MajorId"] = new SelectList(employerRepository.GetMajorCt().Majors, "Id", "MajorName");
            //ViewData["UserId"] = CustomAuthorization.loginUser.Id;
            return Page();
        }

        [BindProperty]
        public Post Post { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                Post.ComId = Int32.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                await employerRepository.CreatePost(Post);
                return RedirectToPage("/Employer/PostList"); 
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        }
}
