using ibrozk.Entity;
using Microsoft.EntityFrameworkCore;

namespace ibrozk.Data.Concrete.EfCore
{
    public class ImageContext : DbContext
    {
        public ImageContext(DbContextOptions<ImageContext> options) : base(options)
        {

        }
        public DbSet<Post> Posts => Set<Post>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<User> Users => Set<User>();
    }
}