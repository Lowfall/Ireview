using Ireview.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace Ireview.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }

        public DbSet<Article> Articles;
        public DbSet<Tag> Tags;
    }
}
