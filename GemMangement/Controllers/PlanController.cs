using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GemMangement.Controllers
{
    public class PlansController:Controller
    {
        private readonly GemAppDpContext _Context=new GemAppDpContext();    
        public async Task<IActionResult> index()
        {
         var plan =await _Context.planes.ToListAsync();

            return View(plan);
        }
        public IActionResult Details(int id)
        {
           var plan= _Context.planes.FirstOrDefault(p=>p.Id == id);
            if (plan is null)return RedirectToAction(nameof(Index));
             return View(plan);
        }
    }
}
