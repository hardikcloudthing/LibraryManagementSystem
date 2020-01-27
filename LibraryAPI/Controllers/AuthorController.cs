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
    public class AuthorController : Controller
    {
        private readonly ILibraryManager _librarymanager;
        private readonly IMapper _mapper;

        public AuthorController(ILibraryManager librarymanager, IMapper mapper)
        {
            _librarymanager = librarymanager ?? throw new ArgumentNullException(nameof(librarymanager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Return Single Author by Id.
        /// </summary>
        [HttpGet("api/authors/{id}")]
        public async Task<ActionResult<Author>> GetAuthorById(int id)
        {
            var author = await _librarymanager.GetAuthorById(id);
            if (author != null)
                return Ok(author);
            return NotFound();
        }

        /// <summary>
        /// Return All Authors.
        /// </summary>
        [HttpGet("api/authors/")]
        public async Task<ActionResult<Author>> GetAuthors()
        {
            var authors = await _librarymanager.GetAuthors();
            if (authors != null)
                return Ok(authors);
            return NotFound();
        }

        /// <summary>
        /// Add Author.
        /// </summary>
        [HttpPost("api/authors")]
        public async Task<ActionResult<Author>> AddAuthor([FromBody] Author author)
        {
            var checkAdd = await _librarymanager.AddAuthor(author);
            if (checkAdd != 0)
                return Ok();
            return NotFound();
        }

        /// <summary>
        /// Update Author by Id.
        /// </summary>
        [HttpPut("api/authors/{id}")]
        public async Task<ActionResult<Author>> UpdateAuthor([FromBody] Author author, int id)
        {
            var updatedAuthor = await _librarymanager.UpdateAuthor(author, id);
            if (updatedAuthor != null)
                return Ok();
            return NotFound();
        }

        /// <summary>
        /// Delete Author By Id.
        /// </summary>
        [HttpDelete("api/authors/{id}")]
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            var checkDelete = await _librarymanager.DeleteAuthor(id);
            if (checkDelete != 0)
                return Ok();
            return NotFound();
        }
    }
}