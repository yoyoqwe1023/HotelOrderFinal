using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HotelOrderFinal.Models
{
    public partial class HotelOrderContext : DbContext
    {
        public HotelOrderContext()
        {
        }

        public HotelOrderContext(DbContextOptions<HotelOrderContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Action> Actions { get; set; } = null!;
        public virtual DbSet<ActionDescribe> ActionDescribes { get; set; } = null!;
        public virtual DbSet<Activity> Activities { get; set; } = null!;
        public virtual DbSet<ActivityReference> ActivityReferences { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Discount> Discounts { get; set; } = null!;
        public virtual DbSet<DiscountReference> DiscountReferences { get; set; } = null!;
        public virtual DbSet<Facility> Facilities { get; set; } = null!;
        public virtual DbSet<HotelFacility> HotelFacilities { get; set; } = null!;
        public virtual DbSet<HotelIndustry> HotelIndustries { get; set; } = null!;
        public virtual DbSet<HotelRegionName> HotelRegionNames { get; set; } = null!;
        public virtual DbSet<MultipleFacility> MultipleFacilities { get; set; } = null!;
        public virtual DbSet<MultipleHotelFacility> MultipleHotelFacilities { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<OrderDetailStatus> OrderDetailStatuses { get; set; } = null!;
        public virtual DbSet<OrderSpecialRequest> OrderSpecialRequests { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<RoomAdmin> RoomAdmins { get; set; } = null!;
        public virtual DbSet<RoomClass> RoomClasses { get; set; } = null!;
        public virtual DbSet<RoomImage> RoomImages { get; set; } = null!;
        public virtual DbSet<RoomMember> RoomMembers { get; set; } = null!;
        public virtual DbSet<RoomStyle> RoomStyles { get; set; } = null!;
        public virtual DbSet<RoomTimePrice> RoomTimePrices { get; set; } = null!;
        public virtual DbSet<SpecialRequest> SpecialRequests { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=HotelOrder;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Action>(entity =>
            {
                entity.ToTable("Action");

                entity.Property(e => e.ActionId).HasColumnName("ActionID");

                entity.Property(e => e.ActionDescript).HasMaxLength(20);

                entity.Property(e => e.ActionName).HasMaxLength(20);
            });

            modelBuilder.Entity<ActionDescribe>(entity =>
            {
                entity.HasKey(e => e.ActionToRoomId);

                entity.ToTable("ActionDescribe");

                entity.Property(e => e.ActionToRoomId).HasColumnName("ActionToRoomID");

                entity.Property(e => e.ActioId).HasColumnName("ActioID");

                entity.Property(e => e.MemberId)
                    .HasMaxLength(20)
                    .HasColumnName("MemberID");

                entity.Property(e => e.RoomId)
                    .HasMaxLength(20)
                    .HasColumnName("RoomID");

                entity.HasOne(d => d.Actio)
                    .WithMany(p => p.ActionDescribes)
                    .HasForeignKey(d => d.ActioId)
                    .HasConstraintName("FK_ActionDescribe_Action");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.ActionDescribes)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK_ActionDescribe_RoomMember");
            });

            modelBuilder.Entity<Activity>(entity =>
            {
                entity.ToTable("Activity");

                entity.Property(e => e.ActivityId).HasColumnName("ActivityID");

                entity.Property(e => e.ActivityCost).HasMaxLength(10);

                entity.Property(e => e.ActivityImage).HasMaxLength(50);

                entity.Property(e => e.ActivityPlace).HasMaxLength(50);

                entity.Property(e => e.ActivityTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<ActivityReference>(entity =>
            {
                entity.ToTable("ActivityReference");

                entity.Property(e => e.ActivityReferenceId).HasColumnName("ActivityReferenceID");

                entity.Property(e => e.ActivityId).HasColumnName("ActivityID");

                entity.Property(e => e.OrderId)
                    .HasMaxLength(20)
                    .HasColumnName("OrderID");

                entity.HasOne(d => d.Activity)
                    .WithMany(p => p.ActivityReferences)
                    .HasForeignKey(d => d.ActivityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActivityReference_Activity");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.ActivityReferences)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_ActivityReference_Orders");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.CommentDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CommentPoint).HasColumnType("numeric(10, 2)");

                entity.Property(e => e.MemberId)
                    .HasMaxLength(20)
                    .HasColumnName("MemberID");

                entity.Property(e => e.RoomClassId)
                    .HasMaxLength(20)
                    .HasColumnName("RoomClassID");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK_Comments_RoomMember");

                entity.HasOne(d => d.RoomClass)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.RoomClassId)
                    .HasConstraintName("FK_Comments_RoomClass");
            });

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.ToTable("Discount");

                entity.Property(e => e.DiscountId).HasColumnName("DiscountID");

                entity.Property(e => e.DiscountDiscount).HasColumnType("numeric(10, 2)");

                entity.Property(e => e.DiscountEnd).HasColumnType("datetime");

                entity.Property(e => e.DiscountName).HasMaxLength(20);

                entity.Property(e => e.DiscountStart).HasColumnType("datetime");
            });

            modelBuilder.Entity<DiscountReference>(entity =>
            {
                entity.ToTable("DiscountReference");

                entity.Property(e => e.DiscountReferenceId).HasColumnName("DiscountReferenceID");

                entity.Property(e => e.DiscountId).HasColumnName("DiscountID");

                entity.Property(e => e.MemberId)
                    .HasMaxLength(20)
                    .HasColumnName("MemberID");

                entity.HasOne(d => d.Discount)
                    .WithMany(p => p.DiscountReferences)
                    .HasForeignKey(d => d.DiscountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DiscountReference_Discount");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.DiscountReferences)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK_DiscountReference_RoomMember");
            });

            modelBuilder.Entity<Facility>(entity =>
            {
                entity.Property(e => e.FacilityId).HasColumnName("FacilityID");

                entity.Property(e => e.FacilityName).HasMaxLength(20);
            });

            modelBuilder.Entity<HotelFacility>(entity =>
            {
                entity.Property(e => e.HotelFacilityId).HasColumnName("HotelFacilityID");

                entity.Property(e => e.HotelFacilityName).HasMaxLength(20);
            });

            modelBuilder.Entity<HotelIndustry>(entity =>
            {
                entity.HasKey(e => e.HotelId);

                entity.Property(e => e.HotelId).HasColumnName("HotelID");

                entity.Property(e => e.HotelAddress).HasMaxLength(50);

                entity.Property(e => e.HotelImageDiscription).HasMaxLength(50);

                entity.Property(e => e.HotelName).HasMaxLength(50);

                entity.Property(e => e.HotelPhone).HasMaxLength(20);

                entity.Property(e => e.HotelRegionId).HasColumnName("HotelRegionID");

                entity.HasOne(d => d.HotelRegion)
                    .WithMany(p => p.HotelIndustries)
                    .HasForeignKey(d => d.HotelRegionId)
                    .HasConstraintName("FK_HotelIndustries_HotelRegionName");
            });

            modelBuilder.Entity<HotelRegionName>(entity =>
            {
                entity.HasKey(e => e.HotelRegionId);

                entity.ToTable("HotelRegionName");

                entity.Property(e => e.HotelRegionId).HasColumnName("HotelRegionID");

                entity.Property(e => e.RegionName).HasMaxLength(50);
            });

            modelBuilder.Entity<MultipleFacility>(entity =>
            {
                entity.HasKey(e => e.MultipleFacilitiesId);

                entity.Property(e => e.MultipleFacilitiesId).HasColumnName("MultipleFacilitiesID");

                entity.Property(e => e.FacilityId).HasColumnName("FacilityID");

                entity.Property(e => e.RoomId)
                    .HasMaxLength(20)
                    .HasColumnName("RoomID");

                entity.HasOne(d => d.Facility)
                    .WithMany(p => p.MultipleFacilities)
                    .HasForeignKey(d => d.FacilityId)
                    .HasConstraintName("FK_MultipleFacilities_Facilities");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.MultipleFacilities)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK_MultipleFacilities_Room");
            });

            modelBuilder.Entity<MultipleHotelFacility>(entity =>
            {
                entity.Property(e => e.MultipleHotelFacilityId).HasColumnName("MultipleHotelFacilityID");

                entity.Property(e => e.HotelFacilityId).HasColumnName("HotelFacilityID");

                entity.Property(e => e.HotelId).HasColumnName("HotelID");

                entity.HasOne(d => d.HotelFacility)
                    .WithMany(p => p.MultipleHotelFacilities)
                    .HasForeignKey(d => d.HotelFacilityId)
                    .HasConstraintName("FK_MultipleHotelFacilities_HotelFacilities");

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.MultipleHotelFacilities)
                    .HasForeignKey(d => d.HotelId)
                    .HasConstraintName("FK_MultipleHotelFacilities_HotelIndustries");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId)
                    .HasMaxLength(20)
                    .HasColumnName("OrderID");

                entity.Property(e => e.MemberId)
                    .HasMaxLength(20)
                    .HasColumnName("MemberID");

                entity.Property(e => e.OrderClosed).HasMaxLength(20);

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.OrderTotalPrice).HasColumnType("money");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK_Orders_RoomMember");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.Property(e => e.OrderDetailId)
                    .HasMaxLength(20)
                    .HasColumnName("OrderDetailID");

                entity.Property(e => e.MoveInDate).HasColumnType("datetime");

                entity.Property(e => e.MoveOutDate).HasColumnType("datetime");

                entity.Property(e => e.OrderDetailStatusId).HasColumnName("OrderDetailStatusID");

                entity.Property(e => e.OrderId)
                    .HasMaxLength(20)
                    .HasColumnName("OrderID");

                entity.Property(e => e.PaymentDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

                entity.Property(e => e.PaymentPrice).HasColumnType("money");

                entity.Property(e => e.RoomId)
                    .HasMaxLength(20)
                    .HasColumnName("RoomID");

                entity.HasOne(d => d.OrderDetailStatus)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderDetailStatusId)
                    .HasConstraintName("FK_OrderDetails_OrderDetailStatus");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderDetails_Orders");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.PaymentId)
                    .HasConstraintName("FK_OrderDetails_Payments");
            });

            modelBuilder.Entity<OrderDetailStatus>(entity =>
            {
                entity.ToTable("OrderDetailStatus");

                entity.Property(e => e.OrderDetailStatusId).HasColumnName("OrderDetailStatusID");

                entity.Property(e => e.OrderDetaiStatusContent).HasMaxLength(20);
            });

            modelBuilder.Entity<OrderSpecialRequest>(entity =>
            {
                entity.ToTable("OrderSpecialRequest");

                entity.Property(e => e.OrderSpecialRequestId).HasColumnName("OrderSpecialRequestID");

                entity.Property(e => e.OrderDetailId)
                    .HasMaxLength(20)
                    .HasColumnName("OrderDetailID");

                entity.Property(e => e.SpecialRequestId).HasColumnName("SpecialRequestID");

                entity.HasOne(d => d.OrderDetail)
                    .WithMany(p => p.OrderSpecialRequests)
                    .HasForeignKey(d => d.OrderDetailId)
                    .HasConstraintName("FK_OrderSpecialRequest_OrderDetails");

                entity.HasOne(d => d.SpecialRequest)
                    .WithMany(p => p.OrderSpecialRequests)
                    .HasForeignKey(d => d.SpecialRequestId)
                    .HasConstraintName("FK_OrderSpecialRequest_SpecialRequest");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

                entity.Property(e => e.Payments).HasMaxLength(20);
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Room");

                entity.Property(e => e.RoomId)
                    .HasMaxLength(20)
                    .HasColumnName("RoomID");

                entity.Property(e => e.AdminId)
                    .HasMaxLength(20)
                    .HasColumnName("AdminID");

                entity.Property(e => e.Favorite).HasColumnName("favorite");

                entity.Property(e => e.HotelId).HasColumnName("HotelID");

                entity.Property(e => e.RoomClassId)
                    .HasMaxLength(20)
                    .HasColumnName("RoomClassID");

                entity.Property(e => e.RoomStatus).HasMaxLength(20);

                entity.Property(e => e.RoomStyleId)
                    .HasMaxLength(20)
                    .HasColumnName("RoomStyleID");

                entity.HasOne(d => d.Admin)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.AdminId)
                    .HasConstraintName("FK_Room_RoomAdmin");

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.HotelId)
                    .HasConstraintName("FK_Room_HotelIndustries");

                entity.HasOne(d => d.RoomClass)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.RoomClassId)
                    .HasConstraintName("FK_Room_RoomClass");

                entity.HasOne(d => d.RoomStyle)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.RoomStyleId)
                    .HasConstraintName("FK_Room_RoomStyle");
            });

            modelBuilder.Entity<RoomAdmin>(entity =>
            {
                entity.HasKey(e => e.AdminId);

                entity.ToTable("RoomAdmin");

                entity.Property(e => e.AdminId)
                    .HasMaxLength(20)
                    .HasColumnName("AdminID");

                entity.Property(e => e.AdminName).HasMaxLength(20);

                entity.Property(e => e.AdminPassword).HasMaxLength(20);
            });

            modelBuilder.Entity<RoomClass>(entity =>
            {
                entity.ToTable("RoomClass");

                entity.Property(e => e.RoomClassId)
                    .HasMaxLength(20)
                    .HasColumnName("RoomClassID");

                entity.Property(e => e.RoomClassName).HasMaxLength(20);
            });

            modelBuilder.Entity<RoomImage>(entity =>
            {
                entity.HasKey(e => e.RoomImagesId);

                entity.ToTable("RoomImage");

                entity.Property(e => e.RoomImagesId).HasColumnName("RoomImagesID");

                entity.Property(e => e.RoomId)
                    .HasMaxLength(20)
                    .HasColumnName("RoomID");

                entity.Property(e => e.RoomImage1).HasColumnName("RoomImage");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.RoomImages)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK_RoomImage_Room");
            });

            modelBuilder.Entity<RoomMember>(entity =>
            {
                entity.HasKey(e => e.MemberId);

                entity.ToTable("RoomMember");

                entity.Property(e => e.MemberId)
                    .HasMaxLength(20)
                    .HasColumnName("MemberID");

                entity.Property(e => e.AdminId)
                    .HasMaxLength(20)
                    .HasColumnName("AdminID");

                entity.Property(e => e.MemberBirthday).HasColumnType("date");

                entity.Property(e => e.MemberEmail).HasColumnName("MemberEMail");

                entity.Property(e => e.MemberGender).HasMaxLength(1);

                entity.Property(e => e.MemberIdentity).HasMaxLength(20);

                entity.Property(e => e.MemberName).HasMaxLength(20);

                entity.Property(e => e.MemberPassword).HasMaxLength(20);

                entity.Property(e => e.MemberPhone).HasMaxLength(20);

                entity.HasOne(d => d.Admin)
                    .WithMany(p => p.RoomMembers)
                    .HasForeignKey(d => d.AdminId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoomMember_RoomAdmin");
            });

            modelBuilder.Entity<RoomStyle>(entity =>
            {
                entity.ToTable("RoomStyle");

                entity.Property(e => e.RoomStyleId)
                    .HasMaxLength(20)
                    .HasColumnName("RoomStyleID");

                entity.Property(e => e.RoomStyleName).HasMaxLength(50);
            });

            modelBuilder.Entity<RoomTimePrice>(entity =>
            {
                entity.HasKey(e => e.TimePriceId);

                entity.ToTable("RoomTimePrice");

                entity.Property(e => e.TimePriceId).HasColumnName("TimePriceID");

                entity.Property(e => e.HolidayPrice).HasColumnType("decimal(10, 0)");

                entity.Property(e => e.RoomClassId)
                    .HasMaxLength(20)
                    .HasColumnName("RoomClassID");

                entity.Property(e => e.WeekdayPrice).HasColumnType("decimal(10, 0)");

                entity.HasOne(d => d.RoomClass)
                    .WithMany(p => p.RoomTimePrices)
                    .HasForeignKey(d => d.RoomClassId)
                    .HasConstraintName("FK_RoomTimePrice_RoomClass");
            });

            modelBuilder.Entity<SpecialRequest>(entity =>
            {
                entity.ToTable("SpecialRequest");

                entity.Property(e => e.SpecialRequestId).HasColumnName("SpecialRequestID");

                entity.Property(e => e.SpecialRequestName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
