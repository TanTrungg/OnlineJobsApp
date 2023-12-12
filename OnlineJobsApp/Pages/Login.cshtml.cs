using BusinessObject.Models;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace OnlineJobsApp.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IConfiguration _config;

        [BindProperty]
        public UserLogin userLogin { get; set; }

        public string errorMessage { get; set; }

        public LoginModel(IConfiguration config)
        {
            _config = config;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost(string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            IUserRepository userRepository = new UserRepository();
            IEmployerRepository employerRepository = new EmployerRepository();

            User admin = LoginByAdminAccount(userLogin.Email, userLogin.Password);
            User checkLogin = userRepository.checkUserLogin(userLogin.Email, userLogin.Password);
            Company c = employerRepository.GetCompanyByEmailAndPassword(userLogin.Email, userLogin.Password);

            if(admin != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "Admin"),
                    new Claim(ClaimTypes.Email, admin.Email),
                    new Claim(ClaimTypes.Role, "AD"),
                    new Claim("Admin", "AD")
                };
                var identity = new ClaimsIdentity(claims, "CookieAuth");
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync("CookieAuth", principal);
                return RedirectToPage("/Admin/MainPageAdmin");
            }

            if(checkLogin != null)
            {
                //security context
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, checkLogin.FullName),
                    new Claim(ClaimTypes.Email, checkLogin.Email),
                    new Claim(ClaimTypes.Role, "US"),
                    new Claim("JobSeeker", "US")
                };
                var identity = new ClaimsIdentity(claims, "CookieAuth");
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync("CookieAuth", principal);
                HttpContext.Session.SetInt32("UserID", checkLogin.Id);
                HttpContext.Session.SetString("UserFullName", checkLogin.FullName);

                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToPage("/Index");
                }                
            }
            else if (c != null)
            {
                string cid = c.Id.ToString();

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, cid),
                    new Claim(ClaimTypes.Name, c.FullName),
                    new Claim(ClaimTypes.Email, c.Email),
                    new Claim(ClaimTypes.Role, "CO"),
                    new Claim("Company", "CO")
                };
                var identity = new ClaimsIdentity(claims, "CookieAuth");
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync("CookieAuth", principal);
                HttpContext.Session.SetInt32("CompanyID", c.Id);
                HttpContext.Session.SetString("CompanyFullName", c.FullName);

                return RedirectToPage("/Employer/PostList");
            }
            else
            {
                errorMessage = "Your email or password is incorrect!";
                ViewData["Message"] = string.Format("Your email or password is incorrect!");
            }

            return Page();
        }
        private User LoginByAdminAccount(String email, String password)
        {
            String emails, passwords;
            User user = null;

            emails = _config["account:defaultAccount:email"];
            passwords = _config["account:defaultAccount:password"];

            if (email.Equals(emails) && password.Equals(passwords))
            {
                user = new User
                {
                    Email = email,
                    Password = password,
                };
                return user;
            }
            else
            {
                return null;
            }
        }
    }

    public class UserLogin
    {
        [Required(ErrorMessage ="Please input email")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please input password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
