using Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class NonProfitCrmDbContext : DbContext
    {
        public NonProfitCrmDbContext()
        {
        }

        public NonProfitCrmDbContext(DbContextOptions<NonProfitCrmDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Organization> Organization { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<UserRoleMapping> UserRoleMapping { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<ContactType> ContactType { get; set; }
        public virtual DbSet<State> State { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Donation> Donation { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString: @"Data Source=tcp:s08.everleap.com;User ID=DB_3221_crm_user;Password=summi786;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
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
            modelBuilder.Entity<Contact>(entity =>
            {
                entity.Property(e => e.AddressCity).HasMaxLength(50);

                entity.Property(e => e.AddressCountry).HasMaxLength(50);

                entity.Property(e => e.AddressLine1).HasMaxLength(128);

                entity.Property(e => e.AddressLine2).HasMaxLength(128);

                entity.Property(e => e.AddressState).HasMaxLength(50);

                entity.Property(e => e.AddressStreet).HasMaxLength(128);

                entity.Property(e => e.AddressZipcode).HasMaxLength(50);

                

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Name).IsRequired();

                //entity.Ignore(e => e.PhoneCode);

                //entity.Property(e => e.ImagePath).HasMaxLength(500);
                entity.Ignore(e => e.ImageFile);

                entity.Property(e => e.OrgId)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.ContactType)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.ContactTypeId)
                    .HasConstraintName("FK_Contact_ContactType");
            });

            modelBuilder.Entity<ContactType>(entity =>{
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Id)
                    .IsRequired();
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.Property(e => e.CountryId);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Abbreviation)
                        .HasMaxLength(100);

                entity.HasOne(s => s.Country)
                        .WithMany(c => c.States)
                        .HasForeignKey(s => s.CountryId)
                        .HasConstraintName("FK_State_Country_CountryId");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Id);

                entity.Property(e => e.TwoLetterISOCode)
                                .HasMaxLength(50);

                entity.Property(e => e.ThreeLetterISOCode)
                                .HasMaxLength(50);

                entity.Property(e => e.NumericISOCode);

                entity.Property(e => e.Name)
                                .HasMaxLength(250);

                entity.Property(e => e.PhoneCode);
            });

            modelBuilder.Entity<Donation>(entity =>
            {
                entity.Property(e => e.Id);

                entity.Property(e => e.OrgId);

                entity.Property(e => e.EventId);

                entity.Property(e => e.CampaignId);

                entity.Property(e => e.ContactId);

                entity.Property(e => e.GuestEmail)
                        .HasMaxLength(50);

                entity.Property(e => e.Amount)
                        .IsRequired();

                entity.Property(e => e.RecurringDonation);

                entity.Property(e => e.Date)
                        .IsRequired();

                entity.Property(e => e.TransactionTypeId);

                entity.Property(e => e.DonationTypeId);

            });
        }
    }
}
