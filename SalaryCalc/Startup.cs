using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            // подключаем конфиг из appsettings.json
            Configuration.Bind("Project", new Config());

            // добавляем поддержку контроллеров и представлений (MVC)
            services.AddControllersWithViews();

            // подключаем нужный функционал приложения в качестве сервисов (через внедрение зависимостей)
            services.AddTransient<IUsersRepository, EFUsersRepository>();   // связываем интерфейс с его реализацией
            services.AddTransient<IProductsRepository, EFProductsRepository>();
            services.AddTransient<ISalesRepository, EFSalesRepository>();
            services.AddTransient<IPositionsRepository, EFPositionsRepository>();
            services.AddTransient<DataManager>();

            // подключаем контекст БД
            services.AddDbContext<AppDbContext>(c => c.UseSqlServer(Config.ConnectionString));

            // настраиваем identity в систему
            services.AddIdentity<User, IdentityRole>(config =>
            {
                // конфигурируем требования к паролю
                config.Password.RequireDigit = false;
                config.Password.RequireLowercase = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
                config.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<AppDbContext>();  // устанавливаем тип хранилища, которое будет применяться
                                                        // в Identity для хранения данных. В качестве типа хранилища
                                                        // здесь указывается наша база данных

            // настраиваем authentication cookie
            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "Authentication";  // название для cookie
                config.Cookie.HttpOnly = true;  // cookie недоступна на клиентской стороне
                config.LoginPath = "/Account/Login";   // направляем пользователя на страницу аутентификации
                config.AccessDeniedPath = "/Account/AccessDenied"; // если у пользователя нет доступа к странице, направляем его сюда
            });

            // добавляем авторизацию
            services.AddAuthorization(options =>
            {
                // создаем политики для ограничения доступов
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
            // в процессе разработки важно видеть подробную информацию об ошибках
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseStatusCodePages();
            app.UseStaticFiles();   // подключаем поддержку статических файлов (css, js и др.)
            app.UseRouting();

            // используем аутентификацию
            app.UseAuthentication();
            // используем авторизацию
            app.UseAuthorization();

            // регистрируем нужные маршруты
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("admin", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapDefaultControllerRoute();  // маршрут по умолчанию
            });
        }
    }
}
