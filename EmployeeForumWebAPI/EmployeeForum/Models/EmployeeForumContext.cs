using Microsoft.EntityFrameworkCore;

namespace EmployeeForum.Models
{
    public class EmployeeForumContext : DbContext
    {
        public EmployeeForumContext(DbContextOptions<EmployeeForumContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Post> Posts { get; set; }
        public DbSet<PostReply> PostReplies { get; set; }

        public DbSet<PostActivity> PostActivities { get; set; }


    }
}
