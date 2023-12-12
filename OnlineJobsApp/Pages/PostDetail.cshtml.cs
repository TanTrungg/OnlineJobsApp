using BusinessObject.Models;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;

namespace OnlineJobsApp.Pages
{
    public class PostDetailModel : PageModel
    {
        IPostRepository postRepository = new PostRepository();
        ICommentRepository commentRepository = new CommentRepository();

        public Post Post { get; set; }

        public int PostId { get; set; }

        public IList<Comment> Comments { get; set; }

        [BindProperty]
        public Comment CommentByUser { get; set; }

        [BindProperty]
        public Comment CommentByCompany { get; set; }

        public IActionResult OnGet(int id)
        {
            PostId = id;

            if (id == null)
            {
                return NotFound();
            }

            Post = postRepository.GetPostByID(id);

            HttpContext.Session.SetInt32("PostID", id);
            ViewData["PostID"] = id;
            ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
            ViewData["CompanyID"] = HttpContext.Session.GetInt32("CompanyID");
            //ViewData["TimeNow"] = DateTime.Now;
            ViewData["UserFullName"] = HttpContext.Session.GetString("UserFullName");
            ViewData["CompanyFullName"] = HttpContext.Session.GetString("CompanyFullName");

            Comments = commentRepository.GetCommentListByPostID(Post.Id);
            
            

            if (Post == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (HttpContext.Session.GetInt32("UserID") != null)
            {
                commentRepository.AddComment(CommentByUser);
            }
            if (HttpContext.Session.GetInt32("CompanyID") != null)
            {
                commentRepository.AddComment(CommentByCompany);
            }


            return RedirectToPage("/Index");
        }

    }
}
