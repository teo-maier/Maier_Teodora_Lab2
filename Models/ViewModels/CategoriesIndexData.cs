﻿namespace Maier_Teodora_Lab2.Models.ViewModels;

public class CategoriesIndexData
{
    public IEnumerable<Category> Categories { get; set; }
    public IEnumerable<Book> Books { get; set; }
}