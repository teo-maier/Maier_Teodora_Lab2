using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Maier_Teodora_Lab2.Data;
using Maier_Teodora_Lab2.Models;

namespace Maier_Teodora_Lab2.Pages.Borrowings
{
    public class DetailsModel : PageModel
    {
        private readonly Maier_Teodora_Lab2.Data.Maier_Teodora_Lab2Context _context;

        public DetailsModel(Maier_Teodora_Lab2.Data.Maier_Teodora_Lab2Context context)
        {
            _context = context;
        }

        public Borrowing Borrowing { get; set; } = default!;
        public Member Member { get; set; } = default!;
        public Book Book { get; set; } = default!;


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Borrowing == null)
            {
                return NotFound();
            }

            var borrowing = await _context.Borrowing.FirstOrDefaultAsync(m => m.ID == id);
            if (borrowing == null)
            {
                return NotFound();
            }
            else
            {
                Member = _context.Member.Find(borrowing.MemberID);
                Book = _context.Book.Find(borrowing.BookID);
                Book.Title = Book.Title +
                             " - " +
                             _context.Author.Find(Book.AuthorID).LastName +
                             " " +
                             _context.Author.Find(Book.AuthorID).FirstName;
                Borrowing = borrowing;
            }

            return Page();
        }
    }
}