using PollVoteBackend.Models;
using PollVoteBackend.Services.Containers;
using PollVoteBackend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollVoteBackend.Services
{
    public class ActivePollService : IActivePollService
    {
        private Dictionary<string, PollVotesContainer> activePolls;

        public ActivePollService()
        {
            activePolls = new Dictionary<string, PollVotesContainer>();
        }

        public void CreatePoll(Poll poll)
        {
            PollVotesContainer pvc = new PollVotesContainer
            {
                Poll = poll
            };
            activePolls.Add(poll.Id, pvc);
        }

        public bool DeletePoll(string id, string deleteToken)
        {
            var poll = activePolls[id].Poll;
            if (poll.DeleteToken == deleteToken)
            {
                activePolls.Remove(id);
                return true;
            }

            return false;
        }

        public Poll GetPoll(string id)
        {
            return activePolls[id].Poll;
        }

        public bool HasPoll(string id)
        {
            return activePolls.ContainsKey(id);
        }
    }
}
