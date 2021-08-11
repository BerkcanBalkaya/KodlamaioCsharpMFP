using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    //Context : Db tabloları ile proje classlarını bağlamak anlamında 
    public class NorthwindContext:DbContext
    {
        //Bu method proje hangi veritabanı ile ilişkili o belli edildiği yer
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //@ varsa ters slashları ters slash olarak algıla demek ve sql case insensitive 
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database = Northwind;Trusted_Connection=true");
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

    }
}
