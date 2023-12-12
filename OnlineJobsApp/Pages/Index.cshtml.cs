using BusinessObject.Models;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineJobsApp.Pages
{
    public class IndexModel : PageModel
    {

        IPostRepository postRepository = new PostRepository();

        //private readonly BusinessObject.Models.OnlineJobSeekingManagementContext _context;

        private readonly IConfiguration Configuration;

        //public IndexModel(OnlineJobSeekingManagementContext context, IConfiguration configuration)
        //{
        //    _context = context;
        //    Configuration = configuration;
        //}

        public IndexModel(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        //public IList<Post> Post { get; set; }

        public PaginatedList<Post> Post { get; set; }

        IList<Post> PostList = new List<Post>();

        public string CurrentFilter { get; set; }

        public IActionResult OnGet(string currentFilter, string searchString, int? pageIndex)
        {
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            PostList = postRepository.GetPostList();

            if (!String.IsNullOrEmpty(searchString))
            {
                PostList = postRepository.GetPostListBySearchTitleOrMajor(searchString);
            }

            var pageSize = Configuration.GetValue("PageSize", 4);

            
            Post = PaginatedList<Post>.CreatePaging(
               PostList, pageIndex ?? 1, pageSize);
            return Page();
        }

        //public async Task OnGetAsync(string currentFilter, string searchString, int? pageIndex)
        //{
        //    if (searchString != null)
        //    {
        //        pageIndex = 1;
        //    }
        //    else
        //    {
        //        searchString = currentFilter;
        //    }

        //    CurrentFilter = searchString;

        //    IQueryable<Post> postsIQ = (from s in _context.Posts
        //                                select s);

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        postsIQ = postsIQ.Where(s => s.Title.Contains(searchString)
        //                                || s.Major.MajorName.Contains(searchString));
        //    }
        //    var pageSize = Configuration.GetValue("PageSize", 4);


        //    Post = await PaginatedList<Post>.CreateAsync(
        //       postsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        //}

        //public IActionResult OnGetOnClick()
        //{
        //    return RedirectToPage("Details");
        //}
        //asp-page-handler="OnClick"
    }
}
