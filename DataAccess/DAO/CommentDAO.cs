using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class CommentDAO
    {
        private OnlineJobSeekingManagementContext _context;
        private static CommentDAO instance = null;
        private static readonly object instanceLock = new object();

        private CommentDAO()
        {

        }

        public static CommentDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CommentDAO();
                    }
                    return instance;
                }
            }
        }

        public IList<Comment> GetCommentListByPostID(int id)
        {
            List<Comment> comments;
            try
            {
                var OnlineJobSeekingManagementContext = new OnlineJobSeekingManagementContext();
                comments = OnlineJobSeekingManagementContext.Comments.Include(c => c.Com)
                                                              .Include(c => c.User)
                                                              .Include(c => c.Post)
                                                               .Where(c => c.IsDeleted == false && c.PostId == id).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return comments;
        }

        //Lấy Comment từ CommentID
        public Comment GetCommentByID(int commentID)
        {
            Comment comment = null;
            try
            {
                var OnlineJobSeekingManagementContext = new OnlineJobSeekingManagementContext();
                comment = OnlineJobSeekingManagementContext.Comments.Include(c => c.Com)
                                                              .Include(c => c.User)
                                                              .Include(c => c.Post)
                                                              .SingleOrDefault(c => c.Id == commentID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return comment;
        }

        public void AddComment(Comment comment)
        {
            try
            {
                var OnlineJobSeekingManagementContext = new OnlineJobSeekingManagementContext();
                comment.IsDeleted = false;
                comment.CreatedAt = DateTime.Now;
                OnlineJobSeekingManagementContext.SaveChanges();
                OnlineJobSeekingManagementContext.Comments.Add(comment);
                OnlineJobSeekingManagementContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
