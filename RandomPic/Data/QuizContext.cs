using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;


namespace RandomPic.Data
{
    public class QuizContext : DbContext
    {

        public QuizContext(DbContextOptions<QuizContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        public DbSet<Model.Quiz> Quizzes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Model.Quiz>()
                    .Property(e => e.Selections)
                    .HasConversion(
                        v => string.Join("|", v),
                        v => v.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToList()
                    );

            modelBuilder.Entity<Model.Quiz>()
                    .Property(e => e.HasChosen)
                    .HasDefaultValue(false);
        }

    }
}
