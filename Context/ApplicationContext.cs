using System.Reflection;
using Microsoft.EntityFrameworkCore;
using UniqueTodoApplication.Entities;

namespace UniqueTodoApplication.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        
        public DbSet<Admin> Admins{get;set;}

        public DbSet<Customer> Customers{get;set;}

        public DbSet<Category> Categories{get;set;}

        public DbSet<User> Users{get;set;}

        public DbSet<Role> Roles{get;set;}

        public DbSet<Todoitem> Todoitems{get;set;}

        public DbSet<Item> Items{get;set;}

        public DbSet<ItemSubcategory> ItemSubcategories{get;set;}

        public DbSet<Subcategory> Subcategories{get;set;}

        public DbSet<Reminder> Reminders{get;set;}

        public DbSet<UserRole> UserRoles{get;set;}
    }
}