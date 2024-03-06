using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Final_Project_PRN.Models
{
    public partial class PRN221ProjectContext : DbContext
    {
        public PRN221ProjectContext()
        {
        }

        public PRN221ProjectContext(DbContextOptions<PRN221ProjectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Class> Classes { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<Schedule> Schedules { get; set; } = null!;
        public virtual DbSet<Subject> Subjects { get; set; } = null!;
        public virtual DbSet<Teacher> Teachers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                optionsBuilder.UseSqlServer(config.GetConnectionString("Connect"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>(entity =>
            {
                entity.ToTable("Class");

                entity.Property(e => e.ClassId).HasMaxLength(7);

                entity.Property(e => e.Description).HasMaxLength(250);
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.RoomName);

                entity.ToTable("Room");

                entity.Property(e => e.RoomName).HasMaxLength(5);
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.ToTable("Schedule");

                entity.Property(e => e.Id).HasMaxLength(10);

                entity.Property(e => e.ClassId).HasMaxLength(7);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.Room).HasMaxLength(5);

                entity.Property(e => e.SlotId).HasMaxLength(3);

                entity.Property(e => e.SubjectId).HasMaxLength(7);

                entity.Property(e => e.Teacher).HasMaxLength(10);

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Schedule_Class");

                entity.HasOne(d => d.RoomNavigation)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.Room)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Schedule_Room");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Schedule_Subject");

                entity.HasOne(d => d.TeacherNavigation)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.Teacher)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Schedule_Teacher");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("Subject");

                entity.Property(e => e.SubjectId).HasMaxLength(7);

                entity.Property(e => e.Description).HasMaxLength(350);

                entity.Property(e => e.SubjectName).HasMaxLength(50);
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("Teacher");

                entity.Property(e => e.TeacherId)
                    .HasMaxLength(10)
                    .HasColumnName("TeacherID");

                entity.Property(e => e.TeacherName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
