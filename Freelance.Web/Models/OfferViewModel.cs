using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Freelance.Web.Models
{
    public class OfferViewModel
    {
        public IndexState IndexState { get; set; }
        public Guid Id { get; set; }
        public Guid ProfileId { get; set; }
        public string UserId { get; set; }
        [Display(Name = "Имя фрилансера")]
        public string FreelancerName { get; set; }
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Required]
        public TimeSpan Time { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public DateTime DateOfCreate { get; set; }
        public bool FreelancerConfirm { get; set; }
    }
}