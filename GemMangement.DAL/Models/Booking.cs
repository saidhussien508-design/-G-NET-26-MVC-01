using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemMangement.DAL.Models
{
    public  class Booking:BaseEntity
    {
        public Session Session { get; set; }
        public int Sessionid {  get; set; }

        public Member Member { get; set; }  
        public int Memberid { get; set; }
        public bool IsAttneded {  get; set; }
    }
}
