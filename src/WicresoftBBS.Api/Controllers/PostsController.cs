using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WicresoftBBS.Api.Models;
using WicresoftBBS.Api.Services;

namespace WicresoftBBS.Api.Controllers
{
    [Route("api/Posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostsService _service;

        public PostsController(IPostsService service)
        {
            _service = service;
        }

        // GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<PostsSummary>> GetPosts(string searchTerm, int pageIndex, int pageSize, ulong? time, string timeType = "ago", string sortBy = "ReplyTime", string sortType = "desc")
        {
            var postsSummary = await _service.GetPosts(searchTerm, pageIndex, pageSize, time, timeType, sortBy, sortType);
            return postsSummary;
        }

        [HttpGet("GetPostsByUserId")]
        public async Task<ActionResult<PostsSummary>> GetPostsByUserId(int userId, int pageIndex = 0, int pageSize = 10)
        {
            var postsSummary = await _service.GetPostsByUserId(userId, pageIndex, pageSize);
            return postsSummary;
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PostDTO>> GetPost(int id)
        {
            var post = await _service.GetPostById(id);

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }

        // PUT: api/Posts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, PostDTO postDto)
        {
            if (id != postDto.Id)
            {
                return BadRequest();
            }

            if (!_service.PostExists(id))
            {
                return NotFound();
            }

            try
            {
                await _service.UpdatePost(postDto);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // POST: api/Posts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PostDTO>> CreatePost(PostDTO postDto)
        {
            var post = await _service.CreatePost(postDto);

            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_service.PostExists(id))
            {
                return NotFound();
            }

            await _service.DeletePost(id);

            return NoContent();
        }
    }
}
