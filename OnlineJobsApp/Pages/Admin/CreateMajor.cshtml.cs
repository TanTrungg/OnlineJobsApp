using BusinessObject.Models;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace OnlineJobsApp.Pages.Admin
{
    [Authorize(Policy = "MustBelongToAdmin")]
    public class CreateMajorModel : PageModel
    {
        IAdminRepository admin = new AdminRepository();
        public IActionResult OnGet()
        {
            return Page();
        }
        [BindProperty]
        public Major Major { get; set; }
        public string message { get; set; } = "";
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var a =await admin.GetMajorByName(Major.MajorName);
            if(a != null)
            {
                message = "Major with this name is already existed!";
                return Page();
            }
            admin.AddMajor(Major);
            return RedirectToPage("./MajorPage");
        }
    }
}
