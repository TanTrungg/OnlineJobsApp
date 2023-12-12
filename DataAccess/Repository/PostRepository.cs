using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;
using DataAccess.DAO;
using DataAccess.IRepository;

namespace DataAccess.Repository
{
    public class PostRepository : IPostRepository
    {
        public IList<Post> GetPostList() => PostDAO.Instance.GetPostList();

        //public IEnumerable<Post> GetPostList1() => PostDAO.Instance.GetPostList1();

        public IList<Post> GetPostListBySearchTitleOrMajor(string searchString) => PostDAO.Instance.GetPostListBySearchTitleOrMajor(searchString);

        public Post GetPostByID(int postID) => PostDAO.Instance.GetPostByID(postID);

        public Post GetById(int id) => PostDAO.Instance.GetById(id);
    }
}
