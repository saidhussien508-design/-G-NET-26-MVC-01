using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemMangement.DAL.Models
{
    public  class Session:BaseEntity
    {
        public string Description {  get; set; }
        public int  Capacity {  get; set; }
        public DateTime StartDate {  get; set; }    
        public DateTime EndDate {  get; set; }

        public Trainer Trainer { get; set; }    
        public int Trainerid {  get; set; }

        public Category Category { get; set; }  
        public int Categoryid { get; set; }


        public ICollection<Booking> bookings {  get; set; }
    }
}
