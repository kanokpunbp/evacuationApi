using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evacuation.Domain.Entities
{
    public class RunningCodes
    {
        public string Name { get; set; } = null!;
        public string prefix { get; set; } = null!;
        public int CurrentValue { get; set; }
    }
}
