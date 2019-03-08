using EmployeeForum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace EmployeeForum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly EmployeeForumContext context;
        private readonly IConfiguration _configuration;

        public IConfiguration Configuration { get; }

        public AuthController(EmployeeForumContext databaseContext, IConfiguration Configuration)
        {
           context = databaseContext;
            _configuration = Configuration;
        }


        [HttpPost, Route("login")]
        public IActionResult ValidateAuthentication([FromBody]Login loginCredentials)
        {
            if (UserExists(loginCredentials))
            {
                var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:SecurityKey"]));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokenOptions = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["Jwt:Duration"])),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new { Token = tokenString, EmployeeId = GetCurrentLoggedEmployeeId(loginCredentials) });
            }
            else
            {
                return Ok(new { message = Properties.Resources.LoginValidationMessage });
            }
        }

        [HttpPost, Route("register")]
        public IActionResult RegisterAnEmployee([FromBody] Employee employeeEntity)
        {
            try
            {
               context.Employees.Add(employeeEntity);
               context.SaveChanges();
            }
            catch
            {
                return BadRequest(new { message = Properties.Resources.RegistrationFailedMessage });
            }

            return Ok(new { message = Properties.Resources.RegistrationSuccessMessage });
        }

        [HttpGet("exists/{userName}")]
        public bool CheckUserNameExists(string userName)
        {
            return UserNameExists(userName);                                      
        }

        private bool UserNameExists(string userName)
        {
            return context.Employees.Any(employeeAuth => employeeAuth.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));
        }

        private bool UserExists(Login user)
        {
            return context.Employees.Any(employeeAuth => employeeAuth.UserName.Equals(user.UserName, StringComparison.OrdinalIgnoreCase) && employeeAuth.Password == user.Password);
        }

        private int GetCurrentLoggedEmployeeId(Login loginCredentials)
        {
            var employeeDetails = (from employee in context.Employees
                                   where employee.UserName.Equals(loginCredentials.UserName, StringComparison.OrdinalIgnoreCase) && employee.Password == loginCredentials.Password
                                   select employee.EmployeeId);

            return employeeDetails.First();
        }
    }
}
