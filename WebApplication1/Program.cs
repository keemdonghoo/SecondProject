using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
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
                options.IdleTimeout = TimeSpan.FromMinutes(60);
            });

            builder.Services.AddHttpContextAccessor();


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

			app.UseStaticFiles(new StaticFileOptions
			{
				FileProvider = new PhysicalFileProvider(
				 Path.Combine(builder.Environment.ContentRootPath, "MyFiles"))
			 ,
				RequestPath = "/appfiles"

				// URL : /appfiles..
				// 경로 : {project contetn} / MyFiles.. 
			});

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