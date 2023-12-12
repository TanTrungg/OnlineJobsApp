using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;

namespace DataAccess.IRepository
{
    public interface IPostRepository
    {
        IList<Post> GetPostList();

        IList<Post> GetPostListBySearchTitleOrMajor(string searchString);

        //IEnumerable<Post> GetPostList1();
        Post GetPostByID(int postID);

        //Tấn Trung
        Post GetById(int id);
    }
}
