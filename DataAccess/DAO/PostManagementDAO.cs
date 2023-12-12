using BusinessObject.Models;
using DataAccess.Commons;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class PostManagementDAO
    {
        private static PostManagementDAO instance = null;
        private static readonly object instanceLock = new object();

        public static PostManagementDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new PostManagementDAO();
                    }
                    return instance;
                }
            }
        }

        public async Task<List<Post>> GetPostListByComId(int id)
        {
            List<Post> c = new List<Post>();
            try
            {
                using var context = new OnlineJobSeekingManagementContext();
                c = await context.Posts
                    .Where(c => c.IsDeleted == false && c.ComId == id)
                    .OrderByDescending(c => c.CreatedAt)
                    .Include(c => c.Major)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return c;
        }

        public async Task<Post> GetPostById(int? id)
        {
            Post post = new Post();
            try
            {
                using var context = new OnlineJobSeekingManagementContext();
                post = await context.Posts
                    .Where(c => c.Id == id && c.IsDeleted == false)
                    .Include(c => c.Major)
                    .Include(c => c.Com)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return post;
        }

        public async Task CreatePost(Post post)
        {
            try
            {
                using var context = new OnlineJobSeekingManagementContext();
                post.Status = CommonEnums.POST_STATUS.AVAILABLE;
                post.IsDeleted = false;
                post.CreatedAt = DateTime.Now;
                context.Posts.Add(post);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdatePost(Post post)
        {
            try
            {
                using var context = new OnlineJobSeekingManagementContext();
                post.UpdatedAt = DateTime.Now;
                context.Posts.Update(post);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeletePost(Post post)
        {
            try
            {
                using var context = new OnlineJobSeekingManagementContext();
                post.IsDeleted = true;
                post.UpdatedAt = DateTime.Now;
                context.Posts.Update(post);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task CLosePost(Post post)
        {
            try
            {
                using var context = new OnlineJobSeekingManagementContext();
                post.Status = CommonEnums.POST_STATUS.CLOSE;
                post.UpdatedAt = DateTime.Now;
                context.Posts.Update(post);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task OpenPost(Post post)
        {
            try
            {
                using var context = new OnlineJobSeekingManagementContext();
                post.Status = CommonEnums.POST_STATUS.AVAILABLE;
                post.UpdatedAt = DateTime.Now;
                context.Posts.Update(post);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public OnlineJobSeekingManagementContext GetMajorCt()
        {
            OnlineJobSeekingManagementContext mc = new OnlineJobSeekingManagementContext();
            return mc;
        }
    }
}
