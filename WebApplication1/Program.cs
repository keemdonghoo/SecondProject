using Microsoft.EntityFrameworkCore;
using TeamProject.Data;
using TeamProject.Repositories;

namespace TeamProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Razor ���� Session ��� TempData ��� ����
            builder.Services
                    .AddRazorPages()
                    .AddSessionStateTempDataProvider();

            // Controller ���� Session ��� TempData ��� ����
            builder.Services
                    .AddControllersWithViews()
                    .AddSessionStateTempDataProvider();


            // 1. ���Ǻ����� ���� ��� ���� �߰�
            builder.Services.AddSession(options => {
                // Session Timeout ���� 
                options.IdleTimeout = TimeSpan.FromMinutes(1);
            });


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<MovieDbContext>(options => options.UseSqlServer(
                builder.Configuration.GetConnectionString("MovieDbConnectionString")
                ));
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}