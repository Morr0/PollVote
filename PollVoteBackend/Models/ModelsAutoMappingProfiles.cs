using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollVoteBackend.Models
{
    public class ModelsAutoMappingProfiles : Profile
    {
        public ModelsAutoMappingProfiles()
        {
            CreateMap<PollWriteDTO, Poll>();
            CreateMap<Poll, PollReadDTO>();
        }
    }
}
