using LibraryDataContext;
using LibraryModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryRepository
{
    public class LibraryManager : ILibraryManager
    {
        private LibraryContext context;

        public LibraryManager(LibraryContext context)
        {
            this.context = context;
        }

        public async Task<int> AddBook(Book book)
        {
            var author = await context.Authors.FirstOrDefaultAsync(a => a.Name != null && a.Name.Equals(book.Author.Name));

            if (author != null)
            {
                book.Author = author;
            }
            await context.Books.AddAsync(book);
            return await context.SaveChangesAsync();
        }

        public async Task<int> AddBooks(StreamReader reader)
        {
            string lines = await reader.ReadLineAsync();
            while (true)
            {
                lines = await reader.ReadLineAsync();
                if (lines == null)
                {
                    break;
                }
                string[] line = lines.ToLower().Split(",");
                var author = await context.Authors.FirstOrDefaultAsync(a => a.Name.ToLower().Equals(line[2].Trim()));

                if (author != null)
                {
                    await context.Books.AddAsync(
                        new Book
                        {
                            ISBN = line[0],
                            Title = line[1],
                            Author = author
                        });
                }
                else
                {
                    await context.Books.AddAsync(
                        new Book
                        {
                            ISBN = line[0],
                            Title = line[1],
                            Author = new Author { Name = line[2] }
                        });
                }
            }

            return await context.SaveChangesAsync();
        }

        public async Task<Book> GetBookById(int id)
        {
            return await context.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<Book>> GetBooks()
        {
            return await context.Books.Include(b => b.Author).ToListAsync();
        }

        public async Task<List<Book>> SearchBooks(string searchQuery)
        {
            var books = await context.Books.Include(b => b.Author).ToListAsync();
            if (searchQuery == null)
                return books.ToList();

            return books.Where(b => b.ISBN.Equals(searchQuery, StringComparison.InvariantCultureIgnoreCase)
                                || b.Title.Equals(searchQuery, StringComparison.InvariantCultureIgnoreCase)
                                || b.Author.Name.Equals(searchQuery, StringComparison.InvariantCultureIgnoreCase))
                                .ToList();
        }

        public async Task<int> UpdateBook(Book book, int id)
        {
            var author = await context.Authors.FirstOrDefaultAsync(a => a.Name.Equals(book.Author.Name, StringComparison.InvariantCultureIgnoreCase));
            if (author != null)
            {
                book.Author = author;
            }
            book.Id = id;
            context.Books.Update(book);
            return await context.SaveChangesAsync();
        }

        public async Task<int> DeleteBook(int id)
        {
            var book = await context.Books.FirstOrDefaultAsync(b => b.Id == id);
            context.Books.Remove(book);
            return await context.SaveChangesAsync();
        }

        public async Task<int> AddAuthor(Author author)
        {
            await context.Authors.AddAsync(author);
            return await context.SaveChangesAsync();
        }

        public async Task<Author> GetAuthorById(int id)
        {
            return await context.Authors.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<Author>> GetAuthors()
        {
            return await context.Authors.ToListAsync();
        }

        public async Task<int> UpdateAuthor(Author author, int id)
        {
            author.Id = id;
            context.Authors.Update(author);
            return await context.SaveChangesAsync();
        }

        public async Task<int> DeleteAuthor(int id)
        {
            var author = await context.Authors.FirstOrDefaultAsync(a => a.Id == id);
            context.Authors.Remove(author);
            return await context.SaveChangesAsync();
        }

        public async Task<int> AddBorrower(Borrower borrower)
        {
            await context.Borrowers.AddAsync(borrower);
            return await context.SaveChangesAsync();
        }

        public async Task<int> AddBorrowers(StreamReader reader)
        {
            string lines = await reader.ReadLineAsync();
            int checkBorrowerIsAdded = 0;
            while (true)
            {
                lines = await reader.ReadLineAsync();
                if (lines == null)
                {
                    break;
                }
                string[] line = lines.ToLower().Split(",");

                await context.Borrowers.AddAsync(
                    new Borrower
                    {
                        Name = line[0],
                        Phone = line[1],
                        Email = line[2]
                    });
                checkBorrowerIsAdded = await context.SaveChangesAsync(); // without using this line, it will not filter while adding new data from csv
            }
            return checkBorrowerIsAdded;
        }

        public async Task<Borrower> GetBorrowerById(int id)
        {
            return await context.Borrowers.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<Borrower>> GetBorrowers()
        {
            return await context.Borrowers.ToListAsync();
        }

        public async Task<int> UpdateBorrower(Borrower borrower, int id)
        {
            borrower.Id = id;
            context.Borrowers.Update(borrower);
            return await context.SaveChangesAsync();
        }

        public async Task<int> DeleteBorrower(int id)
        {
            var borrower = await context.Borrowers.FirstOrDefaultAsync(a => a.Id == id);
            context.Borrowers.Remove(borrower);
            return await context.SaveChangesAsync();
        }

        public async Task<int> AddHistory(CheckInOutHistory history)
        {
            history.Book = await context.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == history.Book.Id);
            history.Borrower = await context.Borrowers.FirstOrDefaultAsync(b => b.Id == history.Borrower.Id);
            await context.CheckInOutHistories.AddAsync(history);
            return await context.SaveChangesAsync();
        }

        public async Task<CheckInOutHistory> GetHistoryById(int id)
        {
            return await context.CheckInOutHistories.Include(h => h.Book)
                                                    .ThenInclude(b => b.Author)
                                                    .Include(h => h.Borrower)
                                                    .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<List<CheckInOutHistory>> GetHistories()
        {
            return await context.CheckInOutHistories.Include(h => h.Book)
                                                    .ThenInclude(b => b.Author)
                                                    .Include(h => h.Borrower)
                                                    .ToListAsync();
        }

        public async Task<List<CheckInOutHistory>> SearchByStatus(bool status)
        {
            return await context.CheckInOutHistories.Where(h => h.Status == status)
                                                    .Include(h => h.Book)
                                                    .ThenInclude(b => b.Author)
                                                    .Include(h => h.Borrower)
                                                    .ToListAsync();
        }

        public async Task<List<CheckInOutHistory>> GetBySearchQuery(string searchQuery)
        {
            var histories = await context.CheckInOutHistories.Include(h => h.Book)
                                                             .ThenInclude(b => b.Author)
                                                             .Include(h => h.Borrower)
                                                             .ToListAsync();
            if (searchQuery == null)
                return histories.ToList();

            return histories.Where(h => h.Borrower.Name.Equals(searchQuery, StringComparison.InvariantCultureIgnoreCase))
                                                .ToList();
        }

        public async Task<int> UpdateHistory(CheckInOutHistory history, int id)
        {
            history.Id = id;
            history.Book = await context.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == history.Book.Id);
            history.Borrower = await context.Borrowers.FirstOrDefaultAsync(b => b.Id == history.Borrower.Id);
            context.CheckInOutHistories.Update(history);
            return await context.SaveChangesAsync();
        }

        public async Task<int> DeleteHistory(int id)
        {
            var history = await context.CheckInOutHistories.FirstOrDefaultAsync(h => h.Id == id);
            context.CheckInOutHistories.Remove(history);
            return await context.SaveChangesAsync();
        }
    }
}