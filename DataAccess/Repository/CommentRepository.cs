using BusinessObject.Models;
using DataAccess.DAO;
using DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class CommentRepository :ICommentRepository
    {
        public IList<Comment> GetCommentListByPostID(int id) => CommentDAO.Instance.GetCommentListByPostID(id);

        public Comment GetCommentByID(int commentID) => CommentDAO.Instance.GetCommentByID(commentID);

        public void AddComment(Comment comment) => CommentDAO.Instance.AddComment(comment);

    }
}
