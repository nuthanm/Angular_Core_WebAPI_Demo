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
    public class PostController : ControllerBase
    {
        private readonly EmployeeForumContext context;

        public PostController(EmployeeForumContext databaseContext)
        {
            context = databaseContext;
        }

        // GET: api/Post/eid/5
        [HttpGet("eid/{employeeId}", Name ="AllPostsByEmployee"), Authorize]
        public IActionResult GetAllPosts(int employeeId)
        {
            var postDetails = (from _post in context.Posts
                               join _employee in context.Employees on _post.EmployeeId equals _employee.EmployeeId
                               join _category in context.Categories on _post.CategoryId equals _category.CategoryId
                               join _postReply in context.PostReplies on _post.PostId equals _postReply.PostId into repliedPosts
                               from _repliedPosts in repliedPosts.DefaultIfEmpty()
                               where _post.EmployeeId == employeeId
                               select new
                               {
                                   _post.PostId,
                                   _category.CategoryName,
                                   _employee.FirstName,
                                   _post.Title,
                                   _post.Description,
                                   _post.CreatedDate
                               }).OrderByDescending(date => date.CreatedDate).ToList();
            return Ok(postDetails);
        }

        [HttpGet]
        public IEnumerable<Post> GetAllPosts()
        {
            return context.Posts.ToList();
        }
        // GET: api/Post/5
        [HttpGet("{postId}"), Authorize]
        public IActionResult GetSelectedPost(int postId)
        {
            var postDetails = (from _post in context.Posts                               
                               join _employee in context.Employees on _post.EmployeeId equals _employee.EmployeeId
                               join _category in context.Categories on _post.CategoryId equals _category.CategoryId
                               join _postReply in context.PostReplies on _post.PostId equals _postReply.PostId into repliedPosts
                               from _repliedPosts in repliedPosts.DefaultIfEmpty()
                               where _post.PostId == postId
                               select new
                               {
                                   _post.PostId,
                                   _category.CategoryName,
                                   _employee.FirstName,
                                   _post.Title,
                                   _post.Description,
                                   _post.CreatedDate                                   
                               }).ToList();

            return Ok(postDetails);            
        }

        // PUT: api/Post/5
        [HttpPut("{postId}"), Authorize]
        public async Task<IActionResult> UpdateAPost(int postId, Post post)
        {
            if (postId != post.PostId)
            {
                return BadRequest();
            }

            context.Entry(post).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(postId))
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

        // POST: api/Post
        [HttpPost]
        public async Task<ActionResult<Post>> CreateANewPost(Post post)
        {
            context.Posts.Add(post);
            await context.SaveChangesAsync();

            return Ok(new { message = Properties.Resources.PostCreatedSuccessMessage });
        }

        // DELETE: api/Post/5
        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult<Post>> DeletePost(int id)
        {
            var post = await context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            context.Posts.Remove(post);
            await context.SaveChangesAsync();

            return post;
        }

        private bool PostExists(int id)
        {
            return context.Posts.Any(e => e.PostId == id);
        }
    }
}
