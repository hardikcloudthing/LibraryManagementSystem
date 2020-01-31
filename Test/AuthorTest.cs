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
    public class AuthorTest
    {
        private DbContextOptions<LibraryContext> getDatabase(string DbName)
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
              .UseInMemoryDatabase(databaseName: DbName)
              .Options;
            return options;
        }

        [Fact]
        public async Task AddAuthorTest()
        {
            var options = getDatabase("AddAuthorDB");
            await using (var context = new LibraryContext(options))
            {
                var _manager = new LibraryManager(context);
                await _manager.AddAuthor(SingleAuthor());
                await context.SaveChangesAsync();
                Assert.Equal(1, await context.Authors.CountAsync());
                Assert.Equal("AuthorName", context.Authors.Single().Name);
            }
        }

        [Fact]
        public async Task GetBookByIdTest()
        {
            var options = getDatabase("GetAuthorDB");
            await using (var context = new LibraryContext(options))
            {
                var _manager = new LibraryManager(context);
                await _manager.AddAuthor(SingleAuthor());
                await context.SaveChangesAsync();
                var author = await _manager.GetAuthorById(1);
                Assert.Equal(1, author.Id);
                Assert.Equal("AuthorName", context.Authors.Single().Name);
            }
        }

        [Fact]
        public async Task GetListofAuthorsTest()
        {
            var options = getDatabase("GetListofAuthorsDB");

            await using (var context = new LibraryContext(options))
            {
                var manager = new LibraryManager(context);
                await context.Authors.AddRangeAsync(ListOfAuthors());
                await context.SaveChangesAsync();
                var books = await manager.GetAuthors();
                Assert.Equal(2, books.Count);
            }
        }

        [Fact]
        public async Task UpdateAuthor()
        {
            var options = getDatabase("UpdateAuthorDB");
            await using (var context = new LibraryContext(options))
            {
                var _manager = new LibraryManager(context);
                await _manager.AddAuthor(SingleAuthor());
                await context.SaveChangesAsync();
            }
            await using (var context = new LibraryContext(options))
            {
                var _manager = new LibraryManager(context);
                var checkUpdate = await _manager.UpdateAuthor(
                    new Author { Name = "AuthorName" }, 1);
                await context.SaveChangesAsync();
                Assert.Equal(1, checkUpdate);
                Assert.Equal("AuthorName", context.Authors.Single().Name);
            }
        }

        [Fact]
        public async Task DeleteAuthorTest()
        {
            var options = getDatabase("DeleteAuthorDB");

            await using (var context = new LibraryContext(options))
            {
                var manager = new LibraryManager(context);
                await manager.AddAuthor(SingleAuthor());
                await context.SaveChangesAsync();
                var checkDelete = await manager.DeleteAuthor(1);
                Assert.Equal(1, checkDelete);
            }
        }

        private Author SingleAuthor()
        {
            return
            new Author
            {
                Name = "AuthorName"
            };
        }

        private List<Author> ListOfAuthors()
        {
            return
            new List<Author>
            {
                new Author
                {
                Name = "AuthorName1"
                },
                new Author
                {
                Name = "AuthorName2"
                }
            };
        }
    }
}