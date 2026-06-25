using AutoMapper;
using GemMangement.DAL;
using GemMangement.DAL.Models;
using GemMangement.DAL.Models.Enum;
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
        public async Task<bool> CreateSessionAsync(CreateSessionViewModel model, CancellationToken ct)
        {
            if(model.StartDate>=model.EndDate)return false;
            if (model.StartDate <= DateTime.UtcNow) return false;
            if (model.Capacity<1||model.Capacity>25) return false;

            var trainer = await _iuniteOfWork.GetRepository<Trainers>().GetByIdAsync(model.TrainerId, ct);
            if(trainer == null) return false;

            var category = await _iuniteOfWork.GetRepository<Category>().GetByIdAsync(model.CategoryId, ct);
            if (category == null) return false;

            var invalid = Enum.TryParse<Specialties>(category.CaregoryNAme, out var categoryspeciaties);
            if(invalid||trainer.specialties!=categoryspeciaties)return false;

           var mapped= _mapper.Map<Session>(model);

            _iuniteOfWork.GetRepository<Session>().Add(mapped);
            var count = await _iuniteOfWork.SaveChangeAsync(ct);
            return count > 0;   
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
    }
}
