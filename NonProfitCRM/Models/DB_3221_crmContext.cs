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

        public virtual DbSet<Campaign> Campaign { get; set; }
        public virtual DbSet<CampaignCategory> CampaignCategory { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<ContactType> ContactType { get; set; }
        public virtual DbSet<Donation> Donation { get; set; }
        public virtual DbSet<DonationType> DonationType { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<Event> Picture { get; set; }
        public virtual DbSet<Organization> Organization { get; set; }
        public virtual DbSet<Pledge> Pledge { get; set; }
        public virtual DbSet<TransactionType> TransactionType { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<UserRoleMapping> UserRoleMapping { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<ProjectType> ProjectType { get; set; }
        public virtual DbSet<Expenditures> Expenditures { get; set; }

        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<State> State { get; set; }

        public virtual DbSet<Volunteers>Volunteers { get; set; }
     

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=CRM;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");


            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Campaign>(entity =>
            {
                entity.Property(e => e.AddressCity).HasMaxLength(50);

                entity.Property(e => e.AddressCountry).HasMaxLength(50);

                entity.Property(e => e.AddressLine1).HasMaxLength(128);

                entity.Property(e => e.AddressLine2).HasMaxLength(128);

                entity.Property(e => e.AddressState).HasMaxLength(50);

                entity.Property(e => e.AddressStreet).HasMaxLength(128);

                entity.Property(e => e.AddressZipcode).HasMaxLength(50);

                entity.Property(e => e.OrgId)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Campaign)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Campaign_Category");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Campaign)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_Campaign_Users");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.Campaign)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Campaign_Organization");
            });

            modelBuilder.Entity<CampaignCategory>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.OrgId).HasMaxLength(128);
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

                entity.Property(e => e.DonorScore).HasColumnType("decimal(7, 4)");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.OrgId)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.ContactType)
                    .WithMany(p => p.Contact)
                    .HasForeignKey(d => d.ContactTypeId)
                    .HasConstraintName("FK_Contact_ContactType");
            });

            modelBuilder.Entity<ContactType>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });


            modelBuilder.Entity<Donation>(entity =>
            {
                entity.Property(e => e.Id);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.GuestEmail).HasMaxLength(100);
               

                entity.Property(e => e.OrgId)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.Campaign)
                    .WithMany(p => p.Donation)
                    .HasForeignKey(d => d.CampaignId)
                    .HasConstraintName("FK_Donation_Campaign");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Donation)
                    .HasForeignKey(d => d.ContactId)
                    .HasConstraintName("FK_Donation_Contact");

                entity.HasOne(d => d.DonationType)
                    .WithMany(p => p.Donation)
                    .HasForeignKey(d => d.DonationTypeId)
                    .HasConstraintName("FK_Donation_DonationType");


                entity.HasOne(d => d.Event)
                    .WithMany(p => p.Donation)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("FK_Donation_Event");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.Donation)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Donation_Organization");

                entity.HasOne(d => d.TransactionType)
                    .WithMany(p => p.Donation)
                    .HasForeignKey(d => d.TransactionTypeId)
                    .HasConstraintName("FK_Donation_TransactionType");
            });

            modelBuilder.Entity<DonationType>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.OrgId).HasMaxLength(128);
            });


            modelBuilder.Entity<Volunteers>(entity =>
            {         
                entity.Property(e => e.OrgId)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Name).IsRequired().HasMaxLength(255);

                entity.Property(e => e.PhoneNumber);
                   
                entity.Property(e => e.Age);

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.AddressLine1).HasMaxLength(255);

                entity.Property(e => e.AddressLine2).HasMaxLength(255);

                entity.Property(e => e.AddressStreet).HasMaxLength(255);

                entity.Property(e => e.AddressCity).HasMaxLength(255);

                entity.Property(e => e.AddressState).HasMaxLength(255);

                entity.Property(e => e.AddressCountry).HasMaxLength(255);

                entity.Property(e => e.AddressZipcode).HasMaxLength(6);

                entity.Property(e => e.InstagramProfile).HasMaxLength(255);
            });

          
            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.AddressCity).HasMaxLength(50);

                entity.Property(e => e.AddressCountry).HasMaxLength(50);

                entity.Property(e => e.AddressLine1).HasMaxLength(128);

                entity.Property(e => e.AddressLine2).HasMaxLength(128);

                entity.Property(e => e.AddressState).HasMaxLength(50);

                entity.Property(e => e.AddressStreet).HasMaxLength(128);

                entity.Property(e => e.AddressZipcode).HasMaxLength(6);

                entity.Property(e => e.StartDate).HasColumnType("50");

                entity.Property(e => e.EndDate).HasColumnType("50");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.LongDescription).HasMaxLength(500);

                entity.Property(e => e.ImagePath).HasMaxLength(500);

                entity.Ignore(e => e.ImageFile);

                entity.Property(e => e.OrgId).HasMaxLength(128);

                entity.HasOne(d => d.Campaign)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.CampaignId)
                    .HasConstraintName("FK_Event_Campaign");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Event_Category");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.OrgId)
                    .HasConstraintName("FK_Event_Organization");
            });

            modelBuilder.Entity<Picture>(entity =>
            {
               

                entity.Property(e => e.EntityType).HasMaxLength(128);

                entity.Property(e => e.PictureUrl).HasMaxLength(250);

                
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.Property(e => e.AddressCity).HasMaxLength(50);

                entity.Property(e => e.AddressCountry).HasMaxLength(50);

                entity.Property(e => e.AddressLine1).HasMaxLength(128);

                entity.Property(e => e.AddressLine2).HasMaxLength(128);

                entity.Property(e => e.AddressState).HasMaxLength(50);

                entity.Property(e => e.AddressStreet).HasMaxLength(128);

                entity.Property(e => e.AddressZipcode).HasMaxLength(50);

                entity.Property(e => e.StartDate).HasColumnType("datetime");
                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.OrgId).HasMaxLength(128);
                entity.Property(e => e.Lead).IsRequired();

                entity.Property(e => e.Name).IsRequired();
                entity.HasOne(d => d.Org)
                    .WithMany(p => p.Project)
                    .HasForeignKey(d => d.OrgId)
                    .HasConstraintName("FK_Project_Organization");

                entity.HasOne(d => d.ProjectType)
                    .WithMany(p => p.Project)
                    .HasForeignKey(d => d.ProjectTypeId)
                    .HasConstraintName("FK_Project_ProjectType");
            });
            modelBuilder.Entity<ProjectType>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.OrgId).HasMaxLength(128);
            });
            modelBuilder.Entity<Expenditures>(entity =>
            {
                entity.Property(e => e.Id);
                entity.Property(e => e.ProjectId).HasMaxLength(128);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Date).HasColumnType("datetime");
                entity.Property(e => e.Submitter).IsRequired();
                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
                
                entity.Property(e => e.Invoice);
            });
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

                modelBuilder.Entity<Pledge>(entity =>
                {
                    entity.Property(e => e.Date).HasColumnType("datetime");

                    entity.Property(e => e.OrgId)
                        .IsRequired()
                        .HasMaxLength(128);

                    entity.Property(e => e.PhoneNumber)
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    entity.HasOne(d => d.Campaign)
                        .WithMany(p => p.Pledge)
                        .HasForeignKey(d => d.CampaignId)
                        .HasConstraintName("FK_Pledge_Campaign");

                    entity.HasOne(d => d.Contact)
                        .WithMany(p => p.Pledge)
                        .HasForeignKey(d => d.ContactId)
                        .HasConstraintName("FK_Pledge_Contact");

                    entity.HasOne(d => d.Donation)
                        .WithMany(p => p.Pledge)
                        .HasForeignKey(d => d.DonationId)
                        .HasConstraintName("FK_Pledge_Donation");

                    entity.HasOne(d => d.Event)
                        .WithMany(p => p.Pledge)
                        .HasForeignKey(d => d.EventId)
                        .HasConstraintName("FK_Pledge_Event");


                entity.HasOne(d => d.Org)
                    .WithMany(p => p.Pledge)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pledge_Organization");
            });


                modelBuilder.Entity<TransactionType>(entity =>
                {
                    entity.Property(e => e.Name)
                        .IsRequired()
                        .HasMaxLength(50);

                    entity.Property(e => e.OrgId).HasMaxLength(128);
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

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.TwoLetterISOCode).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.ThreeLetterISOCode).HasMaxLength(50);

                entity.Property(e => e.NumericISOCode);

                entity.Property(e => e.PhoneCode);
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.Property(e => e.CountryId);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Abbreviation).HasMaxLength(100);

                entity.HasOne(s => s.Country)
                    .WithMany(c => c.State)
                    .HasForeignKey(s => s.CountryId)
                    .HasConstraintName("FK_State_Country_CountryId");
            });

        }

    }

                
           
            

        }

    
