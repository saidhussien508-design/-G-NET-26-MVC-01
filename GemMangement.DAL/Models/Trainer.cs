using GemMangement.DAL.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemMangement.DAL.Models
{
    public  class Trainer:GemUser
    {
        public Specialties specialties {  get; set; } 
    }
}
