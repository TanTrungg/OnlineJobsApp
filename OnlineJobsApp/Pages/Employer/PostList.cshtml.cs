using BusinessObject.Models;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineJobsApp.Pages.Employer
{
    [Authorize(Policy = "MustBelongToCompany")]
    public class PostListModel : PageModel
    {
        IEmployerRepository employerRepository = new EmployerRepository();

        private readonly IConfiguration Configuration;

        public PostListModel(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        //public List<Post> Post { get; set; }
        public PaginatedList<Post> Post { get; set; }

        public async Task<IActionResult> OnGetAsync(int? pageIndex)
        {
            try
            {
                var pageSize = Configuration.GetValue("PageSize", 4);

                int id = 1;
                int cid = Int32.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var lilPost = await employerRepository.GetPostListByComId(cid);
                Post = PaginatedList<Post>.CreatePaging(
               lilPost, pageIndex ?? 1, pageSize);
                return Page();
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Something went wrong! Error: " + ex.Message;
                return NotFound();
            }
        }
    }
}
