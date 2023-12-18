using System.Data;

namespace LibearayManagementSystem.Models
{
    public class Bookdto
    {
            public int BookId { get; set; }
            public string BookName { get; set; }
            public DateTime PublishedOn { get; set; }
            public string Language { get; set; }
            public string Genre { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
    }
}
