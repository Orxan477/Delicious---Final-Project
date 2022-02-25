namespace Restaurant.Core.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string FullName { get; set; }
        public Position Position { get; set; }
        public string Comment { get; set; }
    }
}
