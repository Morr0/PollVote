using PollVoteBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollVoteBackend.Services.Interfaces
{
    public interface IArchivedPollService
    {
        void GetPoll(string id);
        void PutPolls(IEnumerable<Poll> polls);
    }
}
