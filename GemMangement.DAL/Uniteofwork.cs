using GemMangement.DAL.Models;
using GemMangement.DAL.Reposatours.Classes;
using GemMangement.DAL.Reposatours.Interfasses;
using GemMangement.Pl.Dbcontext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemMangement.DAL
{
    public class Uniteofwork : IuniteOfWork
    {
        private readonly GemAppDpContext _context;
        private readonly IsessionRepository _isessionRepository;
        private readonly Dictionary<string, object> _repository = [];
        public Uniteofwork(GemAppDpContext context,IsessionRepository isessionRepository) 
        {
            _context = context;
            _isessionRepository = isessionRepository;
        }

        public IsessionRepository SessionRepository => _isessionRepository;

        public IGenaricRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            var typeName=typeof(TEntity).Name;
            if (_repository.TryGetValue(typeName, out object? value)) 
               return value as IGenaricRepository<TEntity>;

            var repo = new GenaricRepository<TEntity>(_context);
            _repository.Add(typeName, repo);
            return repo;
        }

        public async Task<int> SaveChangeAsync(CancellationToken ct = default)
        
        =>  await _context.SaveChangesAsync(ct);
           
        
    }
}
