using BusinessObject.Models;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineJobsApp.Pages.Employer
{
    [Authorize(Policy = "MustBelongToCompany")]
    public class CandidateListModel : PageModel
    {
        IEmployerRepository employerRepository = new EmployerRepository();

        public List<Candidate> Candidate { get; set; }

        public async Task OnGetAsync(int id)
        {
            try
            {
                Candidate = await employerRepository.GetCandidateListByPostId(id);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Something went wrong! Error: " + ex.Message;
            }
        }
    }
}
