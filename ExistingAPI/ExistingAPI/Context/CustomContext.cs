using ExistingAPI.Entity;
using Microsoft.EntityFrameworkCore;

namespace ExistingAPI.Context
{
    public class CustomContext: DbContext
    {
        public CustomContext(DbContextOptions options): base(options) 
        {
            
        }

        public DbSet<UserEntity> Users { get; set; }
    }
}
