using System.ComponentModel.DataAnnotations;

namespace LibearayManagementSystem.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Nationality { get; set; }

    }
}
