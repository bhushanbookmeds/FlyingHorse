using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NonProfitCRM.Models
{
    public partial class DB_3221_crmContext : DbContext
    {
        public DB_3221_crmContext()
        {
        }

        public DB_3221_crmContext(DbContextOptions<DB_3221_crmContext> options)
            : base(options)
        {
        }
     
        public virtual DbSet<Organization> Organization { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<UserRoleMapping> UserRoleMapping { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=PCCS-0007\SQLEXPRESS;Initial Catalog=DB_3221_crm;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Organization>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(128)
                    .ValueGeneratedNever();

                entity.Property(e => e.AddressCity).HasMaxLength(50);

                entity.Property(e => e.AddressCountry).HasMaxLength(50);

                entity.Property(e => e.AddressLine1).HasMaxLength(128);

                entity.Property(e => e.AddressLine2).HasMaxLength(128);

                entity.Property(e => e.AddressState).HasMaxLength(50);

                entity.Property(e => e.AddressStreet).HasMaxLength(128);

                entity.Property(e => e.AddressZipcode).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ExpiryDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });
           
            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<UserRoleMapping>(entity =>
            {
                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoleMapping)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_UserRoleMapping_Role");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoleMapping)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserRoleMapping_User");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.AddressCity).HasMaxLength(50);

                entity.Property(e => e.AddressCountry).HasMaxLength(50);

                entity.Property(e => e.AddressLine1).HasMaxLength(128);

                entity.Property(e => e.AddressLine2).HasMaxLength(128);

                entity.Property(e => e.AddressState).HasMaxLength(50);

                entity.Property(e => e.AddressStreet).HasMaxLength(128);

                entity.Property(e => e.AddressZipcode).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.OrgId).HasMaxLength(128);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Users_Organization");
            });
        }
    }
}
