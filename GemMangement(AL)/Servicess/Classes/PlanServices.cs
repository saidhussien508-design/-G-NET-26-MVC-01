using GemMangement.DAL.Reposatours.Interfasses;
using GemMangement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemMangement_AL_.Servicess.Classes
{
    public class PlanServices
    {
        private readonly IGenaricRepository<plane> _planrepsitory;

        public PlanServices(IGenaricRepository<plane> planrepsitory)
        {
            _planrepsitory = planrepsitory;
        }
    }
}
