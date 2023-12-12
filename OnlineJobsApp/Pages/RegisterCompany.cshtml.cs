using BusinessObject.Models;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace OnlineJobsApp.Pages
{
    public class RegisterCompanyModel : PageModel
    {
        [BindProperty]
        public Company userRegister { get; set; }

        [BindProperty]
        public string confirmPass { get; set; }

        IEmployerRepository employerRepository = new EmployerRepository();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var c = await employerRepository.GetCompanyByEmail(userRegister.Email);
                if (c != null)
                {
                    ModelState.AddModelError("userRegister.Email", "Email has been used");
                }

                if (userRegister.Password != confirmPass)
                {
                    ModelState.AddModelError("confirmPass", "Not match with password");
                }

                if (!ModelState.IsValid)
                {
                    return Page();
                }

                await employerRepository.AddCompany(userRegister);
                return RedirectToPage("/Login");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
