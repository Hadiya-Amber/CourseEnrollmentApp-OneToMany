using Microsoft.EntityFrameworkCore;

namespace ManyToMany.Models
{
    public class BookDbContext: DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        public DbSet<BookAuthor> BookAuthors { get; set; }

        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<BookAuthor>()
                .HasKey(ba => new { ba.BookId, ba.AuthorId });
            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Book)
                .WithMany(b => b.BookAuthors)
                .HasForeignKey(ba => ba.BookId);
            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Author)
                .WithMany(a => a.BookAuthors)
                .HasForeignKey(ba => ba.AuthorId);

            modelBuilder.Entity<Book>()
                .HasData(
                    new Book { BookId = 1, Title = "C# Programming", PublicationYear = 2020, Price = 29.99M },
                    new Book { BookId = 2, Title = "Entity Framework Core", PublicationYear = 2021, Price = 39.99M }
                );
            modelBuilder.Entity<Author>()
                .HasData(
                    new Author { AuthorId = 1, Name = "Hadiya" },
                    new Author { AuthorId = 2, Name = "Charan" }
                );
            modelBuilder.Entity<BookAuthor>()
                .HasData(
                    new BookAuthor { BookId = 1, AuthorId = 1 },
                    new BookAuthor { BookId = 2, AuthorId = 2 },
                    new BookAuthor { BookId = 1, AuthorId = 2 }
                );
        }
    }
}
