using Blogapplication.API.Data;
using Blogapplication.API.Models.Domain;
using Blogapplication.API.Reposiroties.Interface;
using Microsoft.EntityFrameworkCore;

namespace Blogapplication.API.Reposiroties.Implementation
{
    public class Blogpostrepository : IBlogpostrepository
    {
        private readonly ApplicationDbContext dbContext;
        public Blogpostrepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ApplicationDbContext Dbcontext { get; }

        public async Task<Blogpost> createAsync(Blogpost blogpost)
        {
            await dbContext.Blogposts.AddAsync(blogpost);
            await dbContext.SaveChangesAsync();
            return blogpost;
            
        }

        public async Task<Blogpost?> DeleteblogAsync(Guid id)
        {
            var ExistingBlog = await dbContext.Blogposts.FirstOrDefaultAsync(x => x.Id == id);
            if (ExistingBlog == null)
            {
                return null;   
            }
            dbContext.Blogposts.Remove(ExistingBlog);
            await dbContext.SaveChangesAsync();
            return (ExistingBlog);
        }

        public async Task<Blogpost?> EditBlogAsync(Blogpost blogpost)
        {
            var Existingdata = await dbContext.Blogposts.Include(x => x.categories).FirstOrDefaultAsync(x => x.Id == blogpost.Id);
            
            if(Existingdata == null)
            {
                return null;
      
            }
            //update the blog content
            dbContext.Entry(Existingdata).CurrentValues.SetValues(blogpost);

            //update the category 
            Existingdata.categories = blogpost.categories;

            await dbContext.SaveChangesAsync();
            return blogpost;
             
        }

        public async Task<IEnumerable<Blogpost>> getAllAsync()
        {
            return await dbContext.Blogposts.Include(x => x.categories).ToListAsync();
            
        }

        public async Task<Blogpost?> GetById(Guid id)
        {
            return await dbContext.Blogposts.Include(x => x.categories).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
