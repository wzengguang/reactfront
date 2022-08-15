using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WicresoftBBS.Api.Models;
using WicresoftBBS.Api.Services;

namespace WicresoftBBS.Api.Controllers
{
    [Route("api/Replies")]
    [ApiController]
    public class RepliesController : ControllerBase
    {
        private readonly IRepliesService _service;

        public RepliesController(IRepliesService context)
        {
            _service = context;
        }

        // GET: api/Replies
        [HttpGet("GetRepliesByPostId")]
        public async Task<ActionResult<RepliesSummary>> GetRepliesByPostId(int postId, int pageIndex=0,int pageSize=10)
        {
            var replies = await _service.GetRepliesByPostId(postId, pageIndex, pageSize);

            return replies;
        }

        // GET: api/Replies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReplyDTO>> GetReply(int id)
        {
            var reply = await _service.GetReplyById(id);

            if (reply == null)
            {
                return NotFound();
            }

            return reply;
        }

        // PUT: api/Replies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReply(int id, ReplyDTO replyDto)
        {
            if (id != replyDto.Id)
            {
                return BadRequest();
            }

            if (!_service.ReplyExists(id))
            {
                return NotFound();
            }

            try
            {
                await _service.UpdateReply(replyDto);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // POST: api/Replies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReplyDTO>> CreateReply(ReplyDTO replyDto)
        {
            var reply = await _service.CreateReply(replyDto);

            return CreatedAtAction(nameof(GetReply), new { id = reply.Id }, reply);
        }

        // DELETE: api/Replies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReply(int id)
        {
            if (!_service.ReplyExists(id))
            {
                return NotFound();
            }

            await _service.DeleteReply(id);

            return NoContent();
        }
    }
}
