using GemMangement.DAL.Models.Enum;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemMangement.DAL.Models
{
    public  class Trainers:GemUser
    {
        public Specialties specialties {  get; set; } 
        public ICollection<Session> sessions { get; set; }
    }
}
