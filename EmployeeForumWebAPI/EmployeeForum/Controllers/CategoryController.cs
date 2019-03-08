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
    public class CategoryController : ControllerBase
    {
        private readonly EmployeeForumContext context;

        public CategoryController(EmployeeForumContext databaseContext)
        {
            context = databaseContext;
        }

        // GET: api/Category
        [HttpGet,Authorize]
        
        public IEnumerable<Category> GetCategories()
        {
            return context.Categories.ToList();
        }

        // GET: api/Category/5
        [HttpGet("{id}"), Authorize]
        public IActionResult GetCategory(int id)
        {
            var category =  context.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // PUT: api/Category/5
        [HttpPut("{id}"), Authorize]
        public IActionResult PutCategory(int id, [FromBody]Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest();
            }

            context.Entry(category).State = EntityState.Modified;

            try
            {
                 context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // POST: api/Category
        [HttpPost,Authorize]
        public  IActionResult PostCategory([FromBody]Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Categories.Add(category);
            context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.CategoryId }, category);
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult<Category>> DeleteCategory(int id)
        {
            var category = await context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            context.Categories.Remove(category);
            await context.SaveChangesAsync();

            return category;
        }

        private bool CategoryExists(int id)
        {
            return context.Categories.Any(e => e.CategoryId == id);
        }
    }
}
