using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Scoreboard
{
    public class CandidateDbContext: DbContext
    {
        public CandidateDbContext(): base() { }
        public CandidateDbContext(DbContextOptions<CandidateDbContext> options): base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=scoreboard.db");
            }

        }

        public DbSet<CandidateInfo> Candidates { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CandidateInfo>()
                    .HasKey(e => e.Number);

            modelBuilder.Entity<CandidateInfo>()
                    .Property(p => p.Name)
                    .IsRequired();

            modelBuilder.Entity<CandidateInfo>()
                    .Property(p => p.Company)
                    .IsRequired();
        }
    }
}
