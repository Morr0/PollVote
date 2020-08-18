using Microsoft.EntityFrameworkCore;
using PollVoteBackend.Data;
using PollVoteBackend.Models;
using PollVoteBackend.Services.Events;
using PollVoteBackend.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace PollVoteBackend.Services
{
    public class ArchivedPollService : IArchivedPollService
    {
        private PollsContext _context;
        private IActivePollService _activeService;

        public ArchivedPollService(PollsContext context, IActivePollService activeService)
        {
            _context = context;
            _activeService = activeService;

            // Subscribe
            (activeService as ActivePollService).PollExpirySystem += OnPollExpiry;
        }

        ~ArchivedPollService()
        {
            Console.WriteLine("Expiring ");
            (_activeService as ActivePollService).PollExpirySystem -= OnPollExpiry;
        }

        public async Task<Poll> GetPoll(string id)
        {
            return await _context.Poll.AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public void OnPollExpiry(object sender, PollExpiryEventArgs args)
        {
            Task.WhenAll(Task.Run(async () =>
            {
                await _context.Poll.AddAsync(args.PVC.Poll);
                await _context.SaveChangesAsync();
            }));
        }
    }
}
