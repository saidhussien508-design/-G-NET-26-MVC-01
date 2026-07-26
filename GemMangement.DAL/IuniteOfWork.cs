using GemMangement.DAL.Models;
using GemMangement.DAL.Reposatours.Classes;
using GemMangement.DAL.Reposatours.Interfasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemMangement.DAL
{
    public  interface IuniteOfWork

    { 
        IGenaricRepository<TEntity> GetRepository<TEntity>() where TEntity:BaseEntity;
        Task<int> SaveChangeAsync(CancellationToken ct = default);
       public IsessionRepository SessionRepository { get; }
    }
}
