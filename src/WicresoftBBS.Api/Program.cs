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
            builder.Services.AddScoped<IPostsService, PostsService>();
            builder.Services.AddScoped<IPostTypesService, PostTypesService>();
            builder.Services.AddScoped<IRepliesService, RepliesService>();

#if DEBUG
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Debug", policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });
#endif


            var app = builder.Build();

            app.Urls.Add("https://*:8080");

#if DEBUG
            app.UseCors("Debug");
#else
            app.UseCors();
#endif

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WicresoftBBS.Api V1");
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}