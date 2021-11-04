using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SevenMediaAPI.Model
{
    public class GenderSummary
    {
        public int Age { get; set; }
        public int FemaleCount { get; set; }
        public int MaleCount { get; set; }
        public int OtherCount { get; set; }
    }
}
