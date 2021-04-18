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

        public Salary GetSalaryByDate(string userId, ushort year, byte month)
        {
            return context.Salaries.Where(s => s.UserId == userId & s.Year == year & s.Month == month).FirstOrDefault();
        }

        public void SaveSalary(string userId, double sum, ushort year, byte month)
        {
            Salary salary = context.Salaries.Where(s => s.UserId == userId & s.Year == year & s.Month == month).FirstOrDefault();
            if (salary == default)
            {
                CreateSalary(userId, sum, year, month);
            }
            else
            {
                UpdateSalary(salary, sum, year, month);
            }
        }

        public void CreateSalary(string userId, double sum, ushort year, byte month)
        {
            Salary salary = new Salary
            {
                UserId = userId,
                Sum = sum,
                Year = year,
                Month = month
            };
            context.Salaries.AddAsync(salary);
            context.SaveChanges();
        }

        public void UpdateSalary(Salary salary, double sum, ushort year, byte month)
        {
            salary.Sum += sum;
            salary.Year = year;
            salary.Month = month;
            context.SaveChanges();
        }

        public void DeleteSalary(Guid id)
        {
            context.Salaries.Remove(new Salary() { Id = id });
            context.SaveChangesAsync();
        }
    }
}
