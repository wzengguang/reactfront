using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WicresoftBBS.Api.Models;
using WicresoftBBS.Api.Services;

namespace WicresoftBBS.DataImport.Utils
{
    public static class CommonUtils
    {
        private const string API_CONFIGURATION_JSON_NAME = "appsettings.json";
        private const string DBNAME = "WicresoftBBSDatabaseTest";

        private static readonly string _binFolder = Path.GetDirectoryName(typeof(Program).Assembly.Location); 
        public static IConfigurationRoot GetApiConfiguration()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(_binFolder, API_CONFIGURATION_JSON_NAME));

            return builder.Build();
        }

        public static string GetDBConnectionString()
        {
            var apiConfiguration = GetApiConfiguration();
            return apiConfiguration.GetConnectionString(DBNAME);
        }

        public static BBSDbContext GetContext()
        {
            return new BBSDbContext(new DbContextOptionsBuilder<BBSDbContext>()
                .UseSqlServer(GetDBConnectionString())
                .Options);
        }

        public static IBBSRepo GetRepo()
        {
            return new BBSRepo(GetContext());
        }
    }
}
