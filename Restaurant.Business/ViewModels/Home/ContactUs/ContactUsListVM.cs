using System;

namespace Restaurant.Business.ViewModels.Home.ContactUs
{
    public class ContactUsListVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime SentDate { get; set; }
    }
}
