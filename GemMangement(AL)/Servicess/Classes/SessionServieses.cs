using AutoMapper;
using GemMangement.DAL;
using GemMangement.DAL.Models;
using GemMangement.DAL.Models.Enum;
using GemMangement_AL_.common;
using GemMangement_AL_.Servicess.Interfases;
using GemMangement_AL_.ViewModel.session;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemMangement_AL_.Servicess.Classes
{
    public class SessionServieses : IsessionServesises
    {
        private readonly IuniteOfWork _iuniteOfWork;
        private readonly IMapper _mapper;

        public SessionServieses(IuniteOfWork iuniteOfWork,IMapper mapper)
        {
            _iuniteOfWork = iuniteOfWork;
            _mapper = mapper;
        }

       

        public async Task<IEnumerable<SessionViewModel>> GetAllSessionAsync(CancellationToken ct)
        {
           var result=await _iuniteOfWork.SessionRepository.GetAllSessionWithTrainnerAndCategoryAsync(ct);
            var mapped= result.Select(s => new SessionViewModel
            {
                Id=s.Id,
                Capacity=s.Capacity,
                CategoryName=s.Category.CaregoryNAme,
                Description=s.Description,
                EndDate=s.EndDate,
                StartDate=s.StartDate,

            });
            foreach(var item in mapped)
            {
                item.AvailableSlots = item.Capacity - await _iuniteOfWork.SessionRepository.GetCountBookedSloteAsync(item.Id, ct);
            }
            return mapped;
        }
        public async Task<Result> CreateSessionAsync(CreateSessionViewModel model, CancellationToken ct)
        {
            if(model.StartDate>=model.EndDate)return Result.validation(massage: "EndDate Must Be After StartDate"); 
            if (model.StartDate <= DateTime.UtcNow) return Result.validation(massage: "StartDate Must Be In The Future"); ;
            if (model.Capacity<1||model.Capacity>25) return Result.validation(massage: "Capacity Must Be Between 1 And 25"); ;

            var trainer = await _iuniteOfWork.GetRepository<Trainers>().GetByIdAsync(model.TrainerId, ct);
            if(trainer == null) return Result.notfound(massage: $"Trainer With Id {model.TrainerId} not found"); ;

            var category = await _iuniteOfWork.GetRepository<Category>().GetByIdAsync(model.CategoryId, ct);
            if (category == null) return Result.notfound(massage: $"Category With Id {model.CategoryId} not found"); ;

            var invalid = Enum.TryParse<Specialties>(category.CaregoryNAme, out var categoryspeciaties);
            if(invalid||trainer.specialties!=categoryspeciaties) return Result.validation(massage: "Can not Create Session with This Trainer Because His Specialty");

            var mapped= _mapper.Map<Session>(model);

            _iuniteOfWork.GetRepository<Session>().Add(mapped);
            var count = await _iuniteOfWork.SaveChangeAsync(ct);
            return count > 0?Result.ok():Result.fail("faild to create sessions");   
        }

        public async Task<IEnumerable<TrainnerSelectViewModel>> GetAllTrainnerForDropDownAsync(CancellationToken ct)
        {
           var trainer=await _iuniteOfWork.GetRepository<Trainers>().GetallAsync(false, ct);

           return _mapper.Map<IEnumerable<TrainnerSelectViewModel>>(trainer);

        }

        public async Task<IEnumerable<CategorySelectViewModel>> GetAllCategoryForDropDownAsync(CancellationToken ct)
        {
            var Category = await _iuniteOfWork.GetRepository<Category>().GetallAsync(false, ct);

            
           return _mapper.Map<IEnumerable<CategorySelectViewModel>>(Category);
        }

        public async Task<Result<SessionViewModel>> GetSessionDetailsById(int SessionId, CancellationToken ct)
        {
          var session=await  _iuniteOfWork.SessionRepository.GetSessionByIdWithTrainnerAndCategoryasync(SessionId, ct);
            if (session is null) return Result<SessionViewModel>.notfound($"Session With Id {SessionId}Not Found");
             var mappedviewmodel=_mapper.Map<SessionViewModel>(session);
            mappedviewmodel.AvailableSlots=mappedviewmodel.Capacity-await _iuniteOfWork.SessionRepository.GetCountBookedSloteAsync(SessionId,ct);
            return Result<SessionViewModel>.ok(mappedviewmodel);
        }

        public async Task<Result<SessionToUpDateViewModel>> GetSessionToUpDate(int id, CancellationToken ct)
        {

           var result=await _iuniteOfWork.GetRepository<Session>().GetByIdAsync(id, ct);
            if (result is null)return Result<SessionToUpDateViewModel>.notfound($"Session With Id {id} Is NOt Found");
            if (result.StartDate <= DateTime.Now) return Result<SessionToUpDateViewModel>.fail("Can Not UpDate Session that Has Already started");
           var Bookcount=await _iuniteOfWork.SessionRepository.GetCountBookedSloteAsync(id, ct);
            if(Bookcount>0)return Result<SessionToUpDateViewModel>.fail("Can Not UpDate Session that Has Already Booking");

            var ViewModel= _mapper.Map<SessionToUpDateViewModel>(result);
            return Result<SessionToUpDateViewModel>.ok( ViewModel);

        }

        public async Task<Result> UpDateSessionAsync(int id, SessionToUpDateViewModel model, CancellationToken ct)
        {
            var session = await _iuniteOfWork.GetRepository<Session>().GetByIdAsync(id, ct);
            if (session is null) return Result.notfound($"Session With Id {id} Is NOt Found");
            if (session.StartDate <= DateTime.Now) return Result.fail("Can Not UpDate Session that Has Already started");
            var Bookcount = await _iuniteOfWork.SessionRepository.GetCountBookedSloteAsync(id, ct);
            if (Bookcount > 0) return Result.fail("Can Not UpDate Session that Has Already Booking");

            if (session.StartDate >= model.EndDate) return Result.validation("EndDAte Must Be After StartDate ");
            if (model.StartDate >= DateTime.Now) return Result.validation("StartDate Must Be In The Futher");

            var trainer = await _iuniteOfWork.GetRepository<Trainers>().GetByIdAsync(model.TrainerId, ct);
            if (trainer == null) return Result.notfound(massage: $"Trainer With Id {model.TrainerId} not found"); ;

            var category = await _iuniteOfWork.GetRepository<Category>().GetByIdAsync(session.Categoryid, ct);
            if (category == null) return Result.notfound(massage: $"Category With Id {session.Categoryid} not found"); ;

            var InVAlid= Enum.TryParse<Specialties>(category.CaregoryNAme,true,out var categorySpacifictly);
            if (!InVAlid || categorySpacifictly != trainer.specialties) return Result.validation("Can not Create Session with This Trainer Because His Specialty");
            session.StartDate = model.StartDate;
            session.EndDate = model.EndDate;
            session.Description = model.Description;
            session.Trainerid = model.TrainerId;
            session.UpDateedAt= DateTime.Now;
            _iuniteOfWork.GetRepository<Session>().UpDatAsync(session);
          var count=await _iuniteOfWork.SaveChangeAsync(ct);
            return count > 0 ? Result.ok() : Result.fail("Faild To Update Session");
        }

        public async Task<Result> DeletSessionAsync(int id, CancellationToken ct)
        {
            var session = await _iuniteOfWork.GetRepository<Session>().GetByIdAsync(id, ct);
            if (session is null) return Result.notfound(massage: $"Session With Id {id} not found !");
            if (session.EndDate >= DateTime.Now) return Result.fail(massege: "Can not Delete Session Not Ended Yet");
            var bookingCount = await _iuniteOfWork.SessionRepository.GetCountBookedSloteAsync(id, ct);
            if (bookingCount > 0) return Result.fail(massege: "Can Not Delete session That has already Bookings !");
            _iuniteOfWork.SessionRepository.Delete(session);
            var count=await _iuniteOfWork.SaveChangeAsync(ct);
            return count > 0 ? Result.ok() : Result.fail("Faild To Delete Session");
        }
    }
}
