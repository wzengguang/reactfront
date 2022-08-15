using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WicresoftBBS.Api.Models;
using WicresoftBBS.Api.Services;

namespace WicresoftBBS.Api.Controllers
{
    [Route("api/PostTypes")]
    [ApiController]
    public class PostTypesController : ControllerBase
    {
        private readonly IPostTypesService _service;

        public PostTypesController(IPostTypesService service)
        {
            _service = service;
        }

        // GET: api/PostTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostType>>> GetPostTypes()
        {
            var postTypes = await _service.GetPostTypes();
            return postTypes.ToList();
        }

        // GET: api/PostTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PostType>> GetPostType(int id)
        {
            var postType = await _service.GetPostTypeById(id);

            if (postType == null)
            {
                return NotFound();
            }

            return postType;
        }

        // PUT: api/PostTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePostType(int id, PostType postType)
        {
            if (id != postType.Id)
            {
                return BadRequest();
            }

            if (!_service.PostTypeExists(postType.Id))
            {
                return NotFound();
            }

            try
            {
                await _service.UpdatePostType(postType);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // POST: api/PostTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PostType>> CreatePostType(PostType postType)
        {
            await _service.CreatePostType(postType);

            return CreatedAtAction(nameof(GetPostType), new { id = postType.Id }, postType);
        }

        // DELETE: api/PostTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostType(int id)
        {
            if (!_service.PostTypeExists(id))
            {
                return NotFound();
            }

            await _service.DeletePostType(id);

            return NoContent();
        }
    }
}
