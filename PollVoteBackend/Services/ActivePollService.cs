using PollVoteBackend.Models;
using PollVoteBackend.Services.Containers;
using PollVoteBackend.Services.Events;
using PollVoteBackend.Services.Exceptions;
using PollVoteBackend.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace PollVoteBackend.Services
{
    public class ActivePollService : IActivePollService
    {
        private Dictionary<string, PollVotesContainer> _activePolls;

        public ActivePollService()
        {
            _activePolls = new Dictionary<string, PollVotesContainer>();
        }

        public event EventHandler<PollExpiryEventArgs> PollExpirySystem;
        //public delegate void PollExpirySystem(PollVotesContainer pvc);  

        public void CreatePoll(Poll poll)
        {
            PollVotesContainer pvc = new PollVotesContainer(poll);
            _activePolls.Add(poll.Id, pvc);
        }

        public bool DeletePoll(string id, string deleteToken)
        {
            var poll = _activePolls[id].Poll;
            if (poll.DeleteToken == deleteToken)
            {
                _activePolls.Remove(id);
                return true;
            }

            return false;
        }

        public Poll GetPoll(string id)
        {
            return _activePolls[id].Poll;
        }

        public bool HasPoll(string id)
        {
            return _activePolls.ContainsKey(id);
        }

        /// <returns>False when the choice does not exist</returns>
        public bool Vote(string id, string choice)
        {
            if (!_activePolls.ContainsKey(id))
                throw new NoPollException();

            PollVotesContainer pvc = _activePolls[id];
            if (!pvc.ChoiceNumbers.ContainsKey(choice))
                return false;

            vote(pvc, choice);
            checkForExpiryThenPublishIfSo(pvc);

            return true;
        }

        // A one place for voting
        private void vote(PollVotesContainer pvc, string choice)
        {
            pvc.ChoiceNumbers[choice]++;
            pvc.CurrentVotes++;

            int i = pvc.ChoiceIndexToChoice[choice];
            pvc.Poll.ChoicesAnswers[i]++;
        }

        private void checkForExpiryThenPublishIfSo(PollVotesContainer pvc)
        {
            // When it is about to expire
            if (pvc.CurrentVotes == pvc.Poll.ExpiresOnChoices)
            {
                _activePolls.Remove(pvc.Poll.Id);

                // Notify
                if (PollExpirySystem != null)
                {
                    PollExpirySystem.Invoke(this, new PollExpiryEventArgs
                    {
                        PVC = pvc
                    });
                }
            }
        }

        public Dictionary<string, int> GetVotes(string id)
        {
            if (!_activePolls.ContainsKey(id))
                throw new NoPollException();

            return _activePolls[id].ChoiceNumbers;
         }
    }
}
