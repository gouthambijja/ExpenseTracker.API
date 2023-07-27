namespace ExpenseTrackerLogicLayer.Models
{
    public class BLCategory
    {
        public Guid CategoryId { get; set; }

        public string? Name { get; set; }

        public Guid UserId { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsPermanentDelete { get; set; }
    }
}
