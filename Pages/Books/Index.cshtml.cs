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

        public IList<Book> Book { get; set; } = default!;
        public BookData BookData { get; set; }
        public int BookId { get; set; }
        public int CategoryId { get; set; }


        public async Task OnGetAsync(int? id, int? categoryID)
        {
            BookData = new BookData();

            BookData.Book = await _context.Book
                .Include(b => b.Publisher)
                .Include(b => b.Author)
                .Include(b => b.BookCategories)
                .ThenInclude(b => b.Category)
                .AsNoTracking()
                .OrderBy(b => b.Title)
                .ToListAsync();
            if (id != null)
            {
                BookId = id.Value;
                Book book = BookData.Book
                    .Where(i => i.ID == id.Value).Single();
                BookData.Categories = book.BookCategories.Select(s => s.Category);
            }
        }
    }
}