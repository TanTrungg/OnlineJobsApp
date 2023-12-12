using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;
using DataAccess.Commons;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
    public class PostDAO
    {
        private static PostDAO instance = null;
        private static readonly object instanceLock = new object();

        private PostDAO()
        {

        }

        public static PostDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new PostDAO();
                    }
                    return instance;
                }
            }
        }

        public IList<Post> GetPostList()
        {
            List<Post> posts;
            try
            {
                var OnlineJobSeekingManagementContext = new OnlineJobSeekingManagementContext();
                posts = OnlineJobSeekingManagementContext.Posts.Include(c => c.Com)
                                                               .Where(c => c.IsDeleted == false && c.Status == CommonEnums.POST_STATUS.AVAILABLE).ToList();   
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return posts;
        }

        public IList<Post> GetPostListBySearchTitleOrMajor(string searchString)
        {
            List<Post> posts;
            try
            {
                var OnlineJobSeekingManagementContext = new OnlineJobSeekingManagementContext();
                posts = OnlineJobSeekingManagementContext.Posts.Include(c => c.Com)
                                                               .Where(c => c.IsDeleted == false && c.Status == CommonEnums.POST_STATUS.AVAILABLE)
                                                               .Where(s => s.Title.Contains(searchString)
                                                                || s.Major.MajorName.Contains(searchString)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return posts;
        }

        //public IEnumerable<Post> GetPostList1()
        //{
        //    List<Post> posts;
        //    try
        //    {
        //        var OnlineJobSeekingManagementContext = new OnlineJobSeekingManagementContext();
        //        posts = OnlineJobSeekingManagementContext.Posts.Include(c => c.Com)
        //                                                       .Where(c => c.IsDeleted == false && c.Status == CommonEnums.POST_STATUS.AVAILABLE).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    return posts;
        //}

        public Post GetPostByID(int postID)
        {
            Post post = null;
            try
            {
                var OnlineJobSeekingManagementContext = new OnlineJobSeekingManagementContext();
                post = OnlineJobSeekingManagementContext.Posts.Include(p => p.Com)
                                                              .Include(p => p.Major)
                                                              .SingleOrDefault(post => post.Id == postID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return post;
        }

        //Tấn Trung
        public Post GetById(int id)
        {
            var _context = new OnlineJobSeekingManagementContext();
            return _context.Posts.Include(p => p.Major).Include(p => p.Com).SingleOrDefault(p => p.Id == id);
        }
    }
}
