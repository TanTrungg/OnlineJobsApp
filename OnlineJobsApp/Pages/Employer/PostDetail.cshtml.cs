using BusinessObject.Models;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineJobsApp.Pages.Employer
{
    [Authorize(Policy = "MustBelongToCompany")]
    public class PostDetailModel : PageModel
    {
        IEmployerRepository employerRepository = new EmployerRepository();

        [BindProperty]
        public Post Post { get; set; }
        //public Boolean isAvailable { get; set; }
        //public IEnumerable<Comment> comments { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                Post = await employerRepository.GetPostById(id);    
                if (Post == null)
                {
                    return NotFound();  
                }
                return Page();
            }
            catch(Exception ex)
            {
                TempData["Error"] = "Something went wrong! Error: " + ex.Message;
                return Redirect("/Employer/PostList");
            }
        }

        public async Task<IActionResult> OnPostCloseAsync(int postId)
        {
            try
            {
                var post = await employerRepository.GetPostById(postId);
                if (post == null)
                {
                    return NotFound();
                }
                await employerRepository.ClosePost(post);
                return RedirectToPage("/Employer/PostList");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Something went wrong! Error: " + ex.Message;
                return Redirect("/Employer/PostList");
            }
        }

        public async Task<IActionResult> OnPostOpenAsync(int postId)
        {
            try
            {
                var post = await employerRepository.GetPostById(postId);
                if (post == null)
                {
                    return NotFound();
                }
                await employerRepository.OpenPost(post);
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
