namespace Restaurant.Core.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Number { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public int PeopleCount { get; set; }
        public string Message { get; set; }
        public bool IsDeleted { get; set; }
    }
}
