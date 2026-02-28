using Blogapplication.API.Data;
using Blogapplication.API.Models.Domain;
using Blogapplication.API.Reposiroties.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Blogapplication.API.Reposiroties.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext dbContext;
        public CategoryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;

        }
        public async Task<Category> CreateAsync(Category category)
            {
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();

            return category;

        }

        public async Task<Category?> EditAsync(Category category)
        {
            var ExistingCategory = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);

            if(ExistingCategory != null)
            {
                //replace the existing data with the updated value inside the database

                dbContext.Entry(ExistingCategory).CurrentValues.SetValues(category);
                await dbContext.SaveChangesAsync();
                return category;
            }
            return null;
        }

        public async Task<IEnumerable<Category>> getAllAsync()
        {
            return await dbContext.Categories.ToListAsync();
        }

        public async Task<Category?> GetById(Guid id)
        {
           return await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<Category?> DeleteAsync(Guid id)
        {
            var ExistingCategory = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (ExistingCategory is null)
            {
                return null;
            }
            dbContext.Categories.Remove(ExistingCategory);
            await dbContext.SaveChangesAsync();
            return ExistingCategory;
        }
    }
}
