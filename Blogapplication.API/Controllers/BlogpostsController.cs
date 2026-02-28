using Blogapplication.API.Models.Domain;
using Blogapplication.API.Models.DTO;
using Blogapplication.API.Reposiroties.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Blogapplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogpostsController : ControllerBase
    {
        private readonly IBlogpostrepository blogpostrepository;
        private readonly ICategoryRepository categoryRepository;

        public BlogpostsController(IBlogpostrepository blogpostrepository , 
            ICategoryRepository categoryRepository)
        {
            this.blogpostrepository = blogpostrepository;
            this.categoryRepository = categoryRepository;
        }

        //creating a new Blog: https://localhost:7222/api/Blogposts

        [HttpPost]

        public async Task<IActionResult> createBlogpost( [FromBody] CreateBlogpostrequestDto request)
        {

            //Convert the DTO to domain

            var blogpost = new Blogpost
            {
                Title = request.Title,
                ShortDescription = request.ShortDescription,
                Contact = request.Contact,
                FeaturedImageUrl = request.FeaturedImageUrl,
                Urlhandle = request.Urlhandle,
                PublishedDate = request.PublishedDate,
                IsVisible = request.IsVisible,
                Author = request.Author,
                categories = new List<Category>()

            };

            //implemented the relationship btw the category and the blogpost

            foreach (var categoryGuid in request.Category)
            {
                var ExistingCategory = await categoryRepository.GetById(categoryGuid);
                if(ExistingCategory != null)
                {
                    blogpost.categories.Add(ExistingCategory);
                }
            }
            blogpost = await blogpostrepository.createAsync(blogpost);

            //Convert the domain back to the DTO

            var response = new BlogpostDto
            {
                Id = blogpost.Id,
                Title = blogpost.Title,
                ShortDescription = blogpost.ShortDescription,
                Contact = blogpost.Contact,
                FeaturedImageUrl = blogpost.FeaturedImageUrl,
                Urlhandle = blogpost.Urlhandle,
                PublishedDate = blogpost.PublishedDate,
                IsVisible = blogpost.IsVisible,
                Author = blogpost.Author,
                categories = blogpost.categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Urlhandle = x.Urlhandle

                }).ToList()
            };
            return Ok(response);
        }


        //get all blog post data https://localhost:7222/api/Blogposts

        [HttpGet]
        public async Task<IActionResult> getAllBlogpost()
        {
            var blogposts = await blogpostrepository.getAllAsync();

            var response = new List<BlogpostDto>();

            foreach (var blogpost in blogposts)
            {
                response.Add(new BlogpostDto
                {
                    Id = blogpost.Id,
                    Title = blogpost.Title,
                    ShortDescription = blogpost.ShortDescription,
                    Contact = blogpost.Contact,
                    FeaturedImageUrl = blogpost.FeaturedImageUrl,
                    Urlhandle = blogpost.Urlhandle,
                    PublishedDate = blogpost.PublishedDate,
                    Author = blogpost.Author,
                    IsVisible = blogpost.IsVisible,
                    categories = blogpost.categories.Select(x => new CategoryDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Urlhandle = x.Urlhandle

                    }).ToList()

                });
            }
            return Ok(response);


        }

        //HttpGetAttribute the blog post by id

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetBlogbyid( [FromRoute] Guid id)
        {
            var Existblog = await blogpostrepository.GetById(id);

            if (Existblog is null)
            {
                return NotFound();
            }
            var response = new BlogpostDto
            {
                Id = Existblog.Id,
                Title = Existblog.Title,
                ShortDescription = Existblog.ShortDescription,
                Contact = Existblog.Contact,
                FeaturedImageUrl = Existblog.FeaturedImageUrl,
                Urlhandle = Existblog.Urlhandle,
                PublishedDate = Existblog.PublishedDate,
                IsVisible = Existblog.IsVisible,
                Author = Existblog.Author,
                categories = Existblog.categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Urlhandle = x.Urlhandle

                }).ToList()
            };
            return Ok(response);

        }


        //update a blogpost data

        [HttpPut]
        [Route("{id:Guid}")]

        public async Task<IActionResult> Editblogpost([FromRoute] Guid id  ,EditblogpostDto request )
        {
            var blogpost = new Blogpost
            {
                Id = id,
                Title = request.Title,
                ShortDescription = request.ShortDescription,
                Contact = request.Contact,
                FeaturedImageUrl = request.FeaturedImageUrl,
                Urlhandle = request.Urlhandle,
                PublishedDate = request.PublishedDate,
                IsVisible = request.IsVisible,
                Author = request.Author,
                categories = new List<Category>()

            };


            foreach (var categoryGuid in request.Categories)
            {
                var ExistingCategory = await categoryRepository.GetById(categoryGuid);
                if (ExistingCategory != null)
                {
                    blogpost.categories.Add(ExistingCategory);
                }
            }

            var updatedvalue = await blogpostrepository.EditBlogAsync(blogpost);
            if(updatedvalue == null)
            {
                return NotFound();
            }
            var response = new BlogpostDto
            {
                Id = blogpost.Id,
                Title = blogpost.Title,
                ShortDescription = blogpost.ShortDescription,
                Contact = blogpost.Contact,
                FeaturedImageUrl = blogpost.FeaturedImageUrl,
                Urlhandle = blogpost.Urlhandle,
                PublishedDate = blogpost.PublishedDate,
                IsVisible = blogpost.IsVisible,
                Author = blogpost.Author,
                categories = blogpost.categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Urlhandle = x.Urlhandle

                }).ToList()
            };
            return Ok(response);


        }


        //deleting a blogpost API


        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> DeleteBlog([FromRoute] Guid id)
        {

            var blogpost = await blogpostrepository.DeleteblogAsync(id);
            if (blogpost == null)
            {
                return NotFound();
            }
            var response = new BlogpostDto
            {
                Id = blogpost.Id,
                Title = blogpost.Title,
                ShortDescription = blogpost.ShortDescription,
                Contact = blogpost.Contact,
                FeaturedImageUrl = blogpost.FeaturedImageUrl,
                Urlhandle = blogpost.Urlhandle,
                PublishedDate = blogpost.PublishedDate,
                IsVisible = blogpost.IsVisible,
                Author = blogpost.Author
                

            };
            return Ok(response);
            
        }
    }
}
