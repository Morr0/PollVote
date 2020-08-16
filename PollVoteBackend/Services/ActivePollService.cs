using PollVoteBackend.Models;
using PollVoteBackend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollVoteBackend.Services
{
    public class ActivePollService : IActivePollService
    {
        private Dictionary<string, Poll> activePolls;

        public ActivePollService()
        {
            activePolls = new Dictionary<string, Poll>();
        }

        public void CreatePoll(Poll poll)
        {
            activePolls.Add(poll.Id, poll);
        }

        public bool DeletePoll(string id, string deleteToken)
        {
            if (activePolls[id].DeleteToken == deleteToken)
            {
                activePolls.Remove(id);
                return true;
            }

            return false;
        }

        public Poll GetPoll(string id)
        {
            return activePolls[id];
        }

        public bool HasPoll(string id)
        {
            return activePolls.ContainsKey(id);
        }
    }
}
