using Microsoft.EntityFrameworkCore;
using ELLP_Project.Models;
using System;

namespace ELLP_Project.Persistence.DBContext
{
    
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<AlunoModel> Alunos { get; set; }
        public DbSet<MonitorModel> Monitores { get; set; }
        public DbSet<ProfessorModel> Professores { get; set; }
        public DbSet<OficinaModel> Oficinas { get; set; }
        public DbSet<FaltaModel> Faltas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FaltaModel>()
                .Property(f => f.DataFalta)
                .HasColumnType("date"); 
        }
    }
}

