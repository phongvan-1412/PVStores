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
                //"Data Source=DESKTOP-SKVUP1L\\PHONGVAN;Initial Catalog=PVStores;Persist Security Info=True;; TrustServerCertificate=True");
                "Data Source=DESKTOP-SKVUP1L\\PHONGVAN;Initial Catalog=PVStores;User ID=sa; Password=21112804Kid;TrustServerCertificate=True;",
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure();
                });
            }
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillDetail> BillDetails { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
