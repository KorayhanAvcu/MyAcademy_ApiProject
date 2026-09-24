using Microsoft.EntityFrameworkCore;
using MyAcademy_ApiProject.Entities;

namespace MyAcademy_ApiProject.Context
{
    public class ApiContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=KORAYHAN\\SQLEXPRESS;Database=ApiNewDemoDb;integrated security=true;trust server certificate=true;");
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
