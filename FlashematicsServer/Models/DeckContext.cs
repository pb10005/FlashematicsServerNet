using Microsoft.EntityFrameworkCore;

namespace FlashematicsServer.Models
{
    public class DeckContext: DbContext
    {

        public DeckContext(DbContextOptions<DeckContext> options)
            : base(options)
        {
        }

        public DbSet<DeckItem> DeckItems { get; set; } = null!;
    }
}
