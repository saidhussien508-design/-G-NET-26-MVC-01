using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemMangement_AL_.ViewModel.Anaiytics
{
    public class AnaiyticsViewModel
    {
        public int TotalMember {  get; set; }
        public int totalTranner { get; set; }
        public int ActiveMember { get; set; }
        public int UpcommingSession { get; set; }
        public int OnGoingSession { get; set; }
        public int CompletedSession { get; set; }
    }
}
