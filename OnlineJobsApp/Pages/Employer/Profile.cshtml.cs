using BusinessObject.Models;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineJobsApp.Pages.Employer
{
    [Authorize(Policy = "MustBelongToCompany")]
    public class ProfileModel : PageModel
    {
        IEmployerRepository employerRepository = new EmployerRepository();

        [BindProperty]
        public Company Company { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                int cid = Int32.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                Company = await employerRepository.GetCompanyById(cid);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Something went wrong! Error: " + ex.Message;
            }
        }

        public async Task<IActionResult> OnPostChangePasswordAsync(string currentPassword, string newPassword)
        {
            int cid = Int32.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            Company company = await employerRepository.GetCompanyById(cid);
            if (company.Password != currentPassword)
            {
                return new JsonResult(new { success = false });
            }
            company.Password = newPassword;
            await employerRepository.UpdateCompany(company);

            return new JsonResult(new { success = true });
        }
    }
}
