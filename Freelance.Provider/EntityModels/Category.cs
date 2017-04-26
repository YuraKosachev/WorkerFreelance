using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace Freelance.Provider.EntityModels
{
    public class Category : IModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string NameCategory { get; set; }
        [Required]
        public string DescriptionCategory { get; set; }
        public string ImageName { get; set; }

        public virtual ICollection<Profile> Profilies { get; set; }

    }
}
