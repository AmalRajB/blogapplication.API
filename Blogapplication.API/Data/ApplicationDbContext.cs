using Blogapplication.API.Models.Domain;
using Microsoft.EntityFrameworkCore;    
namespace Blogapplication.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
           
        }
        public DbSet<Blogpost> Blogposts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Blogimage> Blogimages { get; set; }

    }
}
