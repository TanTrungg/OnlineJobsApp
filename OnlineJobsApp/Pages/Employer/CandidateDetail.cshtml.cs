using BusinessObject.Models;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace OnlineJobsApp.Pages.Employer
{
    [Authorize(Policy = "MustBelongToCompany")]
    public class CandidateDetailModel : PageModel
    {
        IEmployerRepository employerRepository = new EmployerRepository();

        [BindProperty]
        public Candidate Candidate { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                Candidate = await employerRepository.GetCandidateById(id);
                if (Candidate == null)
                {
                    return NotFound();
                }
                return Page();
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Something went wrong! Error: " + ex.Message;
                return Redirect("/Employer/CandidateList");
            }
        }

        public async Task<IActionResult> OnPostAcceptAsync(int canId)
        {
            try
            {
                var cd = await employerRepository.GetCandidateById(canId);
                if (cd == null)
                {
                    return NotFound();
                }
                await employerRepository.AcceptCandidate(cd);
                return Redirect("/Employer/CandidateList?id=" + cd.Post.Id);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Something went wrong! Error: " + ex.Message;
                return Redirect("/Employer/PostList");
            }
        }

        public async Task<IActionResult> OnPostDenyAsync(int canId)
        {
            try
            {
                var cd = await employerRepository.GetCandidateById(canId);
                if (cd == null)
                {
                    return NotFound();
                }
                await employerRepository.DenyCandidate(cd);
                return Redirect("/Employer/CandidateList?id=" + cd.Post.Id);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Something went wrong! Error: " + ex.Message;
                return Redirect("/Employer/PostList");
            }
        }
    }
}
