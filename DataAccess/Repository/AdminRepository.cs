using BusinessObject.Models;
using DataAccess.DAO;
using DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class AdminRepository : IAdminRepository
    {
        public Task<List<Post>> GetPost() => AdminDAO.Instance.GetPost();
        public Task<User> GetUserById(int Id) => AdminDAO.Instance.GetUserById(Id);
        public Task<List<User>> GetUsers() => AdminDAO.Instance.GetUser();
        public Task<List<Company>> GetMember() => AdminDAO.Instance.GetMember();
        public Task DeActiveUser(int id) => AdminDAO.Instance.DeActiveUser(id);
        public Task DeActiveCompany(int id) => AdminDAO.Instance.DeActiveCompany(id);
        public Task ReportPost(int id) => AdminDAO.Instance.ReportPost(id);
        public Task BanPost(int id) => AdminDAO.Instance.BanPost(id);
        public Task ActivePost(int id) => AdminDAO.Instance.ActivePost(id);
        
        public Task<List<Major>> GetMajor() => AdminDAO.Instance.GetMajor();
        public void AddMajor(Major major) => AdminDAO.Instance.AddMajor(major);
        public void UpdateMajor(Major major) => AdminDAO.Instance.UpdateMajor(major);
        public Task DeleteMajor(int id) => AdminDAO.Instance.RemoveMajor(id);
        public Task<Major> GetMajorById(int id) => AdminDAO.Instance.GetMajorById(id);
        public Task<Major> GetMajorByName(string name) => AdminDAO.Instance.GetMajorByName(name);
    }
}
