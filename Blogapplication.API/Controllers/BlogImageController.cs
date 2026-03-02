using Blogapplication.API.Models.Domain;
using Blogapplication.API.Models.DTO;
using Blogapplication.API.Reposiroties.Implementation;
using Blogapplication.API.Reposiroties.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Blogapplication.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BlogImageController : ControllerBase
    {
        private readonly IImagerepository imagerepository;

        public BlogImageController(IImagerepository imagerepository)
        {
            this.imagerepository = imagerepository;
        }

        //adding the image into API image folder and store the path in to the database

        [HttpPost]

        public async Task<IActionResult> Fileupload([FromForm] IFormFile file,
            [FromForm] string filename , [FromForm] string title)
        {
            ValidateFileUpload(file);

            if (ModelState.IsValid)
            {
                var blogImage = new Blogimage
                {
                    FileExtension = Path.GetExtension(file.FileName).ToLower(),
                    FileName = filename,
                    Title = title,
                    DateCreated = DateTime.Now,
                };

                blogImage = await imagerepository.Upload(file, blogImage);

                var response = new BlogimageDto
                {
                    Id = blogImage.Id,
                    FileName = blogImage.FileName,
                    Title = blogImage.Title,
                    FileExtension = blogImage.FileExtension,
                    Url = blogImage.Url,
                    DateCreated = blogImage.DateCreated
                };
                return Ok(response);

            }

            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(IFormFile file)
        {
            var AllowdExtensions = new[] { ".jpg", ".jpeg", ".png" };

            if (!AllowdExtensions.Contains(Path.GetExtension(file.FileName)?.Trim().ToLower()))
            {
                ModelState.AddModelError("file", "unsupported file format");
            }
            if (file.Length  > 10485760)
            {
                ModelState.AddModelError("file", "file size is greaterthan 10mb ");
            }
        }



        //get all images form db

        [HttpGet]

        public async Task<IActionResult> getAllImage()
        {
            var images = await imagerepository.GetAll();

            var response = new List<BlogimageDto>();

            foreach (var image in images)
            {
                response.Add(new BlogimageDto
                {
                    Id = image.Id,
                    FileName = image.FileName,
                    Title = image.Title,
                    FileExtension = image.FileExtension,
                    Url = image.Url,
                    DateCreated = image.DateCreated

                }); 
            }
            return Ok(response);
        }
    

    }
}
