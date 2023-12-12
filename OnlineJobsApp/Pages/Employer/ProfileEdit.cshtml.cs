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
    public class ProfileEditModel : PageModel
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

        public async Task<IActionResult> OnPostAsync(int comId)
        {
            try
            {
                var com = await employerRepository.GetCompanyById(comId);
                if (com == null)
                {
                    return NotFound();
                }

                com.Email = Company.Email;
                com.FullName = Company.FullName;
                com.Address = Company.Address;
                com.PhoneNumber = Company.PhoneNumber;

                await employerRepository.UpdateCompany(com);
                return RedirectToPage("/Employer/Profile");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
