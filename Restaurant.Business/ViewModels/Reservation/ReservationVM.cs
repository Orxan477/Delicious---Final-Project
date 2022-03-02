using System;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Business.ViewModels.Reservation
{
    public class ReservationVM
    {
        [Required]
        public string FullName { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        //[MaxLength(12)]
        public string Number { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int PeopleCount { get; set; }
        public string Message { get; set; }
    }
}
