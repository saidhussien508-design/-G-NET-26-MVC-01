using GemMangement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemMangement.DAL.Models
{
    public  class Member:GemUser
    {
        public string? photo {  get; set; }
        public HealthRecord HealthRecord { get; set; }  
        public ICollection <MemberShip> plane { get; set; }
        public ICollection<Booking> bookings { get; set; }
    }
}
