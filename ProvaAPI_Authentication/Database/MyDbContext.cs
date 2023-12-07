using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ProvaAPI_Authentication.Entity;

namespace ProvaAPI_Authentication.Database
{
	public class MyDbContext : DbContext
	{
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<BookEntity> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.ToTable("user");
                entity.HasKey(e => e.IdUser).HasName("PRIMARY");
                entity.Property(e => e.UserName).HasMaxLength(50);
                entity.Property(e => e.Password).HasMaxLength(50);
                entity.Property(e => e.Name).HasMaxLength(50);
                entity.Property(e => e.Surname).HasMaxLength(50);
            });

            modelBuilder.Entity<BookEntity>(entity =>
            {
                entity.ToTable("book");
                entity.HasKey(e => e.IdBook).HasName("PRIMARY");
                entity.Property(e => e.Title).HasMaxLength(50);
                entity.Property(e => e.Author).HasMaxLength(50);
                entity.Property(e => e.PublicationDate).HasColumnType("datetime");
                entity.Property(e => e.IdUser).HasColumnType("int(10)");

                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.IdUser)
                    .IsRequired(false);

            });

            base.OnModelCreating(modelBuilder);
        }
    }
}

