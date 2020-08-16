using NUnit.Framework;
using PollVoteBackend.Models;
using PollVoteBackend.Services;
using PollVoteBackend.Services.Interfaces;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.ComponentModel;

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
    }
}