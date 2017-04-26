using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Freelance.Web.Models
{
    public class CategoryViewModel
    {

        public IndexState IndexState { get; set; }
        public Guid CategoryId { get; set; }
        [Required]
        [RegularExpression(@"^[A-zА-яЁё -]+$", ErrorMessage = "Значение должно содержать только буквы")]
        [Display(Name = "Название категории")]
        public string NameCategory { get; set; }
        public string Image { get; set; }
        [Required]
        [Display(Name = "Описание категории")]
        [DataType(DataType.MultilineText)]
        [RegularExpression(@"[^<>]+$", ErrorMessage = "Введен недопустимый символ")]
        public string DescriptionCategory { get; set; }
        public string ImageName { get; set; }
    }
}