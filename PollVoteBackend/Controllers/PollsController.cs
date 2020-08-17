﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            Poll poll = null;
            if (_activePollService.HasActivePoll(id))
            {
                poll = _activePollService.GetPoll(id);
            } else
            {
                poll = await _archivedPollService.GetPoll(id);
            }

            if (poll == null)
                return NotFound();

            PollReadDTO dto = _mapper.FromPollToReadDTO(poll);
            return Ok(dto);
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
    }
}
