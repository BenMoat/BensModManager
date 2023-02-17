using Microsoft.EntityFrameworkCore;

namespace ForzaColourSearch.Data
{
    public class ForzaColourSearchContext : DbContext
    {
        public ForzaColourSearchContext (DbContextOptions<ForzaColourSearchContext> options)
            : base(options)
        {
        }

        public DbSet<ForzaColourSearch.Models.Modification> Vehicle { get; set; }
    }
}
