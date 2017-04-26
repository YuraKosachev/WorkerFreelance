using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Freelance.Provider.EntityModels
{
    public class Offer : IModel, IModelContainDateTime
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("Profile")]
        public Guid ProfileId { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public DateTime DateOfCreate { get; set; }
        public bool FreelancerConfirm { get; set; }
        public string FileName { get; set; }

        //navi setting

        public virtual Profile Profile { get; set; }
        public virtual User User { get; set; }
    }
}
