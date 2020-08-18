using PollVoteBackend.Models;
using PollVoteBackend.Services.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollVoteBackend.Services.Interfaces
{
    public interface IArchivedPollService
    {
        Task<Poll> GetPoll(string id);

        // Event handler when a poll has expired
        void OnPollExpiry(object sender, PollExpiryEventArgs args);
    }
}
