using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SalaryCalc.Models.Entities;
using SalaryCalc.Models.Repositories.Interfaces;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SalaryCalc.Models.Repositories.EntityFramework
{
    public class EFUsersRepository : IUsersRepository
    {
        private readonly AppDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;

        public EFUsersRepository(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentUserId()
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return userId;
        }

        public IQueryable<User> GetUsers()
        {
            return context.Users;
        }

        public User GetUserById(string Id)
        {
            context.Users.Where(u => u.Id == Id)
                            .Include(p => p.Position)
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

        public Salary GetSalaryByDate(string userId, ushort year, byte month)
        {
            return context.Salaries.Where(s => s.UserId == userId & s.Year == year & s.Month == month).FirstOrDefault();
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
