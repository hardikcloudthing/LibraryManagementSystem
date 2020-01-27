using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LibraryModels;
using LibraryRepository;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace LibraryAPI.Controllers
{
    [ApiController]
    public class BookController : Controller
    {
        private readonly ILibraryManager _librarymanager;
        private readonly IMapper _mapper;

        public BookController(ILibraryManager librarymanager, IMapper mapper)
        {
            _librarymanager = librarymanager ?? throw new ArgumentNullException(nameof(librarymanager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("api/books/{id}")]
        public async Task<ActionResult<BookDTO>> GetBookById(int id)
        {
            var book = await _librarymanager.GetBookById(id);
            if (book != null)
                return Ok(_mapper.Map<BookDTO>(book));
            return NotFound();
        }

        [HttpGet("api/books/")]
        public async Task<ActionResult<List<BookDTO>>> GetBooks()
        {
            var books = await _librarymanager.GetBooks();
            if (books != null)
                return Ok(_mapper.Map<List<BookDTO>>(books));
            return NotFound();
        }

        [HttpGet("api/books/query/{searchQuery}")]
        public async Task<ActionResult<List<BookDTO>>> SearchBooks(string searchQuery)
        {
            var books = await _librarymanager.SearchBooks(searchQuery);
            if (books != null)
                return Ok(_mapper.Map<List<BookDTO>>(books));
            return NotFound();
        }

        [HttpPost("api/books")]
        public async Task<ActionResult<BookDTO>> AddBook([FromBody] BookDTO bookDTO)
        {
            var book = _mapper.Map<Book>(bookDTO);
            var checkAdd = await _librarymanager.AddBook(book);
            if (checkAdd != 0)
                return Ok(_mapper.Map<BookDTO>(book));
            return NotFound();
        }

        [HttpPost("api/books/csv")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> Uploads([FromForm]IFormFile file)
        {
            StreamReader reader = new StreamReader(file.OpenReadStream());
            var checkUpload = await _librarymanager.AddBooks(reader);
            if (checkUpload != 0)
                return Ok();
            return NotFound();
        }

        [HttpPut("api/books/{id}")]
        public async Task<ActionResult<Book>> UpdateAuthor([FromBody] BookDTO bookDTO, int id)
        {
            var book = _mapper.Map<Book>(bookDTO);
            var updatedBook = await _librarymanager.UpdateBook(book, id);
            if (updatedBook != null)
                return Ok(updatedBook);
            return NotFound();
        }

        [HttpDelete("api/books/{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var checkDelete = await _librarymanager.DeleteBook(id);
            if (checkDelete != 0)
                return Ok();
            return NotFound();
        }
    }
}