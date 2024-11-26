using Microsoft.EntityFrameworkCore;
using OnlineSystemStore.Domain.Tables;


namespace OnlineSystemStore.DAL.DB
{
    public class OnlineDataStore : DbContext
    {
        public OnlineDataStore(DbContextOptions<OnlineDataStore> options) : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
