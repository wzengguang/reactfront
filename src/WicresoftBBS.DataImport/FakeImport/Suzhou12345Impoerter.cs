using WicresoftBBS.DataImport.Utils;
using WicresoftBBS.Api.Models;
using Newtonsoft.Json;

namespace WicresoftBBS.DataImport.BasicImport
{
    class Suzhou12345Impoerter
    {
        public static void DataImport()
        {
            var file = Path.Combine(CommonUtils.GetDataFolderPath(), "Suzhou12345", "data.json");
            var posts = File.ReadAllLines(file).Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line => JsonConvert.DeserializeObject<Post>(line)).ToList();
            var userServices = CommonUtils.GetUsersService();
            var postsService = CommonUtils.GetPostsService();
            var postTypesService = CommonUtils.GetPostTypesService();
            var users = userServices.GetAllUsers().Result;
            var postTypes = postTypesService.GetPostTypes().Result;


            _ = userServices.AddUsers(DataUtils.GetNewUsers(posts, users)).Result;
            _ = postTypesService.AddPostTypes(DataUtils.GetNewPostTypes(posts, postTypes)).Result;

            users = userServices.GetAllUsers().Result;
            postTypes = postTypesService.GetPostTypes().Result;
            DataUtils.RedirectUser(posts, users);
            DataUtils.RedirectPostType(posts, postTypes);
            foreach (var post in posts)
            {
                DataUtils.SetFloors(post);
            }
            _ = postsService.AddPosts(posts).Result;
        }
    }
}
