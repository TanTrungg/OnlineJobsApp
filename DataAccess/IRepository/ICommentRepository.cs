using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface ICommentRepository
    {
        IList<Comment> GetCommentListByPostID(int id);

        Comment GetCommentByID(int commentID);

        void AddComment(Comment comment);
    }
}
