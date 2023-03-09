using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class PVStoresContext : DbContext
    {
        public PVStoresContext(DbContextOptions<PVStoresContext> options) : base(options)
        {

        }

        public PVStoresContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BillDetail>().HasKey(bid => new { bid.bid_id, bid.b_id, bid.p_id});
            base.OnModelCreating(modelBuilder);
        }
        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Bill> Bill { get; set; }
        public virtual DbSet<BillDetail> BillDetail { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Comments> Comment { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Producer> Producer { get; set; }
    }
}
