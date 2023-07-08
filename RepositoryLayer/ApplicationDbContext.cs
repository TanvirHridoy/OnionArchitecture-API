using DomainLayer.EntityMapper;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace RepositoryLayer
{
    public partial class ApplicationDbContext : IdentityDbContext// DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
        }

		//protected override void OnModelCreating(ModelBuilder modelBuilder)
		//{
		//    modelBuilder.ApplyConfiguration(new CustomerMap());
		//    base.OnModelCreating(modelBuilder);
		//}


		public DbSet<Otp> Otp { get; set; }
		public DbSet<OtpType> OtpType { get; set; }
		public DbSet<OtpCarrier> OtpCarrier { get; set; }
		

		//public DbSet<Customer> Customer { get; set; }
		//public DbSet<Product> Product { get; set; }
		// public DbSet<Price> Price { get; set; }
		//public DbSet<Salary> Salary { get; set; }
		//public DbSet<SalaryStab> SalaryStab { get; set; }

	}
}
