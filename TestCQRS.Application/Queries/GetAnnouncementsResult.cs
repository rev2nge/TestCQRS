namespace TestCQRS.Application.Queries
{
    public class GetAnnouncementsResult
    {
        public Guid Id { get; set; }
        public int? Number { get; set; }
        public string? Text { get; set; }
        public string? Rate { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
