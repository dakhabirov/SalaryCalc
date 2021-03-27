using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SalaryCalc.Models.Entities;
using System;

namespace SalaryCalc.Models
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<Salary> Salaries { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<SaleProduct> SaleProducts { get; set; }

        // заполняем базу данных первичными данными
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // устанавливаем связь многие-ко-многие через промежуточную таблицу
            builder.Entity<SaleProduct>()
            .HasKey(sp => new { sp.SaleId, sp.ProductId });

            builder.Entity<SaleProduct>()
                .HasOne(sp => sp.Sale)
                .WithMany(sp => sp.SaleProducts)
                .HasForeignKey(sp => sp.SaleId);

            builder.Entity<SaleProduct>()
                .HasOne(sp => sp.Product)
                .WithMany(sp => sp.SaleProducts)
                .HasForeignKey(sp => sp.ProductId);




            base.OnModelCreating(builder);

            // генерируем гуиды (уникальные идентификаторы)
            Guid adminRoleGuid = Guid.NewGuid();
            Guid managerRoleGuid = Guid.NewGuid();
            Guid adminGuid = Guid.NewGuid();
            Guid managerGuid = Guid.NewGuid();
            Guid positionGuid = Guid.NewGuid();

            // создаем должность (для теста)
            Position position = new Position
            {
                Id = positionGuid,
                Name = "Должность",
                HourlyRate = 100
            };
            builder.Entity<Position>().HasData(position);

            // создаем роли
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = adminRoleGuid.ToString(),
                Name = "Administrator",
            });
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = managerRoleGuid.ToString(),
                Name = "Manager",
            });


            // создаем пользователей
            builder.Entity<User>().HasData(new User
            {
                Id = adminGuid.ToString(),
                UserName = "sa",
                NormalizedUserName = "SA",
                PasswordHash = new PasswordHasher<User>().HashPassword(null, "123qwe"),
                Fullname = "Fullname",
                PositionId = positionGuid
            });
            builder.Entity<User>().HasData(new User
            {
                Id = managerGuid.ToString(),
                UserName = "manager",
                NormalizedUserName = "MANAGER",
                PasswordHash = new PasswordHasher<User>().HashPassword(null, "123qwe"),
                Fullname = "Fullname",
                PositionId = positionGuid
            });

            // назначаем созданным пользователям роль
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = adminRoleGuid.ToString(),
                UserId = adminGuid.ToString()
            });
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = managerRoleGuid.ToString(),
                UserId = managerGuid.ToString()
            });
        }
    }
}