using PollVoteBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PollVoteBackend.Services.Containers
{
    /// <summary>
    /// Internal use only. To hold votes for each poll. Also
    /// for inter-service communication.
    /// </summary>
    public class PollVotesContainer
    {
        public Poll Poll;
        public Dictionary<string, int> ChoiceNumbers;
        public int CurrentVotes { get; set; } = 0;

        // Holds reference to the index in Poll for each choice
        // key = choice string
        // value = index of choice in Poll in Choices and ChoicesAnswers
        internal Dictionary<string, int> ChoiceIndexToChoice;

        public PollVotesContainer(Poll poll)
        {
            Poll = poll;
            ChoiceNumbers = new Dictionary<string, int>();

            ChoiceIndexToChoice = new Dictionary<string, int>();

            int count = poll.Choices.Count;
            // Configure Poll.ChoicesAnswers to have the same size as Choices
            poll.ChoicesAnswers = new List<int>(count);

            // Add all possible vote choices
            for (int i = 0; i < count; i++)
            {
                string choice = Poll.Choices[i];
                ChoiceNumbers.Add(choice, 0);

                // Add choice to index mapping
                ChoiceIndexToChoice.Add(choice, i);

                // Put defaults
                poll.ChoicesAnswers.Add(0);
            }
        }
    }
}
