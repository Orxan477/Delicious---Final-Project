namespace Restaurant.Core.Models
{
    public class BillingAdress
    {
        public int Id { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
