using BusinessObject.Models;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace OnlineJobsApp.Pages.Admin
{
    [Authorize(Policy = "MustBelongToAdmin")]
    public class banOrAccComModel : PageModel
    {
        IAdminRepository admin = new AdminRepository();
        //[BindProperty]
        //public Company Company { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            await admin.DeActiveCompany((int)id);
            return RedirectToPage("./CompanyIndex");
        }
    }
}
