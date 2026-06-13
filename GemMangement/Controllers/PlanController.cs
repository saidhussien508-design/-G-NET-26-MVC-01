using GemMangement.DAL.Reposatours.Classes;
using GemMangement.DAL.Reposatours.Interfasses;
using GemMangement.Pl.Dbcontext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GemMangement.Controllers
{
    public class PlansController:Controller
    {
        //private readonly GemAppDpContext _Context=new GemAppDpContext();

        private readonly IplanReposatory _iplanReposatory;
        public PlansController(IplanReposatory planReposatory)
        {
            _iplanReposatory = planReposatory;
        }
        public async Task<IActionResult> index(CancellationToken ct)
        {
         var plan =await _iplanReposatory.GetallAsync(ct:ct);

            return View(plan);
        }
        public async Task<IActionResult> Details(int id,CancellationToken ct)
        {
           var plan=await _iplanReposatory.GetByIdAsync(id,ct);
            if (plan is null)return RedirectToAction(nameof(Index));
             return View(plan);
        }
    }
}
