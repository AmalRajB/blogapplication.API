namespace Blogapplication.API.Models.DTO
{
    public class CreateBlogpostrequestDto
    {
        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string Contact { get; set; }

        public string FeaturedImageUrl { get; set; }

        public string Urlhandle { get; set; }

        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool IsVisible { get; set; }

        public Guid[] Category { get; set; }
    }
}
