﻿using System;
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
    [Produces("application/json")]
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

        /// <summary>
        /// Return Single Borrower by Id.
        /// </summary>
        [HttpGet("api/borrowers/{id}")]
        public async Task<ActionResult<Borrower>> GetBorrowerById(int id)
        {
            var borrower = await _librarymanager.GetBorrowerById(id);
            if (borrower == null)
                return NotFound();
            return Ok(borrower);
        }

        /// <summary>
        /// Return All Borrowers.
        /// </summary>
        [HttpGet("api/borrowers/")]
        public async Task<ActionResult<Borrower>> GetBorrowers()
        {
            var borrowers = await _librarymanager.GetBorrowers();
            if (borrowers == null)
                return NotFound();
            return Ok(borrowers);
        }

        /// <summary>
        /// Add Borrower.
        /// </summary>
        [HttpPost("api/borrowers")]
        public async Task<ActionResult<Borrower>> AddBorrower([FromBody] Borrower borrower)
        {
            var checkAdd = await _librarymanager.AddBorrower(borrower);
            if (checkAdd == 0)
                return NotFound();
            return Ok();
        }

        /// <summary>
        /// Add CSV File of Borrowers.
        /// </summary>
        [HttpPost("api/borrowers/csv")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> Uploads([FromForm]IFormFile file)
        {
            StreamReader reader = new StreamReader(file.OpenReadStream());
            var checkUpload = await _librarymanager.AddBorrowers(reader);
            if (checkUpload == 0)
                return NotFound();
            return Ok();
        }

        /// <summary>
        /// Update Borrower.
        /// </summary>
        [HttpPut("api/borrowers/{id}")]
        public async Task<ActionResult<Borrower>> UpdateBorrower([FromBody] Borrower borrower, int id)
        {
                if (await _librarymanager.UpdateBorrower(borrower, id) == 0)
                    return NotFound();
            return Ok();
        }

        /// <summary>
        /// Delete Borrower By Id.
        /// </summary>
        [HttpDelete("api/borrowers/{id}")]
        public async Task<ActionResult> DeleteBorrower(int id)
        {
            var checkDelete = await _librarymanager.DeleteBorrower(id);
            if (checkDelete == 0)
                return NotFound();
            return Ok();
        }
    }
}