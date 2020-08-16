using PollVoteBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollVoteBackend.Services.Containers
{
    /// <summary>
    /// Internal use only. To hold votes for each poll
    /// </summary>
    internal class PollVotesContainer
    {
        public Poll Poll;
        public Dictionary<string, int> ChoiceNumbers;

        public PollVotesContainer()
        {
            ChoiceNumbers = new Dictionary<string, int>();
        }
    }
}
