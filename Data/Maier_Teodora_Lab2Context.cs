using Microsoft.EntityFrameworkCore;
using Maier_Teodora_Lab2.Models;

namespace Maier_Teodora_Lab2.Data
{
    public class Maier_Teodora_Lab2Context : DbContext
    {
        public Maier_Teodora_Lab2Context(DbContextOptions<Maier_Teodora_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Maier_Teodora_Lab2.Models.Book> Book { get; set; }

        public DbSet<Maier_Teodora_Lab2.Models.Publisher> Publisher { get; set; }

        public DbSet<Maier_Teodora_Lab2.Models.Author> Author { get; set; }

        public DbSet<Maier_Teodora_Lab2.Models.Category> Category { get; set; }

        public DbSet<Maier_Teodora_Lab2.Models.BookCategory> BookCategory { get; set; }

        public DbSet<Maier_Teodora_Lab2.Models.Member>? Member { get; set; }

        public DbSet<Maier_Teodora_Lab2.Models.Borrowing>? Borrowing { get; set; }
    }
}