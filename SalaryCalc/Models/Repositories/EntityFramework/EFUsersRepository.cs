using Microsoft.EntityFrameworkCore;
using SalaryCalc.Models.Entities;
using SalaryCalc.Models.Repositories.Interfaces;
using System;
using System.Linq;

namespace SalaryCalc.Models.Repositories.EntityFramework
{
    public class EFUsersRepository : IUsersRepository
    {
        private readonly AppDbContext context;

        public EFUsersRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IQueryable<User> GetUsers()
        {
            return context.Users;
        }

        public User GetUserById(string Id)
        {
            context.Users.Where(u => u.Id == Id)
                .Include(u => u.Position)
                .FirstOrDefault();    // подгружаем должность из базы данных в контекст
            return context.Users.FirstOrDefault(u => u.Id == Id);
        }

        public User GetUserByUserName(string userName)
        {
            return context.Users.FirstOrDefault(u => u.UserName == userName);
        }


        public IQueryable<Salary> GetSalaries(string id)
        {
            return context.Salaries.Where(s => s.UserId == id);
        }

        public Salary GetSalaryByDate(string userId, DateTime date)
        {
            return context.Salaries.Where(s => s.UserId == userId & s.Date == date).FirstOrDefault();
        }

        public void SaveUser(User model)
        {
            User user = context.Users.FirstOrDefault(u => u.Id == model.Id);
            if (user != null)
            {
                user.UserName = model.UserName;
                user.Fullname = model.Fullname;
                user.Position = model.Position;
                context.SaveChanges();
            }
        }

        public void DeleteUser(string id)
        {
            User user = context.Users.Where(u => u.Id == id).FirstOrDefault();
            context.Entry(user).State = EntityState.Deleted;
            //context.Users.Remove(user);
            context.SaveChanges();
        }
    }
}
