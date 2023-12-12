using BusinessObject.Models;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Threading.Tasks;

namespace OnlineJobsApp.Pages.Employer
{
    [Authorize(Policy = "MustBelongToCompany")]
    public class PostEditModel : PageModel
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
            ViewData["MajorId"] = new SelectList(employerRepository.GetMajorCt().Majors, "Id", "MajorName");
            //ViewData["UserId"] = CustomAuthorization.loginUser.Id;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int postId)
        {
            try
            {
                var post = await employerRepository.GetPostById(postId);
                if (post == null)
                {
                    return NotFound();
                }

                post.Title = Post.Title;
                post.Content = Post.Content;
                post.MajorId = Post.MajorId;

                await employerRepository.UpdatePost(post);
                return RedirectToPage("/Employer/PostList");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        }
}
