using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Maier_Teodora_Lab2.Data;
using Maier_Teodora_Lab2.Models;

namespace Maier_Teodora_Lab2.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly Maier_Teodora_Lab2.Data.Maier_Teodora_Lab2Context _context;

        public IndexModel(Maier_Teodora_Lab2.Data.Maier_Teodora_Lab2Context context)
        {
            _context = context;
        }

        public IList<Book> Book { get; set; }
        public BookData BookData { get; set; }
        public int BookId { get; set; }
        public int CategoryId { get; set; }
        public string TitleSort { get; set; }
        public string AuthorSort { get; set; }
        public string CurrentFilter { get; set; }


        public async Task OnGetAsync(int? id, int? categoryId, string sortOrder, string searchString)
        {
            BookData = new BookData();

            TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            AuthorSort = String.IsNullOrEmpty(sortOrder) ? "author_desc" : "";
            CurrentFilter = searchString;

            BookData.Books = await _context.Book
                .Include(b => b.Publisher)
                .Include(b => b.Author)
                .Include(b => b.BookCategories)
                .ThenInclude(b => b.Category)
                .AsNoTracking()
                .OrderBy(b => b.Title)
                .ToListAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                BookData.Books = BookData.Books.Where(s => s.Author.FirstName.Contains(searchString)
                                                           || s.Author.LastName.Contains(searchString)
                                                           || s.Title.Contains(searchString));
            }

            if (id != null)
            {
                BookId = id.Value;
                Book book = BookData.Books
                    .Where(i => i.ID == id.Value).Single();
                BookData.Categories = book.BookCategories.Select(s => s.Category);
            }

            switch (sortOrder)
            {
                case "title_desc":
                    BookData.Books = BookData.Books.OrderByDescending(s =>
                        s.Title);
                    break;
                case "author_desc":
                    BookData.Books = BookData.Books.OrderByDescending(s =>
                        s.Author.FullName);
                    break;
            }
        }
    }
}