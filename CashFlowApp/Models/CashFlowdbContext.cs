using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CashFlowApp.Models
{
    public partial class CashFlowdbContext : DbContext
    {
        public CashFlowdbContext()
        {
        }

        public CashFlowdbContext(DbContextOptions<CashFlowdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CashIn> CashIn { get; set; }
        public virtual DbSet<CashOut> CashOut { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<DayCashIn> DayCashIn { get; set; }
        public virtual DbSet<DayCashOut> DayCashOut { get; set; }
        public virtual DbSet<McashIn> McashIn { get; set; }
        public virtual DbSet<McashOut> McashOut { get; set; }
        public virtual DbSet<Projects> Projects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CashIn>(entity =>
            {
                entity.Property(e => e.ProjectId).HasColumnName("Project_Id");

                entity.Property(e => e.TyearCashIn)
                    .HasColumnName("TYearCashIn")
                    .HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.CashIn)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CashIn_Projects");
            });

            modelBuilder.Entity<CashOut>(entity =>
            {
                entity.Property(e => e.ProjectId).HasColumnName("Project_Id");

                entity.Property(e => e.TyearCashOut)
                    .HasColumnName("TYearCashOut")
                    .HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.CashOut)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CashOut_Projects");
            });

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.HasKey(e => e.CatrgoryId);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<DayCashIn>(entity =>
            {
                entity.HasKey(e => e.DcashInId);

                entity.Property(e => e.DcashInId).HasColumnName("DCashInId");

                entity.Property(e => e.AddedBy)
                    .HasColumnName("Added_by")
                    .HasMaxLength(50);

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Category).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.EntryDate).HasColumnType("datetime");

                entity.Property(e => e.ProjectId).HasColumnName("Project_Id");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.DayCashIn)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DayCashIn_Projects");
            });

            modelBuilder.Entity<DayCashOut>(entity =>
            {
                entity.HasKey(e => e.DcashOutId);

                entity.Property(e => e.DcashOutId).HasColumnName("DCashOutId");

                entity.Property(e => e.AddedBy)
                    .HasColumnName("Added_by")
                    .HasMaxLength(50);

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Category).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.EntryDate).HasColumnType("datetime");

                entity.Property(e => e.ProjectId).HasColumnName("Project_Id");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.DayCashOut)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DayCashOut_Projects");
            });

            modelBuilder.Entity<McashIn>(entity =>
            {
                entity.ToTable("MCashIn");

                entity.Property(e => e.McashInId).HasColumnName("MCashInId");

                entity.Property(e => e.April)
                    .HasColumnName("april")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.August)
                    .HasColumnName("august")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Dec)
                    .HasColumnName("dec")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Fab)
                    .HasColumnName("fab")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Jan)
                    .HasColumnName("jan")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.July)
                    .HasColumnName("july")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.June)
                    .HasColumnName("june")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.March)
                    .HasColumnName("march")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.May)
                    .HasColumnName("may")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Nov)
                    .HasColumnName("nov")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Oct)
                    .HasColumnName("oct")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ProjectId).HasColumnName("Project_Id");

                entity.Property(e => e.Sep)
                    .HasColumnName("sep")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Year).HasColumnName("year");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.McashIn)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MCashIn_Projects");
            });

            modelBuilder.Entity<McashOut>(entity =>
            {
                entity.ToTable("MCashOut");

                entity.Property(e => e.McashOutId).HasColumnName("MCashOutId");

                entity.Property(e => e.April)
                    .HasColumnName("april")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.August)
                    .HasColumnName("august")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Dec)
                    .HasColumnName("dec")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Fab)
                    .HasColumnName("fab")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Jan)
                    .HasColumnName("jan")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.July)
                    .HasColumnName("july")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.June)
                    .HasColumnName("june")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.March)
                    .HasColumnName("march")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.May)
                    .HasColumnName("may")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Nov)
                    .HasColumnName("nov")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Oct)
                    .HasColumnName("oct")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ProjectId).HasColumnName("Project_Id");

                entity.Property(e => e.Sep)
                    .HasColumnName("sep")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Year).HasColumnName("year");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.McashOut)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MCashOut_Projects");
            });

            modelBuilder.Entity<Projects>(entity =>
            {
                entity.HasKey(e => e.ProjectId);

                entity.Property(e => e.Description).HasMaxLength(150);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TcashIn)
                    .HasColumnName("TCashIn")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TcashOut)
                    .HasColumnName("TCashOut")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Trevenue)
                    .HasColumnName("TRevenue")
                    .HasColumnType("decimal(18, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
