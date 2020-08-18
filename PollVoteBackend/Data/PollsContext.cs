using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PollVoteBackend.Models;

namespace PollVoteBackend.Data
{
    public class PollsContext : DbContext
    {
        private IConfiguration _configuration;
        public PollsContext(DbContextOptions<PollsContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(_configuration.GetSection("Connection").Value);

        public DbSet<Poll> Poll { get; set; }
    }
}
