using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class UserDAO
    {
        private OnlineJobSeekingManagementContext _context;

        public UserDAO()
        {
            _context = new OnlineJobSeekingManagementContext();
        }

        public User checkUserLogin(string email, string password)
        {
            return _context.Users.SingleOrDefault(u => u.Email == email && u.Password == password && u.IsDeleted == false);
        }

        public User getUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public User getUserByID(int id)
        {
            return _context.Users.SingleOrDefault(u => u.Id == id);
        }

        public void updateUser(User user)
        {
            var flag = getUserByID(user.Id);
            if(flag != null)
            {
                flag.FullName = user.FullName;
                flag.Email = user.Email;
                flag.PhoneNumber = user.PhoneNumber;
                flag.Address = user.Address;
                flag.Skill = user.Skill;
                flag.UpdatedAt = user.UpdatedAt;

                _context.SaveChanges();
            }
        }

        public void updatePassword(User user)
        {
            var flag = getUserByID(user.Id);
            if (flag != null)
            {
                flag.Password = user.Password;

                _context.SaveChanges();
            }
        }

        public void addNewUser(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
        }
    }
}
