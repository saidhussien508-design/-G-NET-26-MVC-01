using GemMangement_AL_.ViewModel.Anaiytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemMangement_AL_.Servicess.Interfases
{
    public interface IAnaiyticsServices
    {
        Task<AnaiyticsViewModel> GetDataasync(CancellationToken ct);
    }
}
