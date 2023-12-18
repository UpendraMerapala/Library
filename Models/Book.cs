using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace LibearayManagementSystem.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        [Required]
        public string BookName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime PublishedOn { get; set; }
        [Required]
        public string Language {  get; set; }
        [Required]
        public string Genre { get; set; }
        

        //Foreign Key
        [Display(Name = "Publication")]
        public virtual int PublicationId {  get; set; }

        [ForeignKey("PublicationId")]
        public virtual Publication Publications { get; set;}

        //Foreign key
        [Display(Name = "Author")]
        public virtual int AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public virtual Author Authors { get; set; }

    }
}
