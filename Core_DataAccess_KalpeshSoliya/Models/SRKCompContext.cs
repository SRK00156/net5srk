using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Core_DataAccess_KalpeshSoliya.Models
{
    public partial class SRKCompContext : DbContext
    {
        public SRKCompContext()
        {
        }

        public SRKCompContext(DbContextOptions<SRKCompContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Dept> Depts { get; set; }
        public virtual DbSet<Emp> Emps { get; set; }
        public virtual DbSet<LogTable> LogTables { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=SRKSUR5210LT;Initial Catalog=SRKComp;User Id=sa; Password=Kds@12345");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Dept>(entity =>
            {
                entity.ToTable("Dept");

                entity.Property(e => e.DeptId)
                    .ValueGeneratedNever()
                    .HasColumnName("dept_id");

                entity.Property(e => e.DeptName)
                    .HasMaxLength(100)
                    .HasColumnName("dept_name");
            });

            modelBuilder.Entity<Emp>(entity =>
            {
                entity.ToTable("Emp");

                entity.Property(e => e.EmpId)
                    .ValueGeneratedNever()
                    .HasColumnName("emp_id");

                entity.Property(e => e.DeptId).HasColumnName("dept_id");

                entity.Property(e => e.EmpDesignation)
                    .HasMaxLength(100)
                    .HasColumnName("emp_designation");

                entity.Property(e => e.EmpName)
                    .HasMaxLength(100)
                    .HasColumnName("emp_name");

                entity.Property(e => e.EmpSalary)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("emp_salary");

                entity.HasOne(d => d.Dept)
                    .WithMany(p => p.Emps)
                    .HasForeignKey(d => d.DeptId)
                    .HasConstraintName("FK_Emp_Dept");
            });

            modelBuilder.Entity<LogTable>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.ToTable("LogTable");

                entity.Property(e => e.LogId).ValueGeneratedNever();

                entity.Property(e => e.ActioName).HasMaxLength(100);

                entity.Property(e => e.ControllerName).HasMaxLength(100);

                entity.Property(e => e.CurrentLoginName).HasMaxLength(100);

                entity.Property(e => e.ErrorMessage).HasMaxLength(100);

                entity.Property(e => e.RequestDateTime).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
