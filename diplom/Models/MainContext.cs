﻿namespace diplom.Models;
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
    public DbSet<catalog_product> catalog_Products { get; set; }
    public DbSet<Package> Packages { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<Product_Raiting> Product_Raitings { get; set; }
    public DbSet<provider_product> provider_products { get; set; }
    public DbSet<Rate> Rates { get; set; }
    public DbSet<user_basket> user_baskets { get; set; }
    public DbSet<user_like> user_likes { get; set; }
    public DbSet<_catalog> _catalogs { get; set; }
    public DbSet<_order> _orders { get; set; }
    public DbSet<_Provider> _Providers { get; set; }
    public DbSet<_status> _statuses { get; set; }
    public DbSet<_User> _User { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseSqlServer(@"Data Source=192.168.227.12;User ID=user04;Password=04;database=1myay;TrustServerCertificate=True");

        optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-SD2NSU5\MSSQLSERVER05;User ID=sa;Password=12345;database=this;TrustServerCertificate=True");
    }
    public MainContext()
    {
    }
}

