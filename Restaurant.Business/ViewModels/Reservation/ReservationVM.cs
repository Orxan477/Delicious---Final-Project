using System;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Business.ViewModels.Reservation
{
    public class ReservationVM
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public int PeopleCount { get; set; }
        public string Message { get; set; }
    }
}
