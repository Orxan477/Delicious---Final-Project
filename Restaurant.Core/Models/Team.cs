namespace Restaurant.Core.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Image { get; set; }
        public string About { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }
        public  bool IsDeleted { get; set; }
    }
}
