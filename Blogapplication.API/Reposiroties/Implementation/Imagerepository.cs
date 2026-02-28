using Blogapplication.API.Data;
using Blogapplication.API.Models.Domain;
using Blogapplication.API.Reposiroties.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Blogapplication.API.Reposiroties.Implementation
{
    public class Imagerepository : IImagerepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ApplicationDbContext dbContext;

        public Imagerepository(IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor,ApplicationDbContext dbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }
        public async Task<Blogimage> Upload(IFormFile file, Blogimage blogimage)
        {

            //Upload the image to the Image folder
            var localpath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{blogimage.FileName}{blogimage.FileExtension}");

            using var stream = new FileStream(localpath, FileMode.Create);

            await file.CopyToAsync(stream);

            //update the database

            var httprequest = httpContextAccessor.HttpContext.Request;
            var Urlpath = $"{httprequest.Scheme}://{httprequest.Host}{httprequest.PathBase}/Image/{blogimage.FileName}{blogimage.FileExtension}";

            blogimage.Url = Urlpath;


            await dbContext.Blogimages.AddAsync(blogimage);

            await dbContext.SaveChangesAsync();

            return blogimage;



        }
    }
}
