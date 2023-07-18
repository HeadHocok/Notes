using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Reflection;
using Notes.Application;
using Notes.Application.Common.Mappings;
using Notes.Application.Interfaces;
using Notes.Persistence;
using Notes.WebApi.Middleware;

namespace Notes.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                try
                {
                    var context = serviceProvider.GetRequiredService<NotesDbContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception exception)
                {
                    // Обработка исключения при инициализации базы данных
                    Console.WriteLine(exception);
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices((hostContext, services) =>
                    {
                        var configuration = hostContext.Configuration;

                        services.AddAutoMapper(config =>
                        {
                            config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
                            config.AddProfile(new AssemblyMappingProfile(typeof(INotesDbContext).Assembly));
                        });

                        services.AddApplication();
                        services.AddPersistence(configuration);
                        services.AddControllers();

                        services.AddCors(options =>
                        {
                            options.AddPolicy("AllowAll", policy =>
                            {
                                policy.AllowAnyHeader();
                                policy.AllowAnyMethod();
                                policy.AllowAnyOrigin();
                            });
                        });

                        services.AddAuthentication(config => //Прохождение аутентификации в приложении
                        {
                            config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                            config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                        })
                        .AddJwtBearer("Bearer", options =>
                        {
                            options.Authority = "https://localhost:7049/"; //Наш URL сервера
                            options.Audience = "NotesWebAPI";
                            options.RequireHttpsMetadata = false;
                        });
                    });

                    webBuilder.Configure(app =>
                    {
                        var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();
                        if (env.IsDevelopment())
                        {
                            app.UseDeveloperExceptionPage();
                        }

                        app.UseCustomExceptionHandler();
                        app.UseRouting();
                        app.UseHttpsRedirection();
                        app.UseCors("AllowAll");
                        app.UseAuthentication();
                        app.UseAuthorization();

                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapControllers();
                        });
                    });
                });
    }
}