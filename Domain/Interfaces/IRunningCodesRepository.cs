using evacuation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evacuation.Domain.Interfaces
{
    public interface IRunningCodesRepository : IGenericRepository<RunningCodes>
    {
        Task<RunningCodes> GetNextAsync(string name);
    }
}
