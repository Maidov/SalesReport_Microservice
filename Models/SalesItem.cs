namespace ReportApi.Models
{
    public class SalesItem
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public string? Name { get; set; }
        public string? Category { get; set; }
        public long Amount { get; set; }
        public long Revenue { get; set; }
        public long Expenses { get; set; }
        public long Income { get; set; }
    }
}