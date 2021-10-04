using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace FarmerScheme.Models
{
    public partial class ProjectGladiatorContext : DbContext
    {
        public ProjectGladiatorContext()
        {
        }

        public ProjectGladiatorContext(DbContextOptions<ProjectGladiatorContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<Bidder> Bidders { get; set; }
        public virtual DbSet<Claim> Claims { get; set; }
        public virtual DbSet<CropRequest> CropRequests { get; set; }
        public virtual DbSet<CropSoldHistory> CropSoldHistories { get; set; }
        public virtual DbSet<FarmerBidder> FarmerBidders { get; set; }
        public virtual DbSet<InsurancePolicy> InsurancePolicies { get; set; }
        public virtual DbSet<Random> Randoms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=JULIA\\SERVER;Database=ProjectGladiator;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.EmailId)
                    .HasName("PK__Admin__7ED91AEFF9694471");

                entity.ToTable("Admin");

                entity.Property(e => e.EmailId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EmailID");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Bank>(entity =>
            {
                entity.HasKey(e => e.AccNo)
                    .HasName("PK__Bank__91CBCB5352C7B791");

                entity.ToTable("Bank");

                entity.Property(e => e.AccNo)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Ifsc)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("IFSC");

                entity.Property(e => e.UniqueId).HasColumnName("UniqueID");

                entity.HasOne(d => d.Unique)
                    .WithMany(p => p.Banks)
                    .HasForeignKey(d => d.UniqueId)
                    .HasConstraintName("FK__Bank__UniqueID__398D8EEE");
            });

            modelBuilder.Entity<Bidder>(entity =>
            {
                entity.HasKey(e => e.BiddingId)
                    .HasName("PK__Bidder__B1AE7D67764522EE");

                entity.ToTable("Bidder");

                entity.Property(e => e.BiddingId).HasColumnName("BiddingID");

                entity.Property(e => e.BidAmount).HasColumnType("money");

                entity.Property(e => e.CropId).HasColumnName("CropID");

                entity.Property(e => e.SellStatus)
                    .HasColumnName("sellStatus")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UniqueId).HasColumnName("UniqueID");

                entity.HasOne(d => d.Crop)
                    .WithMany(p => p.Bidders)
                    .HasForeignKey(d => d.CropId)
                    .HasConstraintName("FK__Bidder__CropID__4316F928");

                entity.HasOne(d => d.Unique)
                    .WithMany(p => p.Bidders)
                    .HasForeignKey(d => d.UniqueId)
                    .HasConstraintName("FK__Bidder__UniqueID__440B1D61");
            });

            modelBuilder.Entity<Claim>(entity =>
            {
                entity.ToTable("Claim");

                entity.Property(e => e.ClaimId).HasColumnName("ClaimID");

                entity.Property(e => e.Approval)
                    .HasColumnName("approval")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.DateOfLoss)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Reason)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.InsuranceNoNavigation)
                    .WithMany(p => p.Claims)
                    .HasForeignKey(d => d.InsuranceNo)
                    .HasConstraintName("FK__Claim__Insurance__4E88ABD4");
            });

            modelBuilder.Entity<CropRequest>(entity =>
            {
                entity.HasKey(e => e.CropId)
                    .HasName("PK__CropRequ__9235613525210214");

                entity.ToTable("CropRequest");

                entity.Property(e => e.CropId).HasColumnName("CropID");

                entity.Property(e => e.Approval)
                    .HasColumnName("approval")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CropName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CropType)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FertilizerType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Msp)
                    .HasColumnType("money")
                    .HasColumnName("MSP");

                entity.Property(e => e.SoilPhcertificate)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("SoilPHCertificate");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("status")
                    .HasDefaultValueSql("('Pending')");

                entity.Property(e => e.UniqueId).HasColumnName("UniqueID");

                entity.HasOne(d => d.Unique)
                    .WithMany(p => p.CropRequests)
                    .HasForeignKey(d => d.UniqueId)
                    .HasConstraintName("FK__CropReque__Uniqu__403A8C7D");
            });

            modelBuilder.Entity<CropSoldHistory>(entity =>
            {
                entity.HasKey(e => e.SoldId)
                    .HasName("PK__CropSold__3F0E66A98E7FC493");

                entity.ToTable("CropSoldHistory");

                entity.Property(e => e.CropId).HasColumnName("CropID");

                entity.Property(e => e.CropName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfSale)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Msp)
                    .HasColumnType("money")
                    .HasColumnName("MSP");

                entity.Property(e => e.SoldPrice).HasColumnType("money");

                entity.Property(e => e.UniqueId).HasColumnName("UniqueID");

                entity.HasOne(d => d.Crop)
                    .WithMany(p => p.CropSoldHistories)
                    .HasForeignKey(d => d.CropId)
                    .HasConstraintName("FK__CropSoldH__CropI__6D0D32F4");

                entity.HasOne(d => d.Unique)
                    .WithMany(p => p.CropSoldHistories)
                    .HasForeignKey(d => d.UniqueId)
                    .HasConstraintName("FK__CropSoldH__Uniqu__6E01572D");
            });

            modelBuilder.Entity<FarmerBidder>(entity =>
            {
                entity.HasKey(e => e.UniqueId)
                    .HasName("PK__FarmerBi__A2A2BAAA1373E4E1");

                entity.ToTable("FarmerBidder");

                entity.HasIndex(e => e.EmailId, "UQ__FarmerBi__7ED91AEE5B6166FD")
                    .IsUnique();

                entity.Property(e => e.UniqueId).HasColumnName("UniqueID");

                entity.Property(e => e.Aadhar)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Certificate)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ContactNo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.EmailId)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("EmailID");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.LandAddress)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LandPincode)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Pan)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Pincode)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.RegType)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<InsurancePolicy>(entity =>
            {
                entity.HasKey(e => e.InsuranceNo)
                    .HasName("PK__Insuranc__743D8973BB58EDEE");

                entity.ToTable("InsurancePolicy");

                entity.Property(e => e.CropName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CropType)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.InsuranceCompany)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PremiumAmount).HasColumnType("money");

                entity.Property(e => e.SumInsured).HasColumnType("money");

                entity.Property(e => e.SumInsuredPh)
                    .HasColumnType("money")
                    .HasColumnName("SumInsuredPH");

                entity.Property(e => e.UniqueId).HasColumnName("UniqueID");

                entity.Property(e => e.ZoneType)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Unique)
                    .WithMany(p => p.InsurancePolicies)
                    .HasForeignKey(d => d.UniqueId)
                    .HasConstraintName("FK__Insurance__Uniqu__47DBAE45");
            });

            modelBuilder.Entity<Random>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Random");

                entity.Property(e => e.SoldDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
