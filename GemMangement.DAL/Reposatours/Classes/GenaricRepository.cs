using GemMangement.DAL.Models;
using GemMangement.DAL.Reposatours.Interfasses;
using GemMangement.Models;
using GemMangement.Pl.Dbcontext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GemMangement.DAL.Reposatours.Classes
{
    public class GenaricRepository<TEntity> : IGenaricRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly GemAppDpContext _Context;
        private readonly DbSet<TEntity> _dpset;
        public GenaricRepository(GemAppDpContext gemAppDpContext)
        {
            _Context = gemAppDpContext;
            _dpset = _Context.Set<TEntity>();
        }
        public async Task<IEnumerable<TEntity>> GetallAsync(bool tracking = false, CancellationToken ct = default)
                    => tracking? await _dpset.ToListAsync(ct) : await _Context.Set<TEntity>().AsNoTracking().ToListAsync(ct);


        public Task<TEntity?> GetByIdAsync(int id, CancellationToken ct = default)
                     => _dpset.FirstOrDefaultAsync(s => s.Id == id, ct);
        

        public void Add(TEntity entity)
        {
          _dpset.AddAsync(entity);
           
        }
        public void UpDatAsync(TEntity entity )
        {
            _dpset.Update(entity);
         
        }
        public void Delete(TEntity entity)
        {
            _dpset.Remove(entity);
          
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct)
        {
           return await _dpset.AnyAsync(predicate, ct);
        }

        public async Task<TEntity?> Firstordefultacync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct)
        {
            return await _dpset.FirstOrDefaultAsync(predicate, ct);
        }

        public async Task<int> GetCountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken ct = default)
        {
            return predicate is not null ? await _Context.Set<TEntity>().CountAsync(predicate, ct) : await _Context.Set<TEntity>().CountAsync(ct);
        }
    }

       
      
    }

