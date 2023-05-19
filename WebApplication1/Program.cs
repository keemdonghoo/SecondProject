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

            builder.Services
                .AddRazorPages()
                .AddSessionStateTempDataProvider();

            builder.Services
                .AddControllersWithViews()
                .AddSessionStateTempDataProvider();

            builder.Services.AddScoped<IWriteRepository, WriteRepository>();

            builder.Services.AddSession(options => {
                // Session Timeout 설정 
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

            // 이 위치에 세션을 사용하도록 설정합니다.
            app.UseSession();


            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

        
            app.Run();
        }
    }
}