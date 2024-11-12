using Api.Repositories;
using Api.Repositories.Mocks;
using Api.Services;
using Microsoft.OpenApi.Models;

namespace Api.Main
{

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container. 
            builder.Services.AddSingleton<IMockEmployeeRepo, MockEmployeeRepo>();
            builder.Services.AddSingleton<IMockDependentRepo, MockDependentRepo>();
            builder.Services.AddTransient<IEmployeeService, EmployeeService>();
            builder.Services.AddTransient<IDependentService, DependentService>();
            builder.Services.AddTransient<IPaycheckService, PaycheckService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Employee Benefit Cost Calculation Api",
                    Description = "Api to support employee benefit cost calculations"
                });
            });

            var allowLocalhost = "allow localhost";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(allowLocalhost,
                    policy => { policy.WithOrigins("http://localhost:3000", "http://localhost"); });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(allowLocalhost);
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();

        }
    }
}