namespace diplom.Models;
using Microsoft.EntityFrameworkCore;

    public class MainContext: DbContext
    {
        private static MainContext instance;
        public static MainContext Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MainContext();

                }
                return instance;
            }
        }

    public DbSet<Admin> Admins { get; set; }
    public DbSet<catalog_product> catalog_Product { get; set; }
    public DbSet<Package> Package { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<Rate> Rate { get; set; }
    public DbSet<_catalog> _catalog { get; set; }
    public DbSet<_order> _orders { get; set; }
    public DbSet<_Provider> _Provider{ get; set; }
    public DbSet<_status> _statuses { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<order_detail> order_detail { get; set; }

    public DbSet<UserBasket> User_Baskets { get; set; }
    public DbSet<UserLikes> UserLike { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<_order>()
            .HasMany(o => o.Products)
            .WithMany(p => p._Orders)
            .UsingEntity<order_detail>(
                j => j.HasOne(od => od.product)
                .WithMany(p => p.order_Details)
                .HasForeignKey(od => od.id_product),

                j => j.HasOne(od => od._Order)
                .WithMany(o => o.order_Details)
                .HasForeignKey(od => od.id_order),

                j =>
                {
                    j.HasKey(t => new { t.id_order , t.id_product});
                    j.ToTable("order_details");
                }

            );

        //modelBuilder.Entity<_order>()
        //    .HasOne(o=>o.Status)
        //    .WithMany()
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseSqlServer(@"Data Source=192.168.227.12;User ID=user04;Password=04;database=1myay;TrustServerCertificate=True");

        //optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-SD2NSU5\MSSQLSERVER05;User ID=sa;Password=12345;database=this10;TrustServerCertificate=True");

        //optionsBuilder.UseSqlServer("Data Source=DESKTOP-3FU748J;Database=misha;Integrated Security = sspi; Encrypt=False;");
        optionsBuilder.UseSqlServer(@"Data Source=hnt8.ru,1433;User ID=sa;Password=_RasulkotV2;database=Aleksey_BD_finish;TrustServerCertificate=True");

    }
    public MainContext()
    {
    }
}

