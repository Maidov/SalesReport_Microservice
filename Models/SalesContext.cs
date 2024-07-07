using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class SalesContext : DbContext
    {
        public SalesContext() { }
        public SalesContext(DbContextOptions<SalesContext> options)
            : base(options)
        {
        }

        // DbSet и другие настройки вашего контекста
        public DbSet<SalesItem> SalesItems { get; set; }
    }
}