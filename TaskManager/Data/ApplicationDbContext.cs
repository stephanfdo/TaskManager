using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models;
namespace TaskManager.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Update the property name to match the entity name
        public DbSet<UserTask> UserTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Configure UserTask entity
            builder.Entity<UserTask>()
                .HasIndex(t => new { t.UserId, t.Title });

            // Map to a new table name "UserTasks" instead of "Tasks"
            builder.Entity<UserTask>()
                .ToTable("UserTasks");
        }
    }
}