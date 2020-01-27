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
        private LibraryContext context = new LibraryContext();

        public LibraryManager()
        {
        }

        public LibraryManager(LibraryContext context)
        {
            this.context = context;
        }

        public async Task<int> AddBook(Book book)
        {
            var author = await context.Authors.Where(a => a.Name.ToLower().Equals(book.Author.Name.Trim().ToLower())).FirstOrDefaultAsync();
            if (author != null)
            {
                book.Author = author;
            }
            await context.Books.AddAsync(book);
            return await context.SaveChangesAsync();
        }

        public async Task<int> AddBooks(StreamReader reader)
        {
            while (true)
            {
                string lines = reader.ReadLine();
                if (lines == null)
                {
                    break;
                }
                string[] line = lines.Trim().ToLower().Split(",");
                var author = await context.Authors.Where(a => a.Name.ToLower().Equals(line[2].ToLower().Trim()))
                                                  .FirstOrDefaultAsync();
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
            return await context.Books.Where(b => b.Id == id).Include(b => b.Author).FirstOrDefaultAsync();
        }

        public async Task<List<Book>> GetBooks()
        {
            return await context.Books.Include(b => b.Author).ToListAsync();
        }

        public async Task<List<Book>> SearchBooks(string searchQuery)
        {
            var books = await context.Books.Include(b => b.Author).ToListAsync();
            if (searchQuery != null)
                return books.Where(b => b.ISBN == searchQuery
                                            || b.Title == searchQuery
                                            || b.Author.Name == searchQuery).ToList();
            return books.ToList();
        }

        public async Task<Book> UpdateBook(Book book, int id)
        {
            var author = await context.Authors.Where(a => a.Name.ToLower().Equals(book.Author.Name.Trim().ToLower())).FirstOrDefaultAsync();
            if (author != null)
            {
                book.Author = author;
            }
            book.Id = id;
            context.Books.Update(book);
            await context.SaveChangesAsync();
            return await context.Books.Where(b => b.Id == id).Include(b => b.Author).FirstOrDefaultAsync();
        }

        public async Task<int> DeleteBook(int id)
        {
            var book = await context.Books.Where(b => b.Id == id).FirstOrDefaultAsync();
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
            return await context.Authors.Where(a => a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Author>> GetAuthors()
        {
            return await context.Authors.ToListAsync();
        }

        public async Task<Author> UpdateAuthor(Author author, int id)
        {
            author.Id = id;
            context.Authors.Update(author);
            await context.SaveChangesAsync();
            return await context.Authors.Where(a => a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> DeleteAuthor(int id)
        {
            var author = await context.Authors.Where(a => a.Id == id).FirstOrDefaultAsync();
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
                string[] line = lines.Trim().ToLower().Split(",");

                var borrower = await context.Borrowers.Where(b => b.CId.ToLower().Equals(line[0]))
                                                      .FirstOrDefaultAsync();
                if (borrower == null)
                {
                    await context.Borrowers.AddAsync(
                        new Borrower
                        {
                            CId = line[0],
                            Name = line[1],
                            Phone = line[2],
                            Email = line[3]
                        });
                    checkBorrowerIsAdded = await context.SaveChangesAsync(); // without using this line, it will not filter while adding new data from csv
                }
            }
            return checkBorrowerIsAdded;
        }

        public async Task<Borrower> GetBorrowerById(int id)
        {
            return await context.Borrowers.Where(a => a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Borrower>> GetBorrowers()
        {
            return await context.Borrowers.ToListAsync();
        }

        public async Task<int> UpdateBorrower(Borrower borrower, int id)
        {
            var borrowerExist = await context.Borrowers.Where(b => b.CId == borrower.CId).FirstOrDefaultAsync();
            if (borrowerExist == null)
            {
                borrower.Id = id;
                context.Borrowers.Update(borrower);
            }
            return await context.SaveChangesAsync();
        }

        public async Task<int> DeleteBorrower(int id)
        {
            var borrower = await context.Borrowers.Where(a => a.Id == id).FirstOrDefaultAsync();
            context.Borrowers.Remove(borrower);
            return await context.SaveChangesAsync();
        }

        public async Task<int> AddHistory(CheckInOutHistory history)
        {
            history.Book = await context.Books.Where(b => b.Id == history.Book.Id).Include(b => b.Author).FirstOrDefaultAsync();
            history.Borrower = await context.Borrowers.Where(b => b.Id == history.Borrower.Id).FirstOrDefaultAsync();
            await context.CheckInOutHistories.AddAsync(history);
            return await context.SaveChangesAsync();
        }

        public async Task<CheckInOutHistory> GetHistoryById(int id)
        {
            return await context.CheckInOutHistories.Where(h => h.Id == id).Include(h => h.Book).ThenInclude(b => b.Author).Include(h => h.Borrower).FirstOrDefaultAsync();
        }

        public async Task<List<CheckInOutHistory>> GetHistories()
        {
            return await context.CheckInOutHistories.Include(h => h.Book).ThenInclude(b => b.Author).Include(h => h.Borrower).ToListAsync();
        }

        public async Task<List<CheckInOutHistory>> SearchByStatus(bool status)
        {
            return await context.CheckInOutHistories.Where(h => h.Status == status)
                                                    .Include(h => h.Book)
                                                    .ThenInclude(b => b.Author)
                                                    .Include(h => h.Borrower)
                                                    .ToListAsync();
        }

        public async Task<int> UpdateHistory(CheckInOutHistory history, int id)
        {
            history.Id = id;
            history.Book = await context.Books.Where(b => b.Id == history.Book.Id).Include(b => b.Author).FirstOrDefaultAsync();
            history.Borrower = await context.Borrowers.Where(b => b.Id == history.Borrower.Id).FirstOrDefaultAsync();
            context.CheckInOutHistories.Update(history);
            return await context.SaveChangesAsync();
        }

        public async Task<int> DeleteHistory(int id)
        {
            var history = await context.CheckInOutHistories.Where(h => h.Id == id).Include(h => h.Book).Include(h => h.Borrower).FirstOrDefaultAsync();
            context.CheckInOutHistories.Remove(history);
            return await context.SaveChangesAsync();
        }
    }
}