using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IUserService
    {
        int AddUser(User user);
        void UpdateUser(User user);
        User GetUserById(int Id);
        IQueryable<User> GetAllUsers();
        void DeleteUser(int Id);
        User Authentication(string UserName, string password);
    }
}
