using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace OnlineJobsApp.Pages
{
    [Authorize(Policy = "MustBelongToJobSeeker")]
    public class ReportModel : PageModel
    {
        IAdminRepository admin = new AdminRepository();
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            await admin.ReportPost((int)id);
            return Redirect("./PostDetail?id=" + id);
        }
    }
}
