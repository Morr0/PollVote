using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PollVoteBackend.Controllers.Queries
{
    public class VotingQuery
    {
        [Required]
        [NotNull]
        public string Choice { get; set; }
    }
}
