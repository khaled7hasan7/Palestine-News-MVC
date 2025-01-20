using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Palestine_News.DBEntities;

public partial class PalestineNewsContext : DbContext
{
    public PalestineNewsContext()
    {
    }

    public PalestineNewsContext(DbContextOptions<PalestineNewsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=KHALED;Database=Palestine-News;TrustServerCertificate=True;Trusted_Connection=True; User Id=sa;password=Khaled782001; Integrated Security=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoriesId);

            entity.Property(e => e.CategoriesId).HasColumnName("CategoriesID");
            entity.Property(e => e.Attribute).HasMaxLength(100);
            entity.Property(e => e.CategoriesName).HasMaxLength(100);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.Property(e => e.CommentId).HasColumnName("CommentID");
            entity.Property(e => e.Text)
                .HasMaxLength(250)
                .HasColumnName("text");

            entity.HasOne(d => d.News).WithMany(p => p.Comments)
                .HasForeignKey(d => d.Newsid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comments_News");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comments_Users");
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.Property(e => e.NewsId).HasColumnName("NewsID");
            entity.Property(e => e.Content).HasMaxLength(1500);
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.Categories).WithMany(p => p.News)
                .HasForeignKey(d => d.CategoriesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_News_Categories");

            entity.HasOne(d => d.User).WithMany(p => p.News)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_News_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
