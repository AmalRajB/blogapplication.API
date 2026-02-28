namespace Blogapplication.API.Models.DTO
{
    public class BlogpostDto
    {

        public Guid Id { get; set; }
        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string Contact { get; set; }

        public string FeaturedImageUrl { get; set; }

        public string Urlhandle { get; set; }

        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool IsVisible { get; set; }

        public List<CategoryDto> categories { get; set; } = new List<CategoryDto>();



    }
}
