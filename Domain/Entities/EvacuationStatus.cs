using evacuation.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evacuation.Domain.Entities
{
    public class EvacuationStatus : BaseEntity
    {
        public string StatusCode { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Sequence { get; set; }
        public ICollection<EvacuationPlan>  evacuationPlans { get; set; }   = null!;
    }

}
