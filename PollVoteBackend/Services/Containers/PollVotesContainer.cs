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
        public int CurrentVotes { get; set; } = 0;

        public PollVotesContainer(Poll poll)
        {
            Poll = poll;
            ChoiceNumbers = new Dictionary<string, int>();

            // Add all possible vote choices
            foreach (string choice in Poll.Choices)
            {
                ChoiceNumbers.Add(choice, 0);
            }
        }
    }
}
