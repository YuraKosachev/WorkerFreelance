using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Freelance.Provider.EntityModels
{
    public class Profile : IModel
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        [Required]
        public string DescriptionProfile { get; set; }
        public TimeSpan TimeFrom { get; set; }
        public TimeSpan TimeTo { get; set; }
        public string FileName { get; set; }
        public string ImageName { get; set; }

        //navi setting

        public virtual User User { get; set; }
        public virtual Category Category { get; set; }
        public ICollection<Offer> Offers { get; set; }
    }
}
