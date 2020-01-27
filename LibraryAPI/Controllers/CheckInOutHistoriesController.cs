using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LibraryModels;
using LibraryRepository;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace LibraryAPI.Controllers
{
    [ApiController]
    public class CheckInOutHistoriesController : Controller
    {
        private readonly ILibraryManager _librarymanager;
        private readonly IMapper _mapper;

        public CheckInOutHistoriesController(ILibraryManager librarymanager, IMapper mapper)
        {
            _librarymanager = librarymanager ?? throw new ArgumentNullException(nameof(librarymanager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Return Single History By Id.
        /// </summary>
        [HttpGet("api/histories/{id}")]
        public async Task<ActionResult<CheckInOutHistoryDTOForGet>> GetHistoryById(int id)
        {
            var history = await _librarymanager.GetHistoryById(id);
            if (history != null)
                return Ok(_mapper.Map<CheckInOutHistoryDTOForGet>(history));
            return NotFound();
        }

        /// <summary>
        /// Return All History.
        /// </summary>
        [HttpGet("api/histories/")]
        public async Task<ActionResult<List<CheckInOutHistoryDTOForGet>>> GetHistories()
        {
            var histories = await _librarymanager.GetHistories();
            if (histories != null)
                return Ok(_mapper.Map<List<CheckInOutHistoryDTOForGet>>(histories));
            return NotFound();
        }

        /// <summary>
        /// Search by Status. ('True' means Books are issued.)
        /// </summary>
        [HttpGet("api/histories/status/{status}")]
        public async Task<ActionResult<List<CheckInOutHistory>>> SearchByStatus(bool status)
        {
            var histories = await _librarymanager.SearchByStatus(status);
            if (histories != null)
                return Ok(histories);
            return NotFound();
        }

        /// <summary>
        /// Search History By Borrower Name or CID.
        /// </summary>
        [HttpGet("api/histories/query/{searchQuery}")]
        public async Task<ActionResult<List<CheckInOutHistory>>> GetBySearchQuery(string searchQuery)
        {
            var histories = await _librarymanager.GetBySearchQuery(searchQuery);
            if (histories.Count() > 0)
                return Ok(histories);
            return NotFound();
        }

        /// <summary>
        /// Issue New Book or (Add History).
        /// </summary>
        [HttpPost("api/histories")]
        public async Task<ActionResult<CheckInOutHistory>> AddHistory([FromBody] CheckInOutHistoryDTO historyDTO)
        {
            var history = _mapper.Map<CheckInOutHistory>(historyDTO);
            var checkAdd = await _librarymanager.AddHistory(history);
            if (checkAdd != 0)
                return Ok();
            return NotFound();
        }

        /// <summary>
        /// Update Book.
        /// </summary>
        [HttpPut("api/histories/{id}")]
        public async Task<ActionResult<Author>> UpdateHistory([FromBody] CheckInOutHistoryDTO historyDTO, int id)
        {
            var history = _mapper.Map<CheckInOutHistory>(historyDTO);
            var updatedHistory = await _librarymanager.UpdateHistory(history, id);
            if (updatedHistory != 0)
                return Ok();
            return NotFound();
        }

        /// <summary>
        /// Delete History By Id.
        /// </summary>
        [HttpDelete("api/histories/{id}")]
        public async Task<ActionResult> DeleteHistory(int id)
        {
            var checkDelete = await _librarymanager.DeleteHistory(id);
            if (checkDelete != 0)
                return Ok();
            return NotFound();
        }
    }
}