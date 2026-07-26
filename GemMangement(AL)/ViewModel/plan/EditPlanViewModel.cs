using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemMangement_AL_.ViewModel.plan
{
    public class EditPlanViewModel
    {
        [Required(ErrorMessage ="Name is Required")]
        [RegularExpression(@"^[a-ZA-z\s]+$",ErrorMessage = "Name can only contain letters and spaces\"")]
        public string Name { get; set; } = default;
 
        public int Deuration { get; set; } = default;

    }
}
