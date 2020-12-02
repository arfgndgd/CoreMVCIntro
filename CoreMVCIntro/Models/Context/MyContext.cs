using CoreMVCIntro.Configuration;
using CoreMVCIntro.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CoreMVCIntro.Models.Context
{

    //EntityFrameCore.SqlServer kütüphanesini indirmeyi de unutmayın..Options ayarlamalarını yapabilmek icin bu gerekecektir.
    public class MyContext:DbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //optionsBuilder.UseSqlServer(connectionString: "server=.;database=CoreCodeFirst;uid=sa;pwd=123");
        //}

        //Dependency Injection yapısı Core platformunuzun arkasında otomatik olarak entegre gelir...Dolayısıyla siz bir veritabanı sınıfınızın constructor'ina parametre olarak bir Options tipinde verirseniz bu parametreye argüman otomatik gönderilir....
        public MyContext(DbContextOptions<MyContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<OrderDetail>().Property(x => x.ID).UseIdentityColumn();
            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        //.Net Core üzerinden migrate yapmak istediginiz takdirde add-migration <parametre> ve sonrasında update-database demeniz gerekir

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }



    }
}
