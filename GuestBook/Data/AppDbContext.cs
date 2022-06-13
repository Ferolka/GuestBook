using GuestBook.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuestBook.Data
{
    public class AppDbContext: DbContext
    {
        public DbSet<GuestbookEntry> GuestbookEntries { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GuestbookEntry>()
            .Property(f => f.Id)
            .ValueGeneratedOnAdd();
            modelBuilder.UseSerialColumns();

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseNpgsql("Host=localhost;Port=5431;Database=guestbook;Username=postgres;Password=mysecretpassword");
        }
    }
}
