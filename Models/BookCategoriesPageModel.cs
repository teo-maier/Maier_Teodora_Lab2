using Microsoft.AspNetCore.Mvc.RazorPages;
using Maier_Teodora_Lab2.Data;

namespace Maier_Teodora_Lab2.Models;

public class BookCategoriesPageModel : PageModel
{
    public List<AssignedCategoryData> AssignedCategoryDataList;

    public void PopulateAssignedCategoryData(Maier_Teodora_Lab2Context context,
        Book book)
    {
        var allCategories = context.Category;
        // The list is not null or empty
        var bookCategories = new HashSet<int>(
            book.BookCategories.Select(c=>c.Category.ID)); //
        AssignedCategoryDataList = new List<AssignedCategoryData>();
        foreach (var cat in allCategories)
        {
            AssignedCategoryDataList.Add(new AssignedCategoryData
            {
                CategoryId = cat.ID,
                Name = cat.CategoryName,
                Assigned = bookCategories.Contains(cat.ID)
            });
        }
    }

    public void UpdateBookCategories(Maier_Teodora_Lab2Context context,
        string[] selectedCategories, Book bookToUpdate)
    {
        if (selectedCategories == null)
        {
            bookToUpdate.BookCategories = new List<BookCategory>();
            return;
        }

        var selectedCategoriesHs = new HashSet<string>(selectedCategories);
        var bookCategories = new HashSet<int>
            (bookToUpdate.BookCategories.Select(c => c.Category.ID));
        foreach (var cat in context.Category)
        {
            if (selectedCategoriesHs.Contains(cat.ID.ToString()))
            {
                if (!bookCategories.Contains(cat.ID))
                {
                    bookToUpdate.BookCategories.Add(
                        new BookCategory
                        {
                            BookId = bookToUpdate.ID,
                            CategoryId = cat.ID
                        });
                }
            }
            else
            {
                if (bookCategories.Contains(cat.ID))
                {
                    BookCategory courseToRemove = bookToUpdate.BookCategories
                        .SingleOrDefault(i => i.CategoryId == cat.ID);
                    context.Remove(courseToRemove);
                }
            }
        }
    }
}