using Microsoft.EntityFrameworkCore;
using SalaryCalc.Models.Repositories.Interfaces;
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
            return context.Users.FirstOrDefault(u => u.Id == Id);
        }

        public User GetUserByUserName(string userName)
        {
            return context.Users.FirstOrDefault(u => u.UserName == userName);
        }

        public void SaveUser(User user)
        {
            var dbUser = context.Users.Where(u => u.Id == user.Id).FirstOrDefault();
            if (dbUser != null)
            {
                context.Entry(dbUser).State = EntityState.Detached;
                context.Entry(user).State = EntityState.Modified;
            }
            else
                context.Entry(user).State = EntityState.Added;

            //context.Entry(user).State = user.Id == default ? EntityState.Added : EntityState.Modified;
            context.SaveChanges();
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
