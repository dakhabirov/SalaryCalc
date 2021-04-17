using SalaryCalc.Models.Entities;
using SalaryCalc.Models.Repositories.Interfaces;
using System;
using System.Linq;

namespace SalaryCalc.Models.Repositories.EntityFramework
{
    public class EFSalariesRepository : ISalariesRepository
    {
        private readonly AppDbContext context;

        public EFSalariesRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Salary> GetSalaries(string userId)
        {
            return context.Salaries.Where(s => s.UserId == userId);
        }

        public Salary GetSalaryByDate(DateTime salaryDate)
        {
            return context.Salaries.Where(s => s.Date == salaryDate).FirstOrDefault();
        }

        public void SaveSalary(string userId, double sum, DateTime date)
        {
            Salary salary = context.Salaries.Where(s => s.UserId == userId & s.Date == date).FirstOrDefault();
            if (salary == default)
            {
                CreateSalary(userId, sum, date);
            }
            else
            {
                UpdateSalary(salary, sum, date);
            }
        }

        public void CreateSalary(string userId, double sum, DateTime date)
        {
            Salary salary = new Salary
            {
                UserId = userId,
                Sum = sum,
                Date = date
            };
            context.Salaries.AddAsync(salary);
            context.SaveChanges();
        }

        public void UpdateSalary(Salary salary, double sum, DateTime date)
        {
            salary.Sum += sum;
            salary.Date = date;
            context.SaveChanges();
        }

        public void DeleteSalary(Guid id)
        {
            context.Salaries.Remove(new Salary() { Id = id });
            context.SaveChangesAsync();
        }
    }
}
