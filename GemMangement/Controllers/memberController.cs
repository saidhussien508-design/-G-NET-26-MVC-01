using GemMangement.DAL.Models;
using GemMangement_AL_.Servicess.Interfases;
using GemMangement_AL_.ViewModel.member;
using Microsoft.AspNetCore.Mvc;

namespace GemMangement.Pl.Controllers
{
    public class memberController:Controller
    {
        private readonly ImemberServises _memberservices;

        public memberController(ImemberServises memberservices)
        {
            _memberservices = memberservices;
        }
        public async Task< IActionResult> index(CancellationToken ct)
        {
           var memberindex=await _memberservices.GetAllMemberAsync(ct);
            return View(memberindex);  
        }
        [HttpGet]
        public IActionResult Create()
        {
          return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateMemberViewModel model,CancellationToken ct)
        {
            if(ModelState.IsValid)
            {
                var membercreat = await _memberservices.CreatmemberAsync(model, ct);
                if (membercreat)
                {
                    TempData["SuccessMasage"] = "Member Created Successfull";
                }
                else
                {
                    TempData["Errormasege"] = "Faild To Create Member";
                }


                return RedirectToAction("index");
            }



            return View(model); 
        }
        [HttpGet]
        public async Task<IActionResult> MemberDetails(int id,CancellationToken ct)
        {
            var details = await _memberservices.GetMemberDetails( id, ct);
            if(details is null)
            {
                TempData["ErrorMassege"] = "member not found";
                return RedirectToAction("index");
            }
              return View(details);
        }

        public async Task<IActionResult> HealthRecordDetails(int id,CancellationToken ct)
        {
            var result=await _memberservices.GetMemberHealtyRecord(id, ct);
            if (result is null)
            {
                TempData["ErrorMassege"] = "member not found";
                return RedirectToAction("index");
            }
            
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> EditMember(int id,CancellationToken ct) 
        {
           var result=await _memberservices.GetMemberToUpdateAsync(id, ct);
            if (result == null)
            {
                TempData["ErrorMassege"] = "member not found";
                return RedirectToAction("index");
            }
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> EditMember(int id,MemberToUpdateViewModel model ,CancellationToken ct)
        {
            if (ModelState.IsValid)
            {
                var membercreat = await _memberservices.UpdatememberAsync(id,model, ct);
                if (membercreat)
                {
                    TempData["SuccessMasage"] = "Member Update Successfull";
                }
                else
                {
                    TempData["Errormasege"] = "Faild To Update Member";
                }


                return RedirectToAction("index");
            }



            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id,CancellationToken ct)
        {
            var result = await _memberservices.GetMemberDetails(id, ct);
            if (result == null)
            {
                TempData["ErrorMassege"] = "member not found";
                return RedirectToAction("index");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken ct)
        {
           var result=await _memberservices.DeletmemberAsync(id, ct);
            if (result)
            {
                TempData["SuccessMasage"] = "Member Delete Successfull";
            }
            else
            {
                TempData["Errormasege"] = "Faild To Delete Member";
            }
            return RedirectToAction("index");
        }
    }
}
