using Microsoft.EntityFrameworkCore;
using UseSwaggerAuthorization.Models;

namespace UseSwaggerAuthorization.Context
{
    public class UseSwaggerAuthorizationDbContext : DbContext
    {
        public UseSwaggerAuthorizationDbContext()
        {
        }

        public UseSwaggerAuthorizationDbContext(DbContextOptions<UseSwaggerAuthorizationDbContext> options) : base(options) 
        {
        
        }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("YourLocalDb");
            }
        }

    }
}
