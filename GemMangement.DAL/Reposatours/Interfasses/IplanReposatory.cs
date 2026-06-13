using GemMangement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemMangement.DAL.Reposatours.Interfasses
{
    public interface IplanReposatory
    {
       Task<IEnumerable<plane>> GetallAsync(bool tracking=false,CancellationToken ct=default);
         Task<plane?> GetByIdAsync(int id, CancellationToken ct=default);
         Task<int> AddAsync(plane plan, CancellationToken ct=default);

        Task<int> UpDateAsync(plane plan, CancellationToken ct = default);
        Task<int> DeleteAsync(plane plan, CancellationToken ct = default);

    }
}
