using contactapi.Models;
using Microsoft.EntityFrameworkCore;

namespace contactapi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            if (Environment.GetEnvironmentVariable("SQL_DB") != null)
            {
                builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(Environment.GetEnvironmentVariable("SQL_DB")));
            }
            else
            {
                builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataConnection")));
            }
            builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}