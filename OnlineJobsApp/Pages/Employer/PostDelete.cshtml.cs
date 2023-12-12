using BusinessObject.Models;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace OnlineJobsApp.Pages.Employer
{
    [Authorize(Policy = "MustBelongToCompany")]
    public class PostDeleteModel : PageModel
    {
        IEmployerRepository employerRepository = new EmployerRepository();

        [BindProperty]
        public Post Post { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Post = await employerRepository.GetPostById(id);

            if (Post == null)
            {
                return NotFound();
            }

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                var post = await employerRepository.GetPostById(id);
                if (post == null)
                {
                    return NotFound();
                }
                await employerRepository.DeletePost(post);
                return RedirectToPage("/Employer/PostList");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Something went wrong! Error: " + ex.Message;
                return Redirect("/Employer/PostList");
            }
        }

    }
}
