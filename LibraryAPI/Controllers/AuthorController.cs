using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LibraryModels;
using LibraryRepository;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Http;
using EO.Internal;

namespace LibraryAPI.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class AuthorController : Controller
    {
        private readonly ILibraryManager _librarymanager;
        //private readonly IMapper _mapper;

        public AuthorController(ILibraryManager librarymanager, IMapper mapper)
        {
            _librarymanager = librarymanager ?? throw new ArgumentNullException(nameof(librarymanager));
            //_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Return Single Author by Id.
        /// </summary>
        /// <return>id</return>>
        [HttpGet("api/authors/{id}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        public async Task<ActionResult<Author>> GetAuthorById(int id)
        {
            var author = await _librarymanager.GetAuthorById(id);
            if (author == null)
                return NotFound();
            return Ok(author);
        }

        /// <summary>
        /// Return All Authors.
        /// </summary>
        [HttpGet("api/authors/")]
        public async Task<ActionResult<Author>> GetAuthors()
        {
            var authors = await _librarymanager.GetAuthors();
            if (authors == null)
                return NotFound();
            return Ok(authors);
        }

        /// <summary>
        /// Add Author.
        /// </summary>
        [HttpPost("api/authors")]
        public async Task<ActionResult<Author>> AddAuthor([FromBody] Author author)
        {
            var checkAdd = await _librarymanager.AddAuthor(author);
            if (checkAdd == 0)
                return NotFound();
            return Ok();
        }

        /// <summary>
        /// Update Author by Id.
        /// </summary>
        [HttpPut("api/authors/{id}")]
        public async Task<ActionResult<Author>> UpdateAuthor([FromBody] Author author, int id)
        {
            var updatedAuthor = await _librarymanager.UpdateAuthor(author, id);
            if (updatedAuthor == 0)
                return NotFound();
            return Ok();
        }

        /// <summary>
        /// Delete Author By Id.
        /// </summary>
        [HttpDelete("api/authors/{id}")]
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            var checkDelete = await _librarymanager.DeleteAuthor(id);
            if (checkDelete == 0)
                return NotFound();
            return Ok();
        }
    }
}