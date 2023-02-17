using Microsoft.EntityFrameworkCore;

namespace BensModManager.Data
{
    public class BensModManagerContext : DbContext
    {
        public BensModManagerContext (DbContextOptions<BensModManagerContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Modification> Modification { get; set; }
    }
}
