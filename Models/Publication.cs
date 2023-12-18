using System.ComponentModel.DataAnnotations;

namespace LibearayManagementSystem.Models
{
    public class Publication
    {
        [Key]
        public int PublicationId { get; set; }
        [Required]
        public string PublishingCompanyName { get; set; }

        //Foreign Key
        [Display(Name = "AddressId")]
        public virtual Address Addresses { get; set; }

    }
}
