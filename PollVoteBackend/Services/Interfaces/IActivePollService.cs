using PollVoteBackend.Models;
using PollVoteBackend.Services.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollVoteBackend.Services.Interfaces
{
    public interface IActivePollService
    {
        void CreatePoll(Poll poll);
        bool DeletePoll(string id, string deleteToken);
        bool HasActivePoll(string id);
        Poll GetPoll(string id);
        bool Vote(string id, string choice);
        Dictionary<string, int> GetVotes(string id);
        bool HasExpiredPoll(string id);
        Poll GetExpiredPoll(string id);
        IEnumerable<PollVotesContainer> ExtractExpiredPolls();
        bool HasExpiredPolls();
    }
}
