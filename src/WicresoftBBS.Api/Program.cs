using Microsoft.EntityFrameworkCore;
using WicresoftBBS.Api.Models;
using WicresoftBBS.Api.Services;

namespace WicresoftBBS.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //WicresoftForumContext
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<BBSDbContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("WicresoftBBSDatabaseTest")));

            builder.Services.AddScoped<IUsersService, UsersService>();
            var app = builder.Build();

            app.Urls.Add("https://*:8080");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}