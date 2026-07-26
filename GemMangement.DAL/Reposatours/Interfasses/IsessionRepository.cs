using GemMangement.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemMangement.DAL.Reposatours.Interfasses
{
    public interface IsessionRepository:IGenaricRepository<Session>
    {
       Task<IEnumerable<Session>> GetAllSessionWithTrainnerAndCategoryAsync(CancellationToken ct);
        Task<int> GetCountBookedSloteAsync(int id, CancellationToken ct);
        Task<Session> GetSessionByIdWithTrainnerAndCategoryasync(int sessionid,CancellationToken ct);

    }
}
