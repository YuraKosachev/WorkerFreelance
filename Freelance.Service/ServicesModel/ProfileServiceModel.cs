using System;


namespace Freelance.Service.ServicesModel
{
    public class ProfileServiceModel
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid CategoryId { get; set; }
        public string FreelancerName { get; set; }
        public string CategoryName { get; set; }
        public string PhoneNumber { get; set; }
        public string FileName { get; set; }
        public string ImageName { get; set; }
        public string DescriptionProfile { get; set; }
        public string CategoryDescription { get; set; }
        public TimeSpan TimeFrom { get; set; }
        public TimeSpan TimeTo { get; set; }
    }
}
