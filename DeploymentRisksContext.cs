using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DeploymentRisks.DAL;

#nullable disable

namespace DeploymentRisks
{
    public partial class DeploymentRisksContext : DbContext
    {
        public DeploymentRisksContext()
        {
        }

        public DeploymentRisksContext(DbContextOptions<DeploymentRisksContext> options)
            : base(options)
        {
        }
        public DbSet<ToDoTask> ToDoTasks { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=DeploymentRisks;Username=postgres;Password=qwerty123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Russian_Russia.1251");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
