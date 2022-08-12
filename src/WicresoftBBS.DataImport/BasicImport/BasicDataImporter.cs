using WicresoftBBS.DataImport.Utils;
using WicresoftBBS.Api.Models;

namespace WicresoftBBS.DataImport.BasicImport
{
    class BasicDataImporter
    {
        private static readonly List<string> _mailPostfixes = new()
        {
            "163.com"
        };

        public static void UserImport()
        {
            var repo = CommonUtils.GetRepo();
            var userNames = new[]
            {
                "Admin",
                "张三",
                "李四",
                "王五",
            };
            var users = new List<User>();
            foreach (var userName in userNames)
            {
                users.Add(ConstructUser(userName));
            }
            _ = repo.ReFillUsers(users).Result;
        }

        private static User ConstructUser(string userName)
        {
            var mailPostfix = _mailPostfixes.OrderBy(item => Guid.NewGuid()).FirstOrDefault();
            return new User
            {
                UserName = userName,
                Password = "1",
                Email = $"{userName}@{mailPostfix}",
                UserType = userName == "Admin" ? 0 : 1,
                CreateTime = DateTime.Now
            };
        }
    }
}
