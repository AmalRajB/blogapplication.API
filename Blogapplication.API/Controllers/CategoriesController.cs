using Blogapplication.API.Data;
using Blogapplication.API.Models.Domain;
using Blogapplication.API.Models.DTO;
using Blogapplication.API.Reposiroties.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Blogapplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ICategoryRepository categoryRepository;

        public CategoriesController(ApplicationDbContext dbContext, ICategoryRepository categoryRepository)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }
        //AddingNewEventArgs new Category to the db
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryrequestDto request)


        {
            var category = new Category
            {
                Name = request.Name,
                Urlhandle = request.Urlhandle
            };
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();

            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Urlhandle = category.Urlhandle
            };

            return Ok(response);
        }


        [HttpGet]

        //List all category data https://localhost:7222/api/Categories
        public async Task<IActionResult>getallcategoryList()
        {
            var categories = await categoryRepository.getAllAsync();

            var response = new List<CategoryDto>();
            foreach(var category in categories)
            {
                response.Add(new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    Urlhandle = category.Urlhandle
                });
            }
            return Ok(response);

        }


        //getting a catogory based on the id

        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<IActionResult> getCategoryById([FromRoute] Guid id)
        {
            await categoryRepository.GetById(id);

            var existingCategory = await categoryRepository.GetById(id);

            if (existingCategory == null)
            {
                return NotFound();
            }
            var response = new CategoryDto
            {
                Id = existingCategory.Id,
                Name = existingCategory.Name,
                Urlhandle = existingCategory.Urlhandle
            };
            return Ok(response);

        }

        //edithing an existing category record API

        [HttpPut]
        [Route("{id:Guid}")]

        public async Task<IActionResult> editCategoryById([FromRoute] Guid id, EditCategoryDto request)
        {
            var category = new Category
            {
                Id = id,
                Name = request.Name,
                Urlhandle = request.Urlhandle

            };

            category = await categoryRepository.EditAsync(category);
            if(category == null)
            {
                return NotFound();
            }
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Urlhandle = category.Urlhandle
            };
            return Ok(response);

        }


        //deleting an existing category item from the database

        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> DeleteCategoryById([FromRoute] Guid id)
        {

            var category = await categoryRepository.DeleteAsync(id);

            if (category is null)
            {
                return NotFound();
            }

            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Urlhandle = category.Urlhandle
            };
            return Ok(response);

        }

    }
}
