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
    public class CheckInOutHistoriesController : Controller
    {
        private readonly ILibraryManager _librarymanager;
        private readonly IMapper _mapper;

        public CheckInOutHistoriesController(ILibraryManager librarymanager, IMapper mapper)
        {
            _librarymanager = librarymanager ?? throw new ArgumentNullException(nameof(librarymanager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("api/histories/{id}")]
        public async Task<ActionResult<CheckInOutHistory>> GetHistoryById(int id)
        {
            var history = await _librarymanager.GetHistoryById(id);
            if (history != null)
                return Ok(history);
            return NotFound();
        }

        [HttpGet("api/histories/")]
        public async Task<ActionResult<CheckInOutHistory>> GetHistories()
        {
            var histories = await _librarymanager.GetHistories();
            if (histories != null)
                return Ok(histories);
            return NotFound();
        }

        [HttpGet("api/histories/status/{status}")]
        public async Task<ActionResult<List<CheckInOutHistory>>> SearchByStatus(bool status)
        {
            var histories = await _librarymanager.SearchByStatus(status);
            if (histories != null)
                return Ok(histories);
            return NotFound();
        }

        [HttpPost("api/histories")]
        public async Task<ActionResult<CheckInOutHistory>> AddHistory([FromBody] CheckInOutHistoryDTO historyDTO)
        {
            var history = _mapper.Map<CheckInOutHistory>(historyDTO);
            var checkAdd = await _librarymanager.AddHistory(history);
            if (checkAdd != 0)
                return Ok();
            return NotFound();
        }

        [HttpPut("api/histories/{id}")]
        public async Task<ActionResult<Author>> UpdateHistory([FromBody] CheckInOutHistoryDTO historyDTO, int id)
        {
            var history = _mapper.Map<CheckInOutHistory>(historyDTO);
            var updatedHistory = await _librarymanager.UpdateHistory(history, id);
            if (updatedHistory != 0)
                return Ok();
            return NotFound();
        }

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