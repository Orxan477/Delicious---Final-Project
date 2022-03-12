using System;

namespace Restaurant.Business.ViewModels.Reservation
{
    public class ReservationListVM
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        //public DateTime Time { get; set; }
        public int PeopleCount { get; set; }
        public string Message { get; set; }
    }
}
