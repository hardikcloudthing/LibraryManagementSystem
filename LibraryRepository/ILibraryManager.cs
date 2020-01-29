using LibraryModels;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace LibraryRepository
{
    public interface ILibraryManager
    {
        Task<int> AddBook(Book book);

        Task<int> AddBooks(StreamReader reader);

        Task<Book> GetBookById(int id);

        Task<List<Book>> GetBooks();

        Task<List<Book>> SearchBooks(string searchQuery);

        Task<int> UpdateBook(Book book, int id);

        Task<int> DeleteBook(int id);

        Task<int> AddAuthor(Author book);

        Task<Author> GetAuthorById(int id);

        Task<List<Author>> GetAuthors();

        Task<int> UpdateAuthor(Author author, int id);

        Task<int> DeleteAuthor(int id);

        Task<int> AddBorrower(Borrower borrower);

        Task<int> AddBorrowers(StreamReader reader);

        Task<Borrower> GetBorrowerById(int id);

        Task<List<Borrower>> GetBorrowers();

        Task<int> UpdateBorrower(Borrower borrower, int id);

        Task<int> DeleteBorrower(int id);

        Task<int> AddHistory(CheckInOutHistory history);

        Task<CheckInOutHistory> GetHistoryById(int id);

        Task<List<CheckInOutHistory>> GetHistories();

        Task<List<CheckInOutHistory>> SearchByStatus(bool status);

        Task<List<CheckInOutHistory>> GetBySearchQuery(string searchQuery);

        Task<int> UpdateHistory(CheckInOutHistory history, int id);

        Task<int> DeleteHistory(int id);
    }
}