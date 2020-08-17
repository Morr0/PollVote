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
        private Dictionary<string, PollVotesContainer> expiredPolls;

        public ActivePollService()
        {
            activePolls = new Dictionary<string, PollVotesContainer>();
            expiredPolls = new Dictionary<string, PollVotesContainer>();
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

        public bool HasActivePoll(string id)
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
            {
                // Check that it is not expired
                if (expiredPolls.ContainsKey(id))
                    throw new PollHasExpiredException();

                throw new NoPollException();
            }

            PollVotesContainer pvc = activePolls[id];
            if (!pvc.ChoiceNumbers.ContainsKey(choice))
                return false;

            pvc.ChoiceNumbers[choice]++;
            pvc.CurrentVotes++;

            // When it is about to expire
            if (pvc.CurrentVotes == pvc.Poll.ExpiresOnChoices)
            {
                // Move from active to expired
                expiredPolls.Add(pvc.Poll.Id, pvc);
                activePolls.Remove(pvc.Poll.Id);
            }

            return true;
        }

        public Dictionary<string, int> GetVotes(string id)
        {
            if (!activePolls.ContainsKey(id))
                throw new NoPollException();

            return activePolls[id].ChoiceNumbers;
         }

        public bool HasExpiredPoll(string id)
        {
            return expiredPolls.ContainsKey(id);
        }

        public Poll GetExpiredPoll(string id)
        {
            return expiredPolls[id].Poll;
        }

        public IEnumerable<PollVotesContainer> ExtractExpiredPolls()
        {
            if (expiredPolls.Count == 0)
                throw new NoPollException();

            IEnumerable<PollVotesContainer> pvcs = expiredPolls.Values;

            expiredPolls.Clear();

            return pvcs;
        }

        public bool HasExpiredPolls()
        {
            return expiredPolls.Count > 0;
        }
    }
}
