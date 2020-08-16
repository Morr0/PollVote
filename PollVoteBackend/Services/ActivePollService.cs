using PollVoteBackend.Models;
using PollVoteBackend.Services.Containers;
using PollVoteBackend.Services.Exceptions;
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
            PollVotesContainer pvc = new PollVotesContainer(poll);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="choice"></param>
        /// <returns>False when the choice does not exist</returns>
        public bool Vote(string id, string choice)
        {
            if (!activePolls.ContainsKey(id))
                throw new NoPollException();

            PollVotesContainer pvc = activePolls[id];
            if (!pvc.ChoiceNumbers.ContainsKey(choice))
                return false;

            pvc.ChoiceNumbers[choice]++;
            return true;
        }

        public Dictionary<string, int> GetVotes(string id)
        {
            if (!activePolls.ContainsKey(id))
                throw new NoPollException();

            return activePolls[id].ChoiceNumbers;
         }
    }
}
