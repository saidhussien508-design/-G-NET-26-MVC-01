using GemMangement.DAL.Models;
using GemMangement_AL_.ViewModel.member;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemMangement_AL_.Servicess.Interfases
{
    public interface ImemberServises
    {
        Task<IEnumerable<MemberViewModel>> GetAllMemberAsync(CancellationToken ct);
        Task<MemberViewModel?> GetMemberDetails( int memberid,CancellationToken ct);
        Task<HealthyRecordViewModel> GetMemberHealtyRecord(int memberid, CancellationToken ct);
        Task<bool> CreatmemberAsync(CreateMemberViewModel model,CancellationToken ct);
        Task<MemberToUpdateViewModel> GetMemberToUpdateAsync(int MemberId, CancellationToken ct);
        Task<bool> UpdatememberAsync(int MemberId,MemberToUpdateViewModel model, CancellationToken ct);
        Task<bool> DeletmemberAsync(int MemberId,  CancellationToken ct);
    }

    
}
