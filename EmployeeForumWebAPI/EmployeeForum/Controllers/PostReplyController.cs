using EmployeeForum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeForum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostReplyController : ControllerBase
    {
        private readonly EmployeeForumContext context;

        public PostReplyController(EmployeeForumContext databaseContext)
        {
            context = databaseContext;
        }

        // GET: api/PostReply
        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<PostReply>>> GetPostReplies()
        {
            return await context.PostReplies.ToListAsync();
        }

        // GET: api/PostReply/5
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<PostReply>> GetPostReply(int id)
        {
            var postReply = await context.PostReplies.FindAsync(id);

            if (postReply == null)
            {
                return NotFound();
            }

            return postReply;
        }

        // PUT: api/PostReply/5
        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> PutPostReply(int id, PostReply postReply)
        {
            if (id != postReply.PostReplyId)
            {
                return BadRequest();
            }

            context.Entry(postReply).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostReplyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PostReply
        [HttpPost, Authorize]
        public async Task<ActionResult<PostReply>> PostPostReply(PostReply postReply)
        {
            context.PostReplies.Add(postReply);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetPostReply", new { id = postReply.PostReplyId }, postReply);
        }

        // DELETE: api/PostReply/5
        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult<PostReply>> DeletePostReply(int id)
        {
            var postReply = await context.PostReplies.FindAsync(id);
            if (postReply == null)
            {
                return NotFound();
            }

            context.PostReplies.Remove(postReply);
            await context.SaveChangesAsync();

            return postReply;
        }

        private bool PostReplyExists(int id)
        {
            return context.PostReplies.Any(e => e.PostReplyId == id);
        }
    }
}
