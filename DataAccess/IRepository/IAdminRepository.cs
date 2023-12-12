using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IAdminRepository
    {
        Task<List<Post>> GetPost();
        Task<List<User>> GetUsers();
        Task<List<Major>> GetMajor();
        Task<Major> GetMajorById(int id);
        void AddMajor(Major major);
        void UpdateMajor(Major major);
        Task DeleteMajor(int id);
        Task<List<Company>> GetMember();
        Task DeActiveUser(int id);
        Task DeActiveCompany(int id);
        Task ReportPost(int id);
        Task BanPost(int id);
        Task ActivePost(int id);
        Task<Major> GetMajorByName(string name);

    }
}
