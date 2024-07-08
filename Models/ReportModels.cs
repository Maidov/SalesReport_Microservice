namespace ReportApi.Models
{
    public class ReportItem
    {
        public string? Name { get; set; }
        public decimal AverageRevenue { get; set; }
        public decimal AverageExpenses { get; set; }
        public decimal AverageIncome { get; set; }
    }
}

namespace ReportApi.Models
{
    public class MonthlyReport
    {
        public string? Category { get; set; }
        public int Year {get; set; }
        public int Month { get; set; }
        public List<ReportItem>? Items { get; set; }
    }
}