using System.ComponentModel.DataAnnotations;

namespace LibearayManagementSystem.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }
        [Required]
        public string AddressLine1 { get; set; }
        [Required]
        public string AddressLine2 { get; set;}
        [Required]
        public String Pincode { get; set;}
    }
}
