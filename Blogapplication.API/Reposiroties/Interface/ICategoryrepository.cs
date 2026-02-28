using Blogapplication.API.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace Blogapplication.API.Reposiroties.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync(Category category);
        Task<IEnumerable<Category>> getAllAsync(); 

        Task<Category?>GetById(Guid id);

        Task<Category?> EditAsync(Category category);

        Task<Category?> DeleteAsync(Guid id);

    }
}