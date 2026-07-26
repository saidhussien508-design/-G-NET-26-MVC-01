using GemMangement.DAL.Reposatours.Interfasses;
using GemMangement_AL_.common;
using GemMangement_AL_.Servicess.Interfases;
using GemMangement_AL_.ViewModel.session;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GemMangement.Pl.Controllers
{
    [Authorize]
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
            ViewBag.Trainers = new SelectList(await _isessionServesises.GetAllTrainnerForDropDownAsync(ct), "Id", "Name");
            ViewBag.Categories = new SelectList(await _isessionServesises.GetAllCategoryForDropDownAsync(ct), "Id", "Name");
            return View();  
        }
        [HttpPost]
        public async Task<IActionResult> create(CreateSessionViewModel model,CancellationToken ct)
        {
            if (ModelState.IsValid) 
            {
               var result=await _isessionServesises.CreateSessionAsync(model, ct);
                if (result.susses)
                {
                    TempData["SuccessMasage"] = "session Created Successfull";
                }
                else
                {
                    TempData["Errormasege"] = result.error;
                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id, CancellationToken ct)
        {
           var model=await _isessionServesises.GetSessionDetailsById(id, ct);
            if (model.susses)
            {
                return View(model.Value);
            }
            TempData["ErrorMassege"]=model.error;
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult>Edit(int id, CancellationToken ct)
        {
            var result = await _isessionServesises.GetSessionToUpDate(id, ct);
            if (result.susses)
            {
                ViewBag.Trainers = new SelectList(await _isessionServesises.GetAllTrainnerForDropDownAsync(ct), "Id", "Name");
                return View(result.Value);
            }
      
            TempData["Errormasege"] = result.error;
            return RedirectToAction("index");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,SessionToUpDateViewModel model ,CancellationToken ct)
        {
           if(!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _isessionServesises.UpDateSessionAsync(id, model, ct);
            if (result.susses)
            {
                TempData["SuccessMasage"] = "session UpDate";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Trainers = new SelectList(await _isessionServesises.GetAllTrainnerForDropDownAsync(ct), "Id", "Name");
                TempData["Errormasege"] = result.error;
                return View(model);
            }
        
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id, CancellationToken ct = default)
        {
            var result = await _isessionServesises.GetSessionDetailsById(id, ct);
            if (result.susses)
            {
                return View(result.Value);
            }
            TempData["ErrorMessage"] = result.error;
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteConfirm(int id,CancellationToken ct)
        {
            var result=await _isessionServesises.DeletSessionAsync(id, ct);
            if (result.susses)
            {
                TempData["SuccessMasage"] = "session Delete";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = result.error;
                return RedirectToAction("index");
            }
        }
    }
}
