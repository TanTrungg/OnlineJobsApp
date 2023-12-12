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
    public class UserRepository : IUserRepository
    {
        private UserDAO _dao;

        public UserRepository()
        {
            _dao = new UserDAO();
        }

        public void addNewUser(User user) => _dao.addNewUser(user);

        public User checkUserLogin(string email, string password) => _dao.checkUserLogin(email, password);

        public User getUserByEmail(string email) => _dao.getUserByEmail(email);

        public User getUserByID(int id) => _dao.getUserByID(id);

        public void updatePassword(User user) => _dao.updatePassword(user);

        public void updateUser(User user) => _dao.updateUser(user);
    }
}
