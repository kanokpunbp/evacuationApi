using evacuation.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evacuation.Domain.Entities
{
    public class VehicleTypes : BaseEntity
    {
        public string TypeName { get; set; } = null!;
        public ICollection<Vehicles> Vehicles { get; set; } = new List<Vehicles>();
    }

}
