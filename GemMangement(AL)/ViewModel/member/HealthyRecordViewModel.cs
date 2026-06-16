using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemMangement_AL_.ViewModel.member
{
    public class HealthyRecordViewModel
    {
        [Range(0.1, 300, ErrorMessage = "Height must be greater than 0")]
        public decimal height { get; set; }

        [Range(0.1, 500, ErrorMessage = "weight must be greater than 0")]
        public decimal weight { get; set; }

        [Required(ErrorMessage = "Error Message is Required")]
        [StringLength(3, ErrorMessage = "Blood type Must be 3 caracters or less ")]
        public string BloodType { get; set; }
        public string? Note{ get; set; }=default;
    }
}
