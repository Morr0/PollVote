using Microsoft.EntityFrameworkCore;
using PollVoteBackend.Data;
using PollVoteBackend.Models;
using PollVoteBackend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollVoteBackend.Services
{
    public class ArchivedPollService : IArchivedPollService
    {
        private PollsContext _context;

        public ArchivedPollService(PollsContext context)
        {
            _context = context;
        }

        public async Task<Poll> GetPoll(string id)
        {
            return await _context.Poll.AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task PutPolls(IEnumerable<Poll> polls)
        {
            await _context.AddRangeAsync(polls);
            await _context.SaveChangesAsync();
        }
    }
}
