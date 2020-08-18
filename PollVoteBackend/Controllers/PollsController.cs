using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PollVoteBackend.Controllers.Queries;
using PollVoteBackend.Models;
using PollVoteBackend.Services.Interfaces;

namespace PollVoteBackend.Controllers
{
    [Route("api/poll")]
    [ApiController]
    public class PollsController : ControllerBase
    {
        private IActivePollService _activePollService;
        private IArchivedPollService _archivedPollService;
        private IMapper _mapper;

        public PollsController(IActivePollService active, IArchivedPollService archived,
            IMapper mapper)
        {
            _activePollService = active;
            _archivedPollService = archived;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            Poll poll = await getPoll(id);
            if (poll == null)
                return NotFound();

            PollReadDTO dto = _mapper.FromPollToReadDTO(poll);
            return Ok(dto);
        }

        private async Task<Poll> getPoll(string id)
        {
            if (_activePollService.HasPoll(id))
            {
                return _activePollService.GetPoll(id);
            }
            else
            {
                return await _archivedPollService.GetPoll(id);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PollWriteDTO __poll)
        {
            Poll poll = _mapper.Map<Poll>(__poll);
            _activePollService.CreatePoll(poll);

            PollReadDTO dto = _mapper.FromPollToReadDTO
                (_activePollService.GetPoll(poll.Id));
            return Ok(dto);
        }

        [HttpPut("vote/{id}")]
        public async Task<IActionResult> Vote([FromRoute] string id, [FromQuery] VotingQuery query)
        {
            if (_activePollService.HasPoll(id))
            Console.WriteLine("Has id");
            Poll poll = await getPoll(id);
            if (poll == null)
                return NotFound();

            if (!string.IsNullOrEmpty(poll.EndedOn))
                return BadRequest();

            // Vote now
            bool result = _activePollService.Vote(id, query.Choice);
            if (!result)
                return Conflict();

            PollReadDTO dto = _mapper.FromPollToReadDTO(poll);
            return Ok(dto);
        }
    }
}
