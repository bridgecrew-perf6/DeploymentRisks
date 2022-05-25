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
                optionsBuilder.UseNpgsql("Host=host.docker.internal;Port=5432;Database=DeploymentRisks;Username=postgres;Password=qwerty123", builder =>
                {
                    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                });
                base.OnConfiguring(optionsBuilder);                
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
