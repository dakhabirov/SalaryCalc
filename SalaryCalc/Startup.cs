using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SalaryCalc.Middlewares;
using SalaryCalc.Models;
using SalaryCalc.Models.Entities;
using SalaryCalc.Models.Repositories.EntityFramework;
using SalaryCalc.Models.Repositories.Interfaces;
using SalaryCalc.Service;
using System.Security.Claims;

namespace SalaryCalc
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            // Подключаем конфиг из appsettings.json.
            Configuration.Bind("Project", new Config());

            // Добавляем поддержку контроллеров и представлений (MVC).
            services.AddControllersWithViews();

            // Подключаем нужный функционал приложения в качестве сервисов (через внедрение зависимостей).
            services.AddTransient<IUsersRepository, EFUsersRepository>();   // Связываем интерфейс с его реализацией.
            services.AddTransient<IPositionsRepository, EFPositionsRepository>();
            services.AddTransient<IProductsRepository, EFProductsRepository>();
            services.AddTransient<ICategoriesRepository, EFCategoriesRepository>();
            services.AddTransient<ISalesRepository, EFSalesRepository>();
            services.AddTransient<ISaleProductsRepository, EFSaleProductsRepository>();
            services.AddTransient<DataManager>();

            // Подключаем контекст БД.
            services.AddDbContext<AppDbContext>(c => c.UseSqlServer(Config.ConnectionString));

            // Настраиваем identity в систему.
            services.AddIdentity<User, IdentityRole>(config =>
            {
                // Конфигурируем требования к паролю.
                config.Password.RequireDigit = false;
                config.Password.RequireLowercase = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
                config.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<AppDbContext>();  // Устанавливаем тип хранилища, которое будет применяться
                                                        // в Identity для хранения данных. В качестве типа хранилища
                                                        // здесь указывается бд.

            // Настраиваем authentication cookie.
            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "Authentication";  // Нзвание для cookie.
                config.Cookie.HttpOnly = true;  // Cookie недоступна на клиентской стороне.
                config.LoginPath = "/Account/Login";   // Направляем пользователя на страницу аутентификации.
            });

            // Добавляем авторизацию.
            services.AddAuthorization(options =>
            {
                // Создаем политики для ограничения доступов.
                options.AddPolicy("Administrator", builder =>
                {
                    builder.RequireClaim(ClaimTypes.Role, "Administrator");
                });
                options.AddPolicy("Manager", builder =>
                {
                    builder.RequireAssertion(u => u.User.HasClaim(ClaimTypes.Role, "Administrator")
                                                  || u.User.HasClaim(ClaimTypes.Role, "Manager"));
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // В процессе разработки важно видеть подробную информацию об ошибках.
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseStatusCodePages();
            app.UseStaticFiles();   // Подключаем поддержку статических файлов (css, js и др.)

            app.UseRouting();   // Добавляем возможности маршрутизации,
                                // благодаря этому приложение может соотносить запросы с определенными маршрутами.

            // Подключаем обработчик ошибок.
            app.UseMiddleware<ErrorHandlingMiddleware>();
            // Подключаем аутентификацию.
            app.UseAuthentication();
            // Подключаем авторизацию.
            app.UseAuthorization();

            // Устанавливаем адреса, которые будут обрабатываться.
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("admin", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
