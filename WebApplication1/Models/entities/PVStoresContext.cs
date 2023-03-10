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
            modelBuilder.Entity<BillDetail>().HasKey(bid => new { bid.ID, bid.BillID, bid.ProductID });
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillDetail> BillDetails { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
