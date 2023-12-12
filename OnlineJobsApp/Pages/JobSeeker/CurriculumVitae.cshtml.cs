using BusinessObject.Models;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System;

namespace OnlineJobsApp.Pages.JobSeeker
{
    [Authorize(Policy = "MustBelongToJobSeeker")]
    public class CurriculumVitaeModel : PageModel
    {
        private readonly IPostRepository _postRepo;
        private readonly IUserRepository _userRepo;
        private readonly IEmployerRepository _employerRepo;
        private readonly IWebHostEnvironment _environment;
        public CurriculumVitaeModel(IWebHostEnvironment environment)
        {
            _postRepo = new PostRepository();
            _userRepo = new UserRepository();
            _employerRepo = new EmployerRepository();
            _environment = environment;
        }
        [BindProperty]
        public Information info { get; set; }


        [BindProperty]
        [Required(ErrorMessage = "Please choosen CV")]
        public IFormFile CVFile { get; set; }

        [BindProperty]
        public Candidate candidate { get; set; }
        //http://localhost:1261/JobSeeker/CurriculumVitae?id=1

        public String errorMessage { get; set; } = null;


        public IActionResult OnGet(int id)
        {
            //infor

            Post post = _postRepo.GetById(id);
            if (post == null)
            {
                return NotFound();
            }

            User applier = GetUserLogin();
            info = new Information();

            info.postName = post.Title;
            info.companyName = post.Com.FullName;
            info.major = post.Major.MajorName;
            info.applicantName = applier.FullName;
            info.email = applier.Email;
            info.phoneNumber = applier.PhoneNumber;



            //send post
            candidate = new Candidate();

            candidate.UserId = applier.Id;
            candidate.PostId = post.Id;

            return Page();

        }

        private User GetUserLogin()
        {

            //getUserlogin
            ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
            string emailLogin = identity.FindFirst(ClaimTypes.Email)?.Value;
            return _userRepo.getUserByEmail(emailLogin);

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                // Pass info object back to the page

                return Page();
            }
            // Check if the file is in PDF format
            if (CVFile.ContentType != "application/pdf")
            {
                ModelState.AddModelError("CVFile", "Please upload a PDF file");
                return Page();
            }

            if (_employerRepo.CheckCandidate((int)candidate.PostId, (int)candidate.UserId))
            {
                errorMessage = "Bạn đã là candidate. Vui lòng không gửi thêm yêu cầu nữa";
                return Page();
            }

            if (_employerRepo.CheckUserApply((int)candidate.PostId, (int)candidate.UserId))
            {
                errorMessage = "Bạn đã apply job này rồi và job của bạn đang được duyệt";
                return Page();
            }
            

            User user = GetUserLogin();
            string fileName = $"{Regex.Replace(user.FullName, @"[^\w\d]+", "_")}_CV_{Guid.NewGuid().ToString().Substring(0, 8)}.pdf";

            //tạo đường dẫn tới CV trong prj
            string uploadsFolder = Path.Combine(_environment.WebRootPath, "CV");

            // nếu chưa có folder CV thì tạo mới
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            // Save the uploaded file to the "CV" folder
            string filePath = Path.Combine(uploadsFolder, fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                CVFile.CopyTo(fileStream);
            }

            candidate.Cv = Path.Combine("CV", fileName);
            candidate.IsDeleted = false;

            _employerRepo.addCandidate(candidate);
            return RedirectToPage("/JobSeeker/JobApplications");
        }
    }

    public class Information
    {
        public string postName { get; set; }
        public string companyName { get; set; }
        public string major { get; set; }
        public string applicantName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
    }
}
