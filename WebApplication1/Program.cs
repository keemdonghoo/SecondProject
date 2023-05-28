using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using System.Net;
using TeamProject.Data;
using TeamProject.Repositories;

namespace TeamProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // TempData provider ����
            builder.Services
                .AddRazorPages()
                .AddSessionStateTempDataProvider();

            // Add services to the container.
            builder.Services
                .AddControllersWithViews()
                .AddSessionStateTempDataProvider();

            builder.Services.AddSession();

            // ������ ���� ����
            builder.Services.AddScoped<ITMDBService, TMDBService>();

            // Here, add HttpClient to the service container
            builder.Services.AddHttpClient<ITMDBService, TMDBService>();

            builder.Services
                .AddRazorPages()
                .AddSessionStateTempDataProvider();

            builder.Services
                .AddControllersWithViews()
                .AddSessionStateTempDataProvider();

            builder.Services.AddScoped<IWriteRepository, WriteRepository>();

            builder.Services.AddSession(options => {
                // Session Timeout ���� 
                options.IdleTimeout = TimeSpan.FromMinutes(60);
            });

            builder.Services.AddHttpContextAccessor();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<MovieDbContext>(options => options.UseSqlServer(
                builder.Configuration.GetConnectionString("MovieDbConnectionString")
                ));

            builder.Services.AddScoped<IUserRepository, UserRepository>();

            // ���񽺿� CORS�� �߰��մϴ�.
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:5000")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                    });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/json";

                        if (contextFeature != null)
                        {
                            var exception = contextFeature.Error;
                            var errorResponse = new
                            {
                                error = exception.Message,
                                stackTrace = exception.StackTrace,
                            };

                            await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
                        }
                    });
                });
            }
            else
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
            });

            app.UseRouting();

            // �̰��� CORS �̵��� �߰��մϴ�.
            app.UseCors();

            // �� ��ġ�� ������ ����ϵ��� �����մϴ�.
            app.UseSession();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
