using GemMangement.DAL.Reposatours.Classes;
using GemMangement.DAL.Reposatours.Interfasses;
using GemMangement.Models;
using GemMangement.Pl.Dbcontext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GemMangement.Controllers
{
    [Authorize]
    public class PlansController:Controller
    {
        //private readonly GemAppDpContext _Context=new GemAppDpContext();

        private readonly IGenaricRepository<plane> _iplanReposatory;
        public PlansController(IGenaricRepository<plane> planReposatory)
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
        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }
    }
}
