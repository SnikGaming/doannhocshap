using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BaiTapNho.models;

public partial class AspContext : DbContext
{
    public AspContext()
    {
    }

    public AspContext(DbContextOptions<AspContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.\\;Initial Catalog=asp;Integrated Security=True;encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.CreateTime)
                .HasColumnType("date")
                .HasColumnName("create_time");
            entity.Property(e => e.EditTime)
                .HasColumnType("date")
                .HasColumnName("edit_time");
            entity.Property(e => e.MemCreate)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("mem_create");
            entity.Property(e => e.MemEdit)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("mem_edit");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Slug).HasMaxLength(150);

            entity.HasOne(d => d.MemCreateNavigation).WithMany(p => p.CategoryMemCreateNavigations)
                .HasForeignKey(d => d.MemCreate)
                .HasConstraintName("FK_Category_Member");

            entity.HasOne(d => d.MemEditNavigation).WithMany(p => p.CategoryMemEditNavigations)
                .HasForeignKey(d => d.MemEdit)
                .HasConstraintName("FK_Category_Member1");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.Username);

            entity.ToTable("Member");

            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Members)
                .HasForeignKey(d => d.Role)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Member_Role");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.ToTable("Post");

            entity.Property(e => e.CatId).HasColumnName("Cat_Id");
            entity.Property(e => e.FullContent).HasColumnName("Full_content");
            entity.Property(e => e.Img).HasMaxLength(350);
            entity.Property(e => e.MemCreate)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("mem_create");
            entity.Property(e => e.MemEdit)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("mem_edit");
            entity.Property(e => e.TimeCreate)
                .HasColumnType("date")
                .HasColumnName("time_create");
            entity.Property(e => e.TimeEdit)
                .HasColumnType("date")
                .HasColumnName("time_edit");
            entity.Property(e => e.Title).HasMaxLength(350);

            entity.HasOne(d => d.Cat).WithMany(p => p.Posts)
                .HasForeignKey(d => d.CatId)
                .HasConstraintName("FK_Post_Category");

            entity.HasOne(d => d.MemCreateNavigation).WithMany(p => p.PostMemCreateNavigations)
                .HasForeignKey(d => d.MemCreate)
                .HasConstraintName("FK_Post_Member1");

            entity.HasOne(d => d.MemEditNavigation).WithMany(p => p.PostMemEditNavigations)
                .HasForeignKey(d => d.MemEdit)
                .HasConstraintName("FK_Post_Member2");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.Name).HasMaxLength(150);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
