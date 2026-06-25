using GemMangement.DAL.Models;
using GemMangement.DAL.Reposatours.Interfasses;
using GemMangement.Pl.Dbcontext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemMangement.DAL.Reposatours.Classes
{
    public class SessionRepository:GenaricRepository<Session>, IsessionRepository
    {
        private readonly GemAppDpContext _context;

        public SessionRepository(GemAppDpContext context) : base(context)
        {
            _context = context;
        }

       

        public async Task<IEnumerable<Session>> GetAllSessionWithTrainnerAndCategoryAsync(CancellationToken ct)
        {
            return await _context.sessions.AsNoTracking().Include(s => s.Trainer).Include(s => s.Category).ToListAsync();
        }

        public async Task<int> GetCountBookedSloteAsync(int id, CancellationToken ct)
        {
            return await _context.bookings.CountAsync(s=>s.Sessionid == id);
        }

    }
}
