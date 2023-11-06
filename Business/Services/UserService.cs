using Business.IServices;
using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class UserService:IUserService
    {
        private readonly DataContext dbContext;
        public UserService(DataContext dataContext)
        {
            dbContext = dataContext;
        }
        public int AddUser(User user)
        {
            var exist = dbContext.Users.FirstOrDefault(x=>x.Username==user.Username);
            if (exist != null)
            {
                return -1;
            }
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
            return 0;
        }
        public User Authentication(string UserName, string password)
        {
            var user = dbContext.Users.Where(x=>x.Username == UserName & x.Password == password).FirstOrDefault();
            return user;
        }
        public void DeleteUser(int Id)
        {
            var exist=dbContext.Users.Find(Id);
            if (exist!=null)
            {
                dbContext.Users.Remove(exist); 
                dbContext.SaveChanges();
            }
        }

        public IQueryable<User> GetAllUsers()
        {
            var users = dbContext.Users;
            return users;
        }

        public User GetUserById(int Id)
        {
            var user = dbContext.Users.Find(Id);
            return user;
        }

        public void UpdateUser(User user)
        {
            //unitofWork.cus
            dbContext.Users.Update(user); 
            dbContext.SaveChanges();
        }
    }
}
