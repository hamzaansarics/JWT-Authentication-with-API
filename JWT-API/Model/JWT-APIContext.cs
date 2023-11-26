using Microsoft.EntityFrameworkCore;

namespace JWT_API.Model
{
    public class JWT_APIContext:DbContext
    {
        public JWT_APIContext(DbContextOptions<JWT_APIContext>options):base(options)
        {
                
        }

        public DbSet<customermodel> customers { get; set; }
        public DbSet<UserModel> users { get; set; }
    }
}
