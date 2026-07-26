using GemMangement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemMangement.DAL.Models
{
    public class MemberShip:BaseEntity
    {
        public Member member {  get; set; }
        public int memberid { get; set;}

        public plane plane { get; set; }
        public int planeid { get; set; }
         public DateTime EndDate { get; set; }
        public bool IsActive => EndDate > DateTime.UtcNow;

        public string Status => EndDate > DateTime.UtcNow ? "Active" : "Expired";
    }
}
