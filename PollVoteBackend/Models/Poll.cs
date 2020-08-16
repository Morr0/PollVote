using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace PollVoteBackend.Models
{

    /// <summary>
    /// Represents a poll of votes
    /// </summary>
    public class Poll
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

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
    }
}
