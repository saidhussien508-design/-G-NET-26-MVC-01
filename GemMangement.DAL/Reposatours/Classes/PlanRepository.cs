using GemMangement.DAL.Reposatours.Interfasses;
using GemMangement.Models;
using GemMangement.Pl.Dbcontext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemMangement.DAL.Reposatours.Classes
{
    public class PlanRepository : IplanReposatory   
    {
        private readonly GemAppDpContext _Context;
       
        public PlanRepository(GemAppDpContext gemAppDpContext)
        {
            _Context = gemAppDpContext;
        }
        public async Task<IEnumerable<plane>> GetallAsync(bool tracking = false, CancellationToken ct = default)
               => tracking ? await _Context.planes.ToListAsync(ct) : await _Context.planes.AsNoTracking().ToListAsync(ct);



        public Task<plane?> GetByIdAsync(int id, CancellationToken ct = default)
       => _Context.planes.FirstOrDefaultAsync(s => s.Id == id, ct);
        public async Task<int> AddAsync(plane plan, CancellationToken ct = default)
        {
          await _Context.planes.AddAsync(plan, ct);
            return await _Context.SaveChangesAsync(ct);
        }
        public async Task<int> UpDateAsync(plane plan, CancellationToken ct = default)
        {
            _Context.planes.Update(plan);
            return await _Context.SaveChangesAsync(ct);
        }
        public async Task<int> DeleteAsync(plane plan, CancellationToken ct = default)
        {
            _Context.planes.Remove(plan);
            return await _Context.SaveChangesAsync(ct);
        }

         }
}
