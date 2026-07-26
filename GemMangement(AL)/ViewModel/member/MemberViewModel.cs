using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemMangement_AL_.ViewModel.member
{
    public  class MemberViewModel
    {
        public int id { get; set; }
        public string? Photo { get; set; }

     

        public string Name { get; set; } = default!;

 

        public string Email { get; set; } =default;



        public string Phone { get; set; } = default!;


        public string Gender { get; set; } =default!;


// Member Details



        public string DateOfBirth { get; set; } = default!;


        public string Address { get; set; }= default;

        public string? PlanName { get; set; }= default;



        public string? MembershipStartDate { get; set; } = default!;


            public string? MembershipEndDate { get; set; } = default!;
    }
}
