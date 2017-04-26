using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace Freelance.Web.Models
{

    public class ProfileViewModel
    {
        
        public IndexState IndexState { get; set; }
        public Guid Id { get; set; }
        public string UserId { get; set; }
        [Display(Name = "Категории")]
        public Guid CategoryId { get; set; }
        [Display(Name = "Имя фрилансера")]
        public string FreelancerName { get; set; }
        [Display(Name = "Вид работ")]
        public string CategoryName { get; set; }
        [Required]
        [Display(Name = "Описание")]
        [DataType(DataType.MultilineText)]
        public string DescriptionProfile { get; set; }
        public string CategoryDescription { get; set; }
        [Display(Name = "Время доступности")]
        public string TimeAvailability { get; set; }
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Прикрепить файл")]
        public string FileName { get; set; }
        public string ImageName { get; set; }
        //public IDictionary<Guid, string> Categories { get; set; }
        [Display(Name = "Время доступности от")]
        public TimeSpan TimeFrom { get; set; }
        [Display(Name = "Время доступности до")]
        public TimeSpan TimeTo { get; set; }

    }
}