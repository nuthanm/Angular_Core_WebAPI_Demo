using EmployeeForum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeForum.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeForumContext context;

        public EmployeeController(EmployeeForumContext databaseContext)
        {
            context = databaseContext;
        }

        // GET: api/Employee
        [HttpGet, Authorize]
        public IActionResult GetEmployees()
        {
            return Ok(context.Employees.ToList());
        }

        // GET: api/Employee/5
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await context.Employees.FindAsync(id);
            return employee;
        }

        // PUT: api/Employee/5
        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> UpdateAnEmployee(int id, Employee employee)
        {

            context.Entry(employee).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound(new { message = Properties.Resources.FailureMessage });

            }
            catch (Exception)
            {
                return Ok(new { message = Properties.Resources.FailureMessage });
            }


            return Ok(new { message = Properties.Resources.EmployeeDetailsUpdatedMessage });
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            var employee = await context.Employees.FindAsync(id);

            context.Employees.Remove(employee);
            await context.SaveChangesAsync();

            return employee;
        }

        private bool EmployeeExists(int id)
        {
            return context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}
