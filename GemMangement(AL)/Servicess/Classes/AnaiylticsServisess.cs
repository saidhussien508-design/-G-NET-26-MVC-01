using GemMangement.DAL;
using GemMangement.DAL.Models;
using GemMangement_AL_.Servicess.Interfases;
using GemMangement_AL_.ViewModel.Anaiytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemMangement_AL_.Servicess.Classes
{

    public class AnaiylticsServisess : IAnaiyticsServices
    {
        private readonly IuniteOfWork _iuniteOfWork;

        public AnaiylticsServisess(IuniteOfWork iuniteOfWork)
        {
           _iuniteOfWork = iuniteOfWork;
        }
        public async Task<AnaiyticsViewModel> GetDataasync(CancellationToken ct)
        {
            var upcomingSessions = await _iuniteOfWork.GetRepository<Session>().GetCountAsync(S => S.StartDate > DateTime.Now, ct);
            var ongoingSessions = await _iuniteOfWork.GetRepository<Session>().GetCountAsync(S => S.StartDate <= DateTime.Now && S.EndDate >= DateTime.Now&&S.EndDate>DateTime.Now, ct);
            var completedSessions = await _iuniteOfWork.GetRepository<Session>().GetCountAsync(S => S.EndDate < DateTime.Now, ct);
            var totalMembers = await _iuniteOfWork.GetRepository<Member>().GetCountAsync(ct: ct);
            var totalTrainers = await _iuniteOfWork.GetRepository<Trainers>().GetCountAsync(ct: ct);
            var totalActiveMembers = await _iuniteOfWork.GetRepository<MemberShip>().GetCountAsync(ct: ct);

            return new AnaiyticsViewModel()
            {
                ActiveMember = totalActiveMembers,
                CompletedSession = completedSessions,
                OnGoingSession = ongoingSessions,
                TotalMember = totalMembers,
                totalTranner = totalTrainers,
                UpcommingSession = upcomingSessions,
            };
        }
    }
}
