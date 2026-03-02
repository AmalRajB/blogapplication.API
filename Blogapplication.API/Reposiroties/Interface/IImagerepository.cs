using Blogapplication.API.Models.Domain;

namespace Blogapplication.API.Reposiroties.Interface
{
    public interface IImagerepository
    {
        Task<Blogimage> Upload(IFormFile file, Blogimage blogimage);

        Task<IEnumerable<Blogimage>> GetAll();
    }
}
