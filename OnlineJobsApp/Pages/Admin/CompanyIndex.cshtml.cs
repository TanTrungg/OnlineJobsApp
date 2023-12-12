using BusinessObject.Models;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineJobsApp.Pages.Admin
{
    [Authorize(Policy = "MustBelongToAdmin")]
    public class CompanyIndexModel : PageModel
    {
        IAdminRepository admin = new AdminRepository();
        private readonly IConfiguration Configuration;

        public CompanyIndexModel(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public PaginatedList<Company> Company { get; set; }
        public async Task<IActionResult> OnGetAsync(int? pageIndex)
        {
            try
            {
                var pageSize = Configuration.GetValue("PageSize", 4);

                int id = 1;
                var lilPost = await admin.GetMember();
                Company = PaginatedList<Company>.CreatePaging(
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
