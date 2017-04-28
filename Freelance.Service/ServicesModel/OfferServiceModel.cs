using System;


namespace Freelance.Service.ServicesModel
{
    public class OfferServiceModel
    {

        public Guid Id { get; set; }
        public Guid ProfileId { get; set; }
        public string UserId { get; set; }
        public string FreelancerName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Date { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public bool FreelancerConfirm { get; set; }
        public DateTime DateOfCreate { get; set; }
        public string NameCategory { get; set; }
        public string DescriptionCategory { get; set; }
        public string CategoryImageName { get; set; }



    }
}
