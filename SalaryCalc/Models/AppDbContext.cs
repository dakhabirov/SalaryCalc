using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SalaryCalc.Models.Entities;
using System;

namespace SalaryCalc.Models
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            // Создаем БД при первом обращении.
            Database.EnsureCreated();
        }

        /// <summary>
        /// Пользователи.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Должности.
        /// </summary>
        public DbSet<Position> Positions { get; set; }

        /// <summary>
        /// Заработные платы.
        /// </summary>
        public DbSet<Salary> Salaries { get; set; }

        /// <summary>
        /// Продажи.
        /// </summary>
        public DbSet<Sale> Sales { get; set; }

        /// <summary>
        /// Товары.
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Категории.
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Проданные товары.
        /// </summary>
        public DbSet<SaleProduct> SaleProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Устанавливаем связь многие-ко-многие через промежуточную таблицу.
            modelBuilder.Entity<SaleProduct>()
            .HasKey(sp => new { sp.SaleId, sp.ProductId });

            modelBuilder.Entity<SaleProduct>()
                .HasOne(sp => sp.Sale)
                .WithMany(sp => sp.SaleProducts)
                .HasForeignKey(sp => sp.SaleId);

            modelBuilder.Entity<SaleProduct>()
                .HasOne(sp => sp.Product)
                .WithMany(sp => sp.SaleProducts)
                .HasForeignKey(sp => sp.ProductId);


            //
            // Инициализация БД начальными данными.
            //

            // Генерируем уникальные идентификаторы.
            Guid adminRoleGuid = Guid.NewGuid();
            Guid adminGuid = Guid.NewGuid();
            Guid positionGuid = Guid.NewGuid();
            Guid categoryGuid = Guid.NewGuid();

            // Создаем должности.
            modelBuilder.Entity<Position>().HasData(new Position
            {
                Id = positionGuid,
                Name = "Должность",
                HourlyRate = 100
            });

            // Cоздаем роли.
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = adminRoleGuid.ToString(),
                Name = "Administrator",
            });

            // Создаем пользователей.
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = adminGuid.ToString(),
                UserName = "sa",
                NormalizedUserName = "SA",
                PasswordHash = new PasswordHasher<User>().HashPassword(null, "123qwe"),
                Fullname = "Fullname",
                PositionId = positionGuid
            });

            // Назначаем пользователям роль.
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = adminRoleGuid.ToString(),
                UserId = adminGuid.ToString()
            });

            //// Создаем категории.
            //modelBuilder.Entity<Category>().HasData(new Category
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "Товар",
            //    IsFavorite = true
            //});

            //modelBuilder.Entity<Category>().HasData(new Category
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "Услуга",
            //    IsFavorite = true
            //});
        }
    }
}