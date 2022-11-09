using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Maier_Teodora_Lab2.Models;

namespace Maier_Teodora_Lab2.Pages.Books
{
    public class EditModel : BookCategoriesPageModel
    {
        private readonly Maier_Teodora_Lab2.Data.Maier_Teodora_Lab2Context _context;

        public EditModel(Maier_Teodora_Lab2.Data.Maier_Teodora_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty] public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Publisher)
                .Include(b => b.BookCategories).ThenInclude(b => b.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (book == null)
            {
                return NotFound();
            }

            PopulateAssignedCategoryData(_context, book);
            var authorList = _context.Author.Select(x => new
            {
                x.Id,
                FullName = x.LastName + " " + x.FirstName
            });
            ViewData["Id"] = new SelectList(authorList, "Id", "FullName");
            ViewData["PublisherId"] = new SelectList(_context.Publisher, "Id", "PublisherName");
            ViewData["AuthorID"] = new SelectList(_context.Author, "Id", "FullName");

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCategories)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookToUpdate = await _context.Book
                .Include(i => i.Publisher)
                .Include(i => i.BookCategories)
                .ThenInclude(i => i.Category)
                .FirstOrDefaultAsync(s => s.ID == id);
            if (bookToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Book>(
                    bookToUpdate,
                    "Book",
                    i => i.Title, i => i.Author,
                    i => i.Price, i => i.PublishingDate, i => i.Publisher))
            {
                UpdateBookCategories(_context, selectedCategories, bookToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            UpdateBookCategories(_context, selectedCategories, bookToUpdate);
            PopulateAssignedCategoryData(_context, bookToUpdate);
            return Page();
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.ID == id);
        }
    }
}