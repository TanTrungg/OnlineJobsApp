using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IUserRepository
    {
        User checkUserLogin(string email, string password);
        User getUserByEmail(string email);
        User getUserByID(int id);
        void updateUser(User user);
        void addNewUser(User user);
        void updatePassword(User user);
    }
}
