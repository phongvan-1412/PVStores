using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models.entities
{
    public class PVStoresContext : DbContext
    {
        public PVStoresContext(DbContextOptions<PVStoresContext> options) : base(options)
        {

        }

        public PVStoresContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                "Data Source=PVStores.mssql.somee.com;Initial Catalog=PVStores;Persist Security Info=True;User ID=phongvan21112804_SQLLogin_1;Password=f4oj2qbqrb; TrustServerCertificate=True");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BillDetail>().HasKey(bid => new { bid.bid_id, bid.b_id, bid.p_id });
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Account> Account { get; set; }
        public DbSet<Bill> Bill { get; set; }
        public DbSet<BillDetail> BillDetail { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comments> Comment { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Producer> Producer { get; set; }
    }
}
