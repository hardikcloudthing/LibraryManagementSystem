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
    public class BorrowerController : Controller
    {
        private readonly ILibraryManager _librarymanager;
        private readonly IMapper _mapper;

        public BorrowerController(ILibraryManager librarymanager, IMapper mapper)
        {
            _librarymanager = librarymanager ?? throw new ArgumentNullException(nameof(librarymanager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("api/borrowers/{id}")]
        public async Task<ActionResult<Borrower>> GetBorrowerById(int id)
        {
            var borrower = await _librarymanager.GetBorrowerById(id);
            if (borrower != null)
                return Ok(borrower);
            return NotFound();
        }

        [HttpGet("api/borrowers/")]
        public async Task<ActionResult<Borrower>> GetBorrowers()
        {
            var borrowers = await _librarymanager.GetBorrowers();
            if (borrowers != null)
                return Ok(borrowers);
            return NotFound();
        }

        [HttpPost("api/borrowers")]
        public async Task<ActionResult<Borrower>> AddBorrower([FromBody] Borrower borrower)
        {
            var checkAdd = await _librarymanager.AddBorrower(borrower);
            if (checkAdd != 0)
                return Ok();
            return NotFound();
        }

        [HttpPost("api/borrowers/csv")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> Uploads([FromForm]IFormFile file)
        {
            StreamReader reader = new StreamReader(file.OpenReadStream());
            var checkUpload = await _librarymanager.AddBorrowers(reader);
            if (checkUpload != 0)
                return Ok();
            return NotFound();
        }

        [HttpPut("api/borrowers/{id}")]
        public async Task<ActionResult<Borrower>> UpdateBorrower([FromBody] Borrower borrower, int id)
        {
            var updatedBorrower = await _librarymanager.UpdateBorrower(borrower, id);
            if (updatedBorrower != 0)
                return Ok();
            return NotFound();
        }

        [HttpDelete("api/borrowers/{id}")]
        public async Task<ActionResult> DeleteBorrower(int id)
        {
            var checkDelete = await _librarymanager.DeleteBorrower(id);
            if (checkDelete != 0)
                return Ok();
            return NotFound();
        }
    }
}