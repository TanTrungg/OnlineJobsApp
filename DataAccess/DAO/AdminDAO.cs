using BusinessObject.Models;
using DataAccess.Commons;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class AdminDAO
    {
        private static AdminDAO instance = null;
        private static readonly object instanceLock = new object();

        public static AdminDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new AdminDAO();
                    }
                    return instance;
                }
            }
        }

        public async Task<User> GetUserById(int id)
        {
            User c = new User();
            try
            {
                using var context = new OnlineJobSeekingManagementContext();
                c = await context.Users
                    .Where(c => c.Id == id)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return c;
        }

        public async Task<Company> GetComById(int id)
        {
            Company c = new Company();
            try
            {
                using var context = new OnlineJobSeekingManagementContext();
                c = await context.Companies
                    .Where(c => c.Id == id)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return c;
        }

        public async Task<Post> GetPostById(int id)
        {
            Post c = new Post();
            try
            {
                using var context = new OnlineJobSeekingManagementContext();
                c = await context.Posts
                    .Where(c => c.Id == id)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return c;
        }
        public async Task<Major> GetMajorById(int id)
        {
            Major c = new Major();
            try
            {
                using var context = new OnlineJobSeekingManagementContext();
                c = await context.Majors
                    .Where(c => c.Id == id)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return c;
        }
        public async Task<Major> GetMajorByName(string name)
        {
            Major c = new Major();
            try
            {
                using var context = new OnlineJobSeekingManagementContext();
                c = await context.Majors
                    .Where(c => c.MajorName == name)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return c;
        }
        public async Task<List<User>> GetUser()
        {
            List<User> user;
            try
            {
                using var context = new OnlineJobSeekingManagementContext();
                user = await context.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }
        public async Task<List<Major>> GetMajor()
        {
            List<Major> user;
            try
            {
                using var context = new OnlineJobSeekingManagementContext();
                user = await context.Majors.Where(m => m.IsDeleted == false).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }
        public async void AddMajor(Major member)
        {
            try
            {
                Major existMember = await GetMajorById(member.Id);
                if (existMember != null)
                {
                    throw new Exception("The member already exist");
                }
                else
                {
                    var storeDB = new OnlineJobSeekingManagementContext();
                    member.CreatedAt = DateTime.Now;
                    member.IsDeleted = false;
                    await storeDB.Majors.AddAsync(member);
                    storeDB.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<Post>> GetPost()
        {
            List<Post> user;
            try
            {
                using var context = new OnlineJobSeekingManagementContext();
                user = await context.Posts.Include(c => c.Major).Where(p => p.IsReported == true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }

        public async Task<List<Company>> GetMember()
        {
            List<Company> user;
            try
            {
                using var context = new OnlineJobSeekingManagementContext();
                user = await context.Companies.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }

        public async void UpdateMajor(Major user)
            {
            try
            {
                Major newUser = await GetMajorById(user.Id);
                if (newUser != null)
                {
                    using var context = new OnlineJobSeekingManagementContext();
                    newUser.MajorName = user.MajorName;
                    newUser.UpdatedAt = DateTime.Now;
                    context.Majors.Update(newUser);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        public async Task RemoveMajor(int id)
        {
            try
            {
                Major existMember = await GetMajorById(id);
                if (existMember != null)
                {
                    var storeDB = new OnlineJobSeekingManagementContext();
                    existMember.IsDeleted = true;
                    existMember.UpdatedAt = DateTime.Now;
                    storeDB.Majors.Update(existMember);
                    await storeDB.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Error");
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task DeActiveUser(int id)
        {
            try
            {
                User newUser = await GetUserById(id);
                if(newUser != null)
                {   
                    using var context = new OnlineJobSeekingManagementContext();
                    newUser.IsDeleted = !newUser.IsDeleted;
                    newUser.UpdatedAt = DateTime.Now;
                    context.Users.Update(newUser);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task DeActiveCompany(int id)
        {
            try
            {
                Company newUser = await GetComById(id);
                if (newUser != null)
                {
                    using var context = new OnlineJobSeekingManagementContext();
                    newUser.IsDeleted = !newUser.IsDeleted;
                    newUser.UpdatedAt = DateTime.Now;
                    context.Companies.Update(newUser);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task ReportPost(int id)
        {
            try
            {
                Post posts = await GetPostById(id);
                if (posts != null)
                {
                    using var context = new OnlineJobSeekingManagementContext();
                    posts.Status = CommonEnums.POST_STATUS.AVAILABLE;
                    posts.IsReported = true;
                    posts.UpdatedAt = DateTime.Now;
                    context.Posts.Update(posts);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task BanPost(int id)
        {
            try
            {
                Post posts = await GetPostById(id);
                if (posts != null)
                {
                    using var context = new OnlineJobSeekingManagementContext();
                    posts.Status = CommonEnums.POST_STATUS.REPORT;
                    posts.IsReported = true;
                    posts.UpdatedAt = DateTime.Now;
                    context.Posts.Update(posts);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task ActivePost(int id)
        {
            try
            {
                Post posts = await GetPostById(id);
                if (posts != null)
                {
                    using var context = new OnlineJobSeekingManagementContext();
                    posts.Status = CommonEnums.POST_STATUS.AVAILABLE;
                    posts.IsReported = null;
                    posts.UpdatedAt = DateTime.Now;
                    context.Posts.Update(posts);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
