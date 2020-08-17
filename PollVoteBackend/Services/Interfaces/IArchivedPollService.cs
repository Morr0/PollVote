using PollVoteBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollVoteBackend.Services.Interfaces
{
    public interface IArchivedPollService
    {
        Task<Poll> GetPoll(string id);
        Task PutPolls(IEnumerable<Poll> polls);
    }
}
