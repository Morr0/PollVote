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

        public Poll GetPoll(string id)
        {
            throw new NotImplementedException();
        }

        public void PutPolls(IEnumerable<Poll> polls)
        {
            throw new NotImplementedException();
        }
    }
}
