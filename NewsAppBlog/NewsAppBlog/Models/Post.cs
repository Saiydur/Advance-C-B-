namespace NewsAppBlog.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
