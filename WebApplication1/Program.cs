using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Repositories;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // TempData provider ¼¼ÆÃ
            builder.Services
                .AddRazorPages()
                .AddSessionStateTempDataProvider();

            // Add services to the container.
            builder.Services
                .AddControllersWithViews()
                .AddSessionStateTempDataProvider();

            builder.Services.AddSession();

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