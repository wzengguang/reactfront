using WicresoftBBS.Api.Models;

namespace WicresoftBBS.DataImport.Utils
{
    public static class DataUtils
    {
        private static readonly List<string> _mailPostfixes = new()
        {
            "163.com"
        };


        public static List<User> GetNewUsers(List<Post> posts, IEnumerable<User> users)
        {
            var userSet = users.Select(user => user.UserName).ToHashSet<string>();
            var userMap = new Dictionary<string, User>();
            foreach (var post in posts)
            {
                if (!userSet.Contains(post.Creator.UserName))
                {
                    if (!userMap.ContainsKey(post.Creator.UserName))
                    {
                        DecorateUser(post.Creator);
                        userMap[post.Creator.UserName] = post.Creator;
                    }
                }

                foreach (var replay in post.Replies)
                {
                    if (!userSet.Contains(replay.Creator.UserName))
                    {
                        if (!userMap.ContainsKey(replay.Creator.UserName))
                        {
                            DecorateUser(replay.Creator);
                            userMap[replay.Creator.UserName] = replay.Creator;
                        }
                    }
                }
            }
            return userMap.Values.ToList();
        }

        public static List<PostType> GetNewPostTypes(List<Post> posts, IEnumerable<PostType> postTypes)
        {
            var postTypeSet = postTypes.Select(postTypes => postTypes.Type).ToHashSet<string>();
            var postTypeMap = new Dictionary<string, PostType>();
            foreach (var post in posts)
            {
                if (!postTypeSet.Contains(post.PostType.Type))
                {
                    if (!postTypeMap.ContainsKey(post.PostType.Type))
                    {
                        postTypeMap[post.PostType.Type] = post.PostType;
                    }
                }
            }
            return postTypeMap.Values.ToList();
        }

        public static void RedirectUser(List<Post> posts, IEnumerable<User> users)
        {
            var userMap = new Dictionary<string, User>();
            foreach (var user in users)
            {
                if (!userMap.ContainsKey(user.UserName))
                {
                    userMap[user.UserName] = user;
                }
            }


            foreach (var post in posts)
            {
                post.CreatorId = userMap[post.Creator.UserName].Id;
                post.Creator = null;
                foreach (var replay in post.Replies)
                {
                    replay.CreatorId = userMap[replay.Creator.UserName].Id;
                    replay.Creator = null;
                }

            }
        }

        public static void RedirectPostType(List<Post> posts, IEnumerable<PostType> postTypes)
        {
            var postTypeMap = new Dictionary<string, PostType>();
            foreach (var postType in postTypes)
            {
                if (!postTypeMap.ContainsKey(postType.Type))
                {
                    postTypeMap[postType.Type] = postType;
                }
            }

            foreach (var post in posts)
            {
                post.PostTypeId = postTypeMap[post.PostType.Type].Id;
                post.PostType = null;
            }
        }

        public static void DecorateUser(User user)
        {
            if (user.Email == null)
            {
                var mailPostfix = _mailPostfixes.OrderBy(item => Guid.NewGuid()).FirstOrDefault();
                user.Email = $"{user.UserName}@{mailPostfix}";
            }
            if (user.Password == null)
            {
                user.Password = "1";
            }
            if (user.UserType == 0)
            {
                user.UserType = 2;
            }
        }

        public static void SetFloors(Post post)
        {
            for (var i = 0; i < post.Replies.Count; i++)
            {
                post.Replies[i].FloorId = i + 1;
            }
        }

    }
}