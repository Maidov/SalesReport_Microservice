using Microsoft.EntityFrameworkCore;

namespace ReportApi.Models
{
    public class SalesContext : DbContext
    {
        public SalesContext() { }
        public SalesContext(DbContextOptions<SalesContext> options)
            : base(options)
        {
        }

        public DbSet<SalesItem> SalesItems { get; set; }
    }
}