using Microsoft.EntityFrameworkCore;
using SalaryCalc.Models.Repositories.Interfaces;
using System;
using System.Linq;

namespace SalaryCalc.Models.Repositories.EntityFramework
{
    public class EFPositionsRepository : IPositionsRepository
    {
        private readonly AppDbContext context;

        public EFPositionsRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Position> GetPositions()
        {
            return context.Positions;
        }

        public Position GetPositionById(Guid Id)
        {
            return context.Positions.FirstOrDefault(p => p.Id == Id);
        }

        public Position GetPositionByName(string Name)
        {
            return context.Positions.FirstOrDefault(p => p.Name == Name);
        }

        public void SavePosition(Position position)
        {
            context.Entry(position).State = position.Id == default ? EntityState.Added : EntityState.Modified;
            context.SaveChanges();
        }

        public void DeletePosition(Guid id)
        {
            context.Positions.Remove(new Position() { Id = id });
            context.SaveChanges();
        }
    }
}
