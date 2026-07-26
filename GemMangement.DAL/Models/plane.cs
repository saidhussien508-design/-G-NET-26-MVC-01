using GemMangement.DAL.Models;

namespace GemMangement.Models
{
    public class plane:BaseEntity
    {
       
        public string Name { get; set; }
        public int DurationDate { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public ICollection <MemberShip> Members { get; set; }
    }
}
