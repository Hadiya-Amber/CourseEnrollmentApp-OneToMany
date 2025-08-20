using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ManyToMany.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string? Title { get; set; }

        public int PublicationYear { get; set; }
        [Precision(18, 2)]
        public decimal Price { get; set; }

        public ICollection<BookAuthor>? BookAuthors { get; set; } 
    }
}
