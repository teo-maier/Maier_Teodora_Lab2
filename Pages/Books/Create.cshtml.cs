using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Maier_Teodora_Lab2.Models;
using Microsoft.AspNetCore.Authorization;

namespace Maier_Teodora_Lab2.Pages.Books
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : BookCategoriesPageModel
    {
        private readonly Maier_Teodora_Lab2.Data.Maier_Teodora_Lab2Context _context;

        public CreateModel(Maier_Teodora_Lab2.Data.Maier_Teodora_Lab2Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var authorList = _context.Author.Select(x => new
            {
                x.Id,
                FullName = x.LastName + " " + x.FirstName
            });

            ViewData["AuthorId"] = new SelectList(authorList, "Id", "FullName");
            ViewData["PublisherId"] = new SelectList(_context.Publisher, "Id", "PublisherName");
            var book = new Book();
            book.BookCategories = new List<BookCategory>();
            PopulateAssignedCategoryData(_context, book);
            return Page();
        }

        [BindProperty] public Book Book { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
        {
            var newBook = new Book();
            if (selectedCategories != null)
            {
                newBook.BookCategories = new List<BookCategory>();
                foreach (var cat in selectedCategories)
                {
                    var catToAdd = new BookCategory
                    {
                        CategoryId = int.Parse(cat),
                    };
                    newBook.BookCategories.Add(catToAdd);
                }
            }

            if (await TryUpdateModelAsync<Book>(
                    newBook,
                    "Book",
                    i => i.Title,
                    i => i.AuthorID,
                    i => i.Price,
                    i => i.PublishingDate,
                    i => i.PublisherId
                ))
            {
                _context.Book.Add(newBook);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            PopulateAssignedCategoryData(_context, newBook);
            return Page();
        }
    }
}