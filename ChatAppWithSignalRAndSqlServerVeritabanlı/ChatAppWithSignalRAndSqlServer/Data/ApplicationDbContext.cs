using ChatAppWithSignalRAndSqlServer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatAppWithSignalRAndSqlServer.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
           

            base.OnModelCreating(builder);
            builder.Entity<Message>().HasOne<AppUser>(a => a.Sender).WithMany(x => x.Messages).HasForeignKey(d => d.UserId);
        }
        public DbSet<Message> Messages { get; set; }


    }
}
