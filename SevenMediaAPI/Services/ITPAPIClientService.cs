using SevenMediaAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SevenMediaAPI.Services
{
    public interface ITPAPIClientService
    {
        Task<List<People>> ReadSample();
    }
}
