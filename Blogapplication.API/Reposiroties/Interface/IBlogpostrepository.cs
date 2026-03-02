using Blogapplication.API.Models.Domain;

namespace Blogapplication.API.Reposiroties.Interface
{
    public interface IBlogpostrepository
    {
        Task<Blogpost> createAsync(Blogpost blogpost);
        Task<IEnumerable<Blogpost>> getAllAsync();

        Task<Blogpost?> GetById(Guid id);
        Task<Blogpost?> GetbyUrl(string urlhandle);

        Task<Blogpost?> EditBlogAsync(Blogpost blogpost);

        Task<Blogpost?> DeleteblogAsync(Guid id);
    }
}
