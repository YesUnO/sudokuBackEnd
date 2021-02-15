using Microsoft.EntityFrameworkCore;
using sudokuBackEnd.DB.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sudokuBackEnd.DB.Data
{
    public class SudokuContext: DbContext
    {
        public SudokuContext(DbContextOptions<SudokuContext> options): base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<GameEntering> GameEntering { get; set; }
        public DbSet<UserGameSolution> UserGameSolution { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<GameEntering>().ToTable("GameEntering");
            //modelBuilder.Entity<UserGameSolution>()
            //    .HasOne(x => x.ResolvedGameEntering)
            //    .WithOne();
            modelBuilder.Entity<UserGameSolution>()
                .ToTable("UserGameSolution");
        }
    }
}
