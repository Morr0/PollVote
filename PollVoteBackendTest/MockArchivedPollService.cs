using PollVoteBackend.Models;
using PollVoteBackend.Services.Events;
using PollVoteBackend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PollVoteBackendTest
{
    internal class MockArchivedPollService : IArchivedPollService
    {
        private Dictionary<string, Poll> _polls;

        public MockArchivedPollService()
        {
            _polls = new Dictionary<string, Poll>();
        }

        public async Task<Poll> GetPoll(string id)
        {
            return _polls[id];
        }

        public void OnPollExpiry(object sender, PollExpiryEventArgs args)
        {
            Poll poll = args.PVC.Poll;
            if (!_polls.ContainsKey(poll.Id))
                _polls.Add(poll.Id, poll);
        }

        public Task PutPolls(IEnumerable<Poll> polls)
        {
            throw new NotImplementedException();
        }
    }
}
