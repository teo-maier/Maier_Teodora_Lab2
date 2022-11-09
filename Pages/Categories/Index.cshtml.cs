using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Maier_Teodora_Lab2.Data;
using Maier_Teodora_Lab2.Models;
using Maier_Teodora_Lab2.Models.ViewModels;

namespace Maier_Teodora_Lab2.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly Maier_Teodora_Lab2.Data.Maier_Teodora_Lab2Context _context;

        public IndexModel(Maier_Teodora_Lab2.Data.Maier_Teodora_Lab2Context context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; } = default!;
        public CategoriesIndexData CategoriesData { get; set; }
        public int CategoryId { get; set; }
        public int BookID { get; set; }

        public async Task OnGetAsync(int? id, int? bookID)
        {
            if (_context.Category != null)
            {
                CategoriesData = new CategoriesIndexData();
                CategoriesData.Categories = await _context.Category
                    .Include(i => i.Books)
                    .ThenInclude(c => c.Author)
                    .OrderBy(i => i.CategoryName)
                    .ToListAsync();
                if (id != null)
                {
                    CategoryId = id.Value;
                    Category category = CategoriesData.Categories
                        .Where(i => i.ID == id.Value).Single();
                    CategoriesData.Books = category.Books;
                }
            }
        }
    }
}
