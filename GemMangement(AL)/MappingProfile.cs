using AutoMapper;
using GemMangement.DAL.Models;
using GemMangement.DAL.Reposatours.Interfasses;
using GemMangement_AL_.ViewModel.member;
using GemMangement_AL_.ViewModel.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemMangement_AL_
{
    public  class MappingProfile:Profile
    {
        public MappingProfile()
        {
            mabtrainer();
            mabmember();
            mabplan();
            mabsession();
        }
        private void mabtrainer()
        {

        }
        private void mabmember()
        {
            CreateMap<Member, MemberViewModel>().ForMember(s => s.DateOfBirth, a => a.MapFrom(o => o.DateOfBirth.ToShortDateString()))
             .ForMember(s => s.Address, a => a.MapFrom(o => $"{o.Address.BuildingNumber}-{o.Address.Street}-{o.Address.City}"));

            CreateMap<Member, MemberToUpdateViewModel>().ForMember(s => s.City, a => a.MapFrom(o => o.Address.City))
               .ForMember(s => s.Street, a => a.MapFrom(o => o.Address.Street))
               .ForMember(s => s.BuildingNumber, a => a.MapFrom(o => o.Address.BuildingNumber));
            CreateMap<CreateMemberViewModel, Member>().ForMember(a => a.Address, o => o.MapFrom(s => new Address()
            {
                City = s.City,
                Street = s.Street,
                BuildingNumber = s.BuildingNumber,
            })).ForMember(a => a.HealthRecord, o => o.MapFrom(s => new HealthRecord()
            {
                Height = s.HealthRecordViewModel.height,
                Weight = s.HealthRecordViewModel.weight,
                BloodType = s.HealthRecordViewModel.BloodType,
                Note = s.HealthRecordViewModel.Note,
            }));
            CreateMap<HealthRecord, HealthyRecordViewModel>();

        }
        private void mabplan()
        {

        }
       
        private void mabsession()
        {
            CreateMap<CreateSessionViewModel, Session>();
            CreateMap<Trainers, TrainnerSelectViewModel>();
            CreateMap<Category, CategorySelectViewModel>();
        }






            


          

        }
    }

