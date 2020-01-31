using LibraryDataContext;
using LibraryModels;
using LibraryRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Test
{
    public class BookTest
    {
        private DbContextOptions<LibraryContext> getDatabase(string DbName)
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
              .UseInMemoryDatabase(databaseName: DbName)
              .Options;
            return options;
        }

        [Fact]
        public async Task AddBookTest()
        {
            var options = getDatabase("AddBookDB");
            await using (var context = new LibraryContext(options))
            {
                var _manager = new LibraryManager(context);
                await _manager.AddBook(SingleBook());
                await context.SaveChangesAsync();
                Assert.Equal(1, await context.Books.CountAsync());
                Assert.Equal("ISBNCode", context.Books.Single().ISBN);
                Assert.Equal("AuthorName", context.Books.Single().Author.Name);
            }
        }

        [Fact]
        public async Task GetBookByIdTest()
        {
            var options = getDatabase("GetBookDB");
            await using (var context = new LibraryContext(options))
            {
                var _manager = new LibraryManager(context);
                await _manager.AddBook(SingleBook());
                await context.SaveChangesAsync();
                var book = await _manager.GetBookById(1);
                Assert.Equal(1, book.Id);
                Assert.Equal("ISBNCode", context.Books.Single().ISBN);
                Assert.Equal("AuthorName", context.Books.Single().Author.Name);
            }
        }

        [Fact]
        public async Task GetListofBooksTest()
        {
            var options = getDatabase("GetListofBooksDB");

            await using (var context = new LibraryContext(options))
            {
                var manager = new LibraryManager(context);
                await context.Books.AddRangeAsync(ListOfBooks());
                await context.SaveChangesAsync();
                var books = await manager.GetBooks();
                Assert.Equal(2, books.Count);
            }
        }

        [Fact]
        public async Task UpdateBook()
        {
            var options = getDatabase("UpdateBookDB");
            await using (var context = new LibraryContext(options))
            {
                var _manager = new LibraryManager(context);
                await _manager.AddBook(SingleBook());
                await context.SaveChangesAsync();
            }
            await using (var context = new LibraryContext(options))
            {
                var _manager = new LibraryManager(context);
                var checkUpdate = await _manager.UpdateBook(new Book
                {
                    ISBN = "ISBNCode",
                    Title = "UpdatedBookTitle",
                    BookIsRemoved = false,
                    Author = new Author { Name = "AuthorName" }
                }, 1);
                await context.SaveChangesAsync();
                Assert.Equal(1, checkUpdate);
                Assert.Equal("UpdatedBookTitle", context.Books.Single().Title);
            }
        }

        [Fact]
        public async Task DeleteBookTest()
        {
            var options = getDatabase("DeleteBookDB");

            await using (var context = new LibraryContext(options))
            {
                var manager = new LibraryManager(context);
                await manager.AddBook(SingleBook());
                await context.SaveChangesAsync();
                var checkDelete = await manager.DeleteBook(1);
                Assert.Equal(1, checkDelete);
            }
        }

        private Book SingleBook()
        {
            return
            new Book
            {
                ISBN = "ISBNCode",
                Title = "BookTitle",
                BookIsRemoved = false,
                Author = new Author { Name = "AuthorName" }
            };
        }

        private List<Book> ListOfBooks()
        {
            return
            new List<Book>
            {
                new Book
                {
                ISBN = "ISBNCode1",
                Title = "BookTitle1",
                BookIsRemoved = false,
                Author = new Author { Name = "AuthorName1" }
                },
                new Book
                {
                ISBN = "ISBNCode2",
                Title = "BookTitle2",
                BookIsRemoved = false,
                Author = new Author { Name = "AuthorName2" }
                }
            };
        }
    }
}