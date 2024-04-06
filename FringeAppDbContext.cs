
using FringesMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FringesMVC
{
    public class FringeAppDbContext : IdentityDbContext<IdentityUser>
    {
        public FringeAppDbContext(DbContextOptions<FringeAppDbContext> options) : base(options)
        {

        }

        // public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }



        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<Message>().HasKey(e => e.Id);
        //}
    }
}
