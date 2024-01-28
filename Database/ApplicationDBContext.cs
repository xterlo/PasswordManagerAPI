using PasswordManagerAPI.Core;
using PasswordManagerAPI.Tables;
using static System.Reflection.Metadata.BlobBuilder;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace PasswordManagerAPI.Database
{
    public class ApplicationDBContext : DbContext
    {

        public DbSet<Users> user { get; set; }
        public DbSet<Passwords> password { get; set; }
        public DbSet<Session> session { get; set; }
        public DbSet<Logger> logger { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Session>()
               .HasOne<Users>(e => e.User)
               .WithMany(d => d.Session)
               .HasForeignKey(e => e.UserId);


            modelBuilder.Entity<Logger>()
                .HasOne(u => u.session)
                .WithMany(p => p.Logger)
                .HasForeignKey(p => p.SessionID);
            
            modelBuilder.Entity<Logger>()
                .HasOne(u => u.user)
                .WithMany(p => p.Logger)
                .HasForeignKey(p => p.UserID);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.UseMySql($"server={Env.DBServer};user={Env.DBUser};password={Env.DBPassword};database={Env.DBDatabase};", new MySqlServerVersion(new Version(8, 0, 11)));
            
        }

    }
}
