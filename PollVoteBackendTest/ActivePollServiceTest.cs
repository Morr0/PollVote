using NUnit.Framework;
using PollVoteBackend.Models;
using PollVoteBackend.Services;
using PollVoteBackend.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PollVoteBackendTest
{
    public class ActivePollServiceTest
    {
        private IActivePollService _service;

        [SetUp]
        public void Setup()
        {
            _service = new ActivePollService();
        }

        [Test]
        public void CreatePollTest()
        {
            Poll poll = getPoll();

            _service.CreatePoll(poll);

            Assert.IsTrue(_service.HasPoll(poll.Id));
        }

        private Poll getPoll(string id = "5")
        {
            return new Poll
            {
                Id = id,
                Question = "What is 1 + 1",
                Choices = new List<string>
                {
                    "2",
                    "3",
                    "4",
                    "5"
                }
            };
        }

        [Test]
        public void DeletePollTest()
        {
            Poll poll = getPoll();
            poll.DeleteToken = "a";

            // Add poll then delete it
            _service.CreatePoll(poll);

            // Must pass
            Assert.IsFalse(_service.DeletePoll(poll.Id, "b"));


            // Mast pass
            Assert.IsTrue(_service.DeletePoll(poll.Id, "a"));
        }

        [Test]
        public void GetPollTest()
        {
            Poll p1 = getPoll();

            _service.CreatePoll(p1);

            Assert.AreEqual(p1, _service.GetPoll(p1.Id));
        }

        [Test]
        public void VoteTest()
        {
            string id = "hello";
            Poll poll = getPoll(id);
            string choice = poll.Choices[0];
            _service.CreatePoll(poll);

            // Add vote
            Assert.IsTrue(_service.Vote(id, choice));

            // Must have vote 
            Assert.AreEqual(1, _service.GetVotes(id)[choice]);

            // Voting on non-existing choice
            choice = "kqhsrg;ljhqergjbew";
            Assert.IsFalse(_service.Vote(id, choice));
        }

        private void voteNTimes(string id, string choice, int n)
        {
            for (int i = 0; i < n; i++)
            {
                _service.Vote(id, choice);
            }
        }

        [Test]
        public async Task ArchivedPollsTest()
        {
            // Setup
            MockArchivedPollService archivedPollsService = new MockArchivedPollService();
            (_service as ActivePollService).PollExpirySystem += archivedPollsService.OnPollExpiry;

            string id = "99";
            Poll poll = getPoll(id);
            string choice = poll.Choices[0];

            _service.CreatePoll(poll);
            voteNTimes(id, choice, poll.ExpiresOnChoices);

            Poll pollFromArchive = await archivedPollsService.GetPoll(id);

            Assert.AreEqual(poll, pollFromArchive);
            Assert.IsFalse(_service.HasPoll(id));

        }
    }
}