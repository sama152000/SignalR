namespace ProductCommentApp.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Username { get; set; }
        public string Text { get; set; }
        public Product Product { get; set; } 
    }
}