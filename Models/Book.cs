using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Maier_Teodora_Lab2.Models
{
    public class Book
    {
        public int ID { get; set; }
        [Display(Name = "Book Title")] public string Title { get; set; }
        [Column(TypeName = "decimal(6, 2)")] public decimal Price { get; set; }
        [Display(Name = "Publishing Date")] [DataType(DataType.Date)] public DateTime PublishingDate { get; set; }
        public int PublisherId { get; set; }
        [Display(Name = "Publisher")] public Publisher? Publisher { get; set; }

        public int AuthorID { get; set; }

        [Display(Name = "Author")] public Author? Author { get; set; }

        [Display(Name = "Boook Categories")] public ICollection<BookCategory>? BookCategories { get; set; }
    }
}