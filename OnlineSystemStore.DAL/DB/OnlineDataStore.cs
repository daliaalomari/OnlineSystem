using Microsoft.EntityFrameworkCore;
using OnlineSystemStore.Domain.Tables;


namespace OnlineSystemStore.DAL.DB
{
    public class OnlineDataStore : DbContext
    {
        public OnlineDataStore(DbContextOptions<OnlineDataStore> options) : base(options) { }
        DbSet<Product> products { get; set; }
        DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
