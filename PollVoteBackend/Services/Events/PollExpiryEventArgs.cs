using PollVoteBackend.Services.Containers;
using System;

namespace PollVoteBackend.Services.Events
{
    public class PollExpiryEventArgs : EventArgs
    {
        public PollVotesContainer PVC { get; set; }
    }
}
