using bugtracker.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace bugtracker.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Bug> BugDatabase { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}