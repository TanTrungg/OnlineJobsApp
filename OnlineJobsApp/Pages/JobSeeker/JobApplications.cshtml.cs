using BusinessObject.Models;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Security.Claims;

namespace OnlineJobsApp.Pages.JobSeeker
{
    [Authorize(Policy = "MustBelongToJobSeeker")]
    public class JobApplicationsModel : PageModel
    {
        private readonly IEmployerRepository _employerRepo;
        private readonly IUserRepository _userRepo;
        public JobApplicationsModel()
        {
            _employerRepo = new EmployerRepository();
            _userRepo = new UserRepository();
        }
        public List<Candidate> Candidates { get; set; }
        public void OnGet()
        {
            User user = GetUserLogin();
            Candidates = _employerRepo.GetCandidateByUserId(user.Id);
        }

        private User GetUserLogin()
        {
            //getUserlogin
            ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
            string emailLogin = identity.FindFirst(ClaimTypes.Email)?.Value;
            return _userRepo.getUserByEmail(emailLogin);

        }
    }
}
