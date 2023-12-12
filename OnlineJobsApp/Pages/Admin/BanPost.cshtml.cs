using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace OnlineJobsApp.Pages.Admin
{
    public class BanPostModel : PageModel
    {
        IAdminRepository admin = new AdminRepository();
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            await admin.BanPost((int)id);
            return RedirectToPage("./ReportedPost");
        }
    }
}
