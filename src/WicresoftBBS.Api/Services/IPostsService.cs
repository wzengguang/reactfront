﻿using WicresoftBBS.Api.Models;

namespace WicresoftBBS.Api.Services
{
    public interface IPostsService
    {
        Task<IEnumerable<PostDTO>> GetPosts(string searchTerm, int pageIndex, int pageSize, ulong? time, string timeType, string sortBy = "ReplyTime", string sortType = "desc");
        Task<PostDTO> GetPostById(int id);
        Task UpdatePost(PostDTO postDto);
        Task<PostDTO> CreatePost(PostDTO postDto);
        Task DeletePost(int id);
        bool PostExists(int id);
    }
}
