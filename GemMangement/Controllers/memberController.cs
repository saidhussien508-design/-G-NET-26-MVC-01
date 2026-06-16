using GemMangement.DAL.Models;
using GemMangement_AL_.Servicess.Interfases;
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
    }
}
