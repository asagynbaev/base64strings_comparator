using Microsoft.EntityFrameworkCore;

namespace RadioFreeEurope.Models
{
    public class DiffDBContext : DbContext
    {
        public DiffDBContext(DbContextOptions<DiffDBContext> options) : base(options) { }

        public DbSet<Diff> Diffs { get; set; }
    }
}
