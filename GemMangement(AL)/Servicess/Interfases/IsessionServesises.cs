using GemMangement_AL_.common;
using GemMangement_AL_.ViewModel.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemMangement_AL_.Servicess.Interfases
{
    public interface IsessionServesises
    {
        Task<IEnumerable<SessionViewModel>> GetAllSessionAsync(CancellationToken ct);
        Task<Result>CreateSessionAsync(CreateSessionViewModel model,CancellationToken ct);

        Task<IEnumerable<TrainnerSelectViewModel>> GetAllTrainnerForDropDownAsync(CancellationToken ct);
        Task<IEnumerable<CategorySelectViewModel>> GetAllCategoryForDropDownAsync(CancellationToken ct);
        Task<Result<SessionViewModel>> GetSessionDetailsById(int SessionId, CancellationToken ct);
        Task<Result<SessionToUpDateViewModel>> GetSessionToUpDate(int id, CancellationToken ct);
        Task<Result>UpDateSessionAsync(int id,SessionToUpDateViewModel model,CancellationToken ct);

        Task<Result>DeletSessionAsync(int id,CancellationToken ct);
    }
}
