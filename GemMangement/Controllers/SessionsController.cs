using GemMangement.DAL.Reposatours.Interfasses;
using GemMangement_AL_.Servicess.Interfases;
using GemMangement_AL_.ViewModel.session;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GemMangement.Pl.Controllers
{
    public class SessionsController :Controller
    {
        private readonly IsessionServesises _isessionServesises;

        public SessionsController(IsessionServesises isessionServesises) 
        {
            _isessionServesises = isessionServesises;
        }
        [HttpGet]
        public async Task<IActionResult> index(CancellationToken ct)
        {
            var result = await _isessionServesises.GetAllSessionAsync(ct);
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> create(CancellationToken ct)
        {
            ViewBag.Trainers = new SelectList(await _isessionServesises.GetAllTrainnerForDropDownAsync(ct),"id","name");
            ViewBag.Categories = new SelectList(await _isessionServesises.GetAllCategoryForDropDownAsync(ct), "id", "CategoryName");
            return View();  
        }
        [HttpPost]
        public async Task<IActionResult> create(CreateSessionViewModel model,CancellationToken ct)
        {
            if (ModelState.IsValid) 
            {
               var result=await _isessionServesises.CreateSessionAsync(model, ct);
                if (result)
                {
                    TempData["SuccessMasage"] = "session Created Successfull";
                }
                else
                {
                    TempData["Errormasege"] = "Faild To Create session";
                }
            }
            return View(model);
        }
    }
}
