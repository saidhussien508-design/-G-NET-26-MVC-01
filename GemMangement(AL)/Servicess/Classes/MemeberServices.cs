using GemMangement.DAL.Models;
using GemMangement.DAL.Reposatours.Interfasses;
using GemMangement_AL_.Servicess.Interfases;
using GemMangement_AL_.ViewModel.member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemMangement_AL_.Servicess.Classes
{
    public class MemeberServices : ImemberServises
    {
        private readonly IGenaricRepository<Member> _memberRepository;

        public MemeberServices(IGenaricRepository<Member> memberRepository)
        {
            _memberRepository = memberRepository;
        }
        public async Task<IEnumerable<MemberViewModel>> GetAllMemberAsync(CancellationToken ct)
        {
            var member = await _memberRepository.GetallAsync(ct: ct);
            //var membersvoidmodel = new List<MemberViewModel>();
            //foreach (var members in member)
            //{
            //    var membermodel = new MemberViewModel()
            //    {
            //        id = members.Id,
            //        Name = members.Name,
            //        Phone = members.phone,
            //        Photo = members.photo,
            //        Gender = members.Gender.ToString(),
            //        Email = members.Email

            //    };
            //    membersvoidmodel.Add(membermodel);
            //}
            //return membersvoidmodel;

            //طريقه تانيه

           var ViewModel = member.Select(members => new MemberViewModel()
            {
                id = members.Id,
                Name = members.Name,
                Phone = members.phone,
                Photo = members.photo,
                Gender = members.Gender.ToString(),
                Email = members.Email

            });
            return ViewModel;
        }
         public Task<MemberViewModel?> GetMemberDetails(CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<HealthyRecordViewModel> GetMemberHealtyRecord(CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<MemberToUpdateViewModel> GetMemberToUpdateAsync(int MemberId, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdatememberAsync(int MemberId, MemberToUpdateViewModel model, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
        public Task<bool> CreatmemberAsync(CreateMemberViewModel model, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletmemberAsync(int MemberId, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
