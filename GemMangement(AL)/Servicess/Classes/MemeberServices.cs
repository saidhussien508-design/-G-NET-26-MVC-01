using AutoMapper;
using GemMangement.DAL;
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
       
        private readonly IuniteOfWork _iuniteOfWork;
        private readonly IMapper _mapper;

        public MemeberServices(IuniteOfWork iuniteOfWork,IMapper mapper)
        {
            _iuniteOfWork = iuniteOfWork;
           _mapper = mapper;
        }
        
        public async Task<IEnumerable<MemberViewModel>> GetAllMemberAsync(CancellationToken ct)
        {
            var member = await _iuniteOfWork.GetRepository<Member>().GetallAsync(ct: ct);
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

            //var ViewModel = member.Select(members => new MemberViewModel()
            // {
            //     id = members.Id,
            //     Name = members.Name,
            //     Phone = members.phone,
            //     Photo = members.photo,
            //     Gender = members.Gender.ToString(),
            //     Email = members.Email

            // });
           var ViewModel= _mapper.Map<IEnumerable< MemberViewModel>>(member);
            return ViewModel;
        }
        public async Task<MemberViewModel?> GetMemberDetails(int memberid, CancellationToken ct)
        {
            var member = await _iuniteOfWork.GetRepository<Member>().GetByIdAsync(memberid, ct);
            if (member is null) return null;
            //var membermodel = new MemberViewModel()
            //{
            //    Photo = member.photo,
            //    Name = member.Name,
            //    Email = member.Email,
            //    Phone = member.phone,
            //    Gender = member.Gender.ToString(),
            //    DateOfBirth = member.DateOfBirth.ToString(),
            //    Address = $"{member.Address.BuildingNumber}-{member.Address.Street}-{member.Address.City}"
            //};
           var membermodel= _mapper.Map<MemberViewModel>(member);
            var activemembership = await _iuniteOfWork.GetRepository<MemberShip>().Firstordefultacync(s => s.memberid == memberid && s.EndDate > DateTime.UtcNow, ct);
            if (activemembership is not null)

            {
                membermodel.Name = activemembership.plane.Name;
                membermodel.MembershipStartDate = activemembership.CreatedAt.ToString();
                membermodel.MembershipStartDate = activemembership.EndDate.ToString();
            }
            return membermodel;
        }
        public async Task<HealthyRecordViewModel> GetMemberHealtyRecord(int memberid, CancellationToken ct)
        {
          var result=await _iuniteOfWork.GetRepository<HealthRecord>().Firstordefultacync(s =>s.Memberid== memberid, ct);
            if (result is  null) return null;

            //var medule = new HealthyRecordViewModel()
            //{
            //  weight=result.Weight,
            //  height=result.Height, 
            //  BloodType=result.BloodType,   
            //  Note=result.Note,
            //};
           var medule= _mapper.Map<HealthyRecordViewModel>(result);
            return medule;
        }

        public async Task<MemberToUpdateViewModel> GetMemberToUpdateAsync(int MemberId, CancellationToken ct)
        {
           var member=await _iuniteOfWork.GetRepository<Member>().GetByIdAsync(MemberId, ct);
            if (member is null) return null;
            //var model=new MemberToUpdateViewModel()
            //{
            //    Email= member.Email,
            //    Name= member.Name,
            //    Phone=member.phone,
            //    Photo=member.photo,
            //    City=member.Address.City,
            //    Street=member.Address.Street,
            //    BuildingNumber=member.Address.BuildingNumber,

            //};
           var model= _mapper.Map<MemberToUpdateViewModel>(member);
            return model;
        }

        public async Task<bool> UpdatememberAsync(int MemberId, MemberToUpdateViewModel model, CancellationToken ct)
        {
            var member = await _iuniteOfWork.GetRepository<Member>().GetByIdAsync(MemberId, ct);
            if (member is null) return false;

            var PhoneExist = await _iuniteOfWork.GetRepository<Member>().AnyAsync(s => s.phone == model.Phone&&s.Id!=MemberId, ct);
            var EmailExist = await _iuniteOfWork.GetRepository<Member>().AnyAsync(s => s.Email == model.Email&& s.Id!= MemberId, ct);

            if (PhoneExist || EmailExist)
            {
                return false;
            }

            member.Email = model.Email;
            member.phone=model.Phone;
            member.Address.BuildingNumber = model.BuildingNumber;
            member.Address.Street = model.Street;
            member.Address.City = model.City;

            _iuniteOfWork.GetRepository<Member>().UpDatAsync(member);
            var count = await _iuniteOfWork.SaveChangeAsync(ct);
            return count > 0;
        }
        public async Task<bool> CreatmemberAsync(CreateMemberViewModel model, CancellationToken ct)
        {
           var PhoneExist=await _iuniteOfWork.GetRepository<Member>().AnyAsync(s=>s.phone==model.Phone, ct) ;
            var EmailExist = await _iuniteOfWork.GetRepository<Member>().AnyAsync(s => s.Email == model.Email, ct);

            if (PhoneExist||EmailExist) 
            { 
                return false;
            }
            //casting from CreateMemberViewModel to member
            //var creatmember = new Member()
            //{
            //    Name = model.Name,
            //    Email = model.Email,
            //    phone = model.Phone,
            //    Gender = model.Gender,
            //    Address = new Address()
            //    {
            //        City = model.City,
            //        BuildingNumber = model.BuildingNumber,
            //        Street = model.Street,

            //    },
            //    HealthRecord = new HealthRecord()
            //    {
            //        Weight=model.HealthRecordViewModel.weight,
            //        Height=model.HealthRecordViewModel.height,  
            //        BloodType=model.HealthRecordViewModel.BloodType,
            //        Note=model.HealthRecordViewModel.Note,


            //    }


            //};
          var creatmember= _mapper.Map<Member>(model);
            _iuniteOfWork.GetRepository<Member>().Add(creatmember);
            var count = await _iuniteOfWork.SaveChangeAsync(ct);
            return count > 0;
        }

        public async Task<bool> DeletmemberAsync(int MemberId, CancellationToken ct)
        {
            var member = await _iuniteOfWork.GetRepository<Member>().GetByIdAsync(MemberId, ct);
            if (member is null) return false;

            var HasFutherSession=await _iuniteOfWork.GetRepository<Booking>().AnyAsync(s => s.Memberid == MemberId && s.Session.StartDate > DateTime.Now,ct);
            if (HasFutherSession) return false;

           _iuniteOfWork.GetRepository<Member>().Delete(member);
            var count=await _iuniteOfWork.SaveChangeAsync(ct);
            return count > 0;

        }

     
        }
    }

