using Microsoft.EntityFrameworkCore;
using PollVoteBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollVoteBackend.Data
{
    public class PollsContext : DbContext
    {
        public PollsContext(DbContextOptions<PollsContext> options)
            : base(options)
        {

        }

        public DbSet<Poll> Poll { get; set; }
    }
}
