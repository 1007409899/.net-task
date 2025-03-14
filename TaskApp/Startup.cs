using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TaskApp.Data;
using TaskApp.Repositories;
using TaskApp.Services;

namespace TaskApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("MiPoliticaCors", builder =>
                {
                    builder
                        .AllowAnyOrigin()    // O usa WithOrigins("http://ejemplo.com") para mayor control
                        .AllowAnyMethod()    // Permitir cualquier método HTTP
                        .AllowAnyHeader();   // Permitir cualquier encabezado
                });
            });
            // Configuración de EF Core con base de datos en memoria
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("TaskDb"));

            // Inyección de dependencias para el Repository y Service
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ITaskService, TaskService>();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Task Management API",
                    Version = "v1"
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task Management API v1");
                    c.RoutePrefix = string.Empty;
                });
            }
            // Agrega CORS aquí
            app.UseCors("MiPoliticaCors");
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
