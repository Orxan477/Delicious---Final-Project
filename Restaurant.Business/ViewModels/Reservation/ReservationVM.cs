using System;

namespace Restaurant.Business.ViewModels.Reservation
{
    public class ReservationVM
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public int Number { get; set; }
        public DateTime Date { get; set; }
        //public string Time { get; set; }
        public int PeopleCount { get; set; }
        public string Message { get; set; }
    }
}
