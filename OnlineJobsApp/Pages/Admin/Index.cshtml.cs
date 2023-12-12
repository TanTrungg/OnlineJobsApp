using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace OnlineJobsApp.Pages.Admin
{
    [Authorize(Policy = "MustBelongToAdmin")]
    public class IndexModel : PageModel
    {
        IAdminRepository admin = new AdminRepository();
        private readonly IConfiguration Configuration;

        public IndexModel(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public PaginatedList<User> User { get; set; }

        public async Task<IActionResult> OnGetAsync(int? pageIndex)
        {
            
            try
            {
                var pageSize = Configuration.GetValue("PageSize", 4);

                int id = 1;
                var lilPost = await admin.GetUsers();
                User = PaginatedList<User>.CreatePaging(
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
