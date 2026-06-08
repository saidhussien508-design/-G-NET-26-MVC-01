namespace GemMangement.Models
{
    public class plane
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DurationDate { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpDatedAt { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
    }
}
