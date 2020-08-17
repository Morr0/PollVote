using AutoMapper;
using Microsoft.AspNetCore.Server.Kestrel.Core.Features;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace PollVoteBackend.Models
{
    // CHANGE THE DTOS AS WELL WHEN YOU CHANGE THAT
    /// <summary>
    /// Represents a poll of votes
    /// </summary>
    public class Poll
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Cookie only
        /// </summary>
        public string DeleteToken { get; set; }

        /// <summary>
        /// Question of a poll
        /// </summary>
        [Required]
        [NotNull]
        public string Question { get; set; }

        /// <summary>
        /// Possible choices of a vote
        /// </summary>
        [Required]
        [NotNull]
        public List<string> Choices { get; set; }

        /// <summary>
        /// Share the same index as Choices but holds the ints
        /// </summary>
        public List<int> ChoicesAnswers { get; set; }

        /// <summary>
        /// How many choices till expiry. i.e. number of choices that will make
        /// it end the poll.
        /// </summary>
        [NotNull]
        public int ExpiresOnChoices { get; set; } = 5;

        /// <summary>
        /// When did the poll end (expire).
        /// </summary>
        [NotNull]
        public string EndedOn { get; set; }

        public string CreatedDate { get; set; } = DateTime.UtcNow.ToString();
    }

    public class PollWriteDTO
    {
        /// <summary>
        /// Question of a poll
        /// </summary>
        [Required]
        [NotNull]
        public string Question { get; set; }

        /// <summary>
        /// Possible choices of a vote
        /// </summary>
        [Required]
        [NotNull]
        public List<string> Choices { get; set; }

        // Is in correlation with choices's indexes 
        [NotMapped]
        public List<int> ChoicesNumbers { get; set; }

        /// <summary>
        /// How many choices till expiry. i.e. number of choices that will make
        /// it end the poll.
        /// </summary>
        [NotNull]
        [Required]
        public int ExpiresOnChoices { get; set; } = 5;

        /// <summary>
        /// When did the poll end (expire).
        /// </summary>
        [NotNull]
        public string EndedOn { get; set; }
    }

    public class PollReadDTO
    {
        public string Question { get; set; }
        public Dictionary<string, int> ChoicesAndAnswers { get; set; }
        public int ExpiresOnChoices { get; set; } = 5;
        public string EndedOn { get; set; }

        
    }

    public static class PollExtensions
    {
        public static PollReadDTO FromPollToReadDTO(this IMapper mapper, Poll poll)
        {
            PollReadDTO dto = mapper.Map<PollReadDTO>(poll);
            dto.ChoicesAndAnswers = new Dictionary<string, int>();

            int count = poll.Choices.Count;
            for (int i = 0; i < count; i++)
            {
                dto.ChoicesAndAnswers.Add(poll.Choices[i], poll.ChoicesAnswers[i]);
            }

            return dto;
        }
    }
}
