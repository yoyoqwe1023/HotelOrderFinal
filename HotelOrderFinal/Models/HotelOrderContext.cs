﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
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

        public virtual DbSet<Activity> Activity { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Discount> Discount { get; set; }
        public virtual DbSet<HotelFacility> HotelFacility { get; set; }
        public virtual DbSet<HotelIndustry> HotelIndustry { get; set; }
        public virtual DbSet<HotelRegionName> HotelRegionName { get; set; }
        public virtual DbSet<MultipleHotelFacility> MultipleHotelFacility { get; set; }
        public virtual DbSet<MultipleRoomFacility> MultipleRoomFacility { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<OrderDetailStatus> OrderDetailStatus { get; set; }
        public virtual DbSet<OrderSpecialRequest> OrderSpecialRequest { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<RoomAdmin> RoomAdmin { get; set; }
        public virtual DbSet<RoomClass> RoomClass { get; set; }
        public virtual DbSet<RoomFacility> RoomFacility { get; set; }
        public virtual DbSet<RoomImage> RoomImage { get; set; }
        public virtual DbSet<RoomMember> RoomMember { get; set; }
        public virtual DbSet<SpecialRequest> SpecialRequest { get; set; }

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
            modelBuilder.Entity<Activity>(entity =>
            {
                entity.Property(e => e.ActivityId).HasColumnName("ActivityID");

                entity.Property(e => e.ActivityCost).HasMaxLength(10);

                entity.Property(e => e.ActivityImage).HasMaxLength(50);

                entity.Property(e => e.ActivityName).IsRequired();

                entity.Property(e => e.ActivityPlace).HasMaxLength(50);

                entity.Property(e => e.ActivityTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.CommentDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MemberId)
                    .HasMaxLength(20)
                    .HasColumnName("MemberID");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK_Comments_RoomMember");
            });

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.Property(e => e.DiscountId).HasColumnName("DiscountID");

                entity.Property(e => e.DiscountDiscount).HasColumnType("numeric(10, 2)");

                entity.Property(e => e.DiscountEnd).HasColumnType("datetime");

                entity.Property(e => e.DiscountName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.DiscountStart).HasColumnType("datetime");

                entity.Property(e => e.MemberId)
                    .HasMaxLength(20)
                    .HasColumnName("MemberID");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Discount)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK_Discount_RoomMember");
            });

            modelBuilder.Entity<HotelFacility>(entity =>
            {
                entity.Property(e => e.HotelFacilityId).HasColumnName("HotelFacilityID");

                entity.Property(e => e.HotelFacilityImage).HasMaxLength(50);

                entity.Property(e => e.HotelFacilityName).HasMaxLength(20);
            });

            modelBuilder.Entity<HotelIndustry>(entity =>
            {
                entity.HasKey(e => e.HotelId)
                    .HasName("PK_HotelIndustries");

                entity.Property(e => e.HotelId).HasColumnName("HotelID");

                entity.Property(e => e.HotelAddress).HasMaxLength(50);

                entity.Property(e => e.HotelImage).HasMaxLength(50);

                entity.Property(e => e.HotelImageDiscription).HasMaxLength(50);

                entity.Property(e => e.HotelName).HasMaxLength(50);

                entity.Property(e => e.HotelPhone).HasMaxLength(20);

                entity.Property(e => e.HotelRegionId).HasColumnName("HotelRegionID");

                entity.HasOne(d => d.HotelRegion)
                    .WithMany(p => p.HotelIndustry)
                    .HasForeignKey(d => d.HotelRegionId)
                    .HasConstraintName("FK_HotelIndustries_HotelRegionName");
            });

            modelBuilder.Entity<HotelRegionName>(entity =>
            {
                entity.HasKey(e => e.HotelRegionId);

                entity.Property(e => e.HotelRegionId).HasColumnName("HotelRegionID");

                entity.Property(e => e.RegionName).HasMaxLength(50);
            });

            modelBuilder.Entity<MultipleHotelFacility>(entity =>
            {
                entity.Property(e => e.MultipleHotelFacilityId).HasColumnName("MultipleHotelFacilityID");

                entity.Property(e => e.HotelFacilityId).HasColumnName("HotelFacilityID");

                entity.Property(e => e.HotelId).HasColumnName("HotelID");

                entity.HasOne(d => d.HotelFacility)
                    .WithMany(p => p.MultipleHotelFacility)
                    .HasForeignKey(d => d.HotelFacilityId)
                    .HasConstraintName("FK_MultipleHotelFacilities_HotelFacilities");

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.MultipleHotelFacility)
                    .HasForeignKey(d => d.HotelId)
                    .HasConstraintName("FK_MultipleHotelFacilities_HotelIndustries");
            });

            modelBuilder.Entity<MultipleRoomFacility>(entity =>
            {
                entity.HasKey(e => e.MultipleRoomFacilitiyId)
                    .HasName("PK_MultipleFacilities");

                entity.Property(e => e.MultipleRoomFacilitiyId).HasColumnName("MultipleRoomFacilitiyID");

                entity.Property(e => e.FacilityId).HasColumnName("FacilityID");

                entity.Property(e => e.RoomId)
                    .HasMaxLength(20)
                    .HasColumnName("RoomID");

                entity.HasOne(d => d.Facility)
                    .WithMany(p => p.MultipleRoomFacility)
                    .HasForeignKey(d => d.FacilityId)
                    .HasConstraintName("FK_MultipleFacilities_Facilities");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.MultipleRoomFacility)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK_MultipleFacilities_Room");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId)
                    .HasMaxLength(20)
                    .HasColumnName("OrderID");

                entity.Property(e => e.ActivityId).HasColumnName("ActivityID");

                entity.Property(e => e.DiscountId).HasColumnName("DiscountID");

                entity.Property(e => e.MemberId)
                    .HasMaxLength(20)
                    .HasColumnName("MemberID");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.OrderStatus).HasMaxLength(20);

                entity.Property(e => e.OrderTotalPrice).HasColumnType("money");

                entity.HasOne(d => d.Activity)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.ActivityId)
                    .HasConstraintName("FK_Orders_Activity");

                entity.HasOne(d => d.Discount)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.DiscountId)
                    .HasConstraintName("FK_Orders_Discount");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK_Orders_RoomMember");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.Property(e => e.OrderDetailId)
                    .HasMaxLength(20)
                    .HasColumnName("OrderDetailID");

                entity.Property(e => e.CheckInDate).HasColumnType("datetime");

                entity.Property(e => e.CheckOutDate).HasColumnType("datetime");

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
                    .WithMany(p => p.OrderDetail)
                    .HasForeignKey(d => d.OrderDetailStatusId)
                    .HasConstraintName("FK_OrderDetails_OrderDetailStatus");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetail)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderDetails_Orders");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.OrderDetail)
                    .HasForeignKey(d => d.PaymentId)
                    .HasConstraintName("FK_OrderDetails_Payments");
            });

            modelBuilder.Entity<OrderDetailStatus>(entity =>
            {
                entity.Property(e => e.OrderDetailStatusId).HasColumnName("OrderDetailStatusID");

                entity.Property(e => e.OrderDetaiStatusContent).HasMaxLength(20);
            });

            modelBuilder.Entity<OrderSpecialRequest>(entity =>
            {
                entity.Property(e => e.OrderSpecialRequestId).HasColumnName("OrderSpecialRequestID");

                entity.Property(e => e.OrderDetailId)
                    .HasMaxLength(20)
                    .HasColumnName("OrderDetailID");

                entity.Property(e => e.SpecialRequestId).HasColumnName("SpecialRequestID");

                entity.HasOne(d => d.OrderDetail)
                    .WithMany(p => p.OrderSpecialRequest)
                    .HasForeignKey(d => d.OrderDetailId)
                    .HasConstraintName("FK_OrderSpecialRequest_OrderDetails");

                entity.HasOne(d => d.SpecialRequest)
                    .WithMany(p => p.OrderSpecialRequest)
                    .HasForeignKey(d => d.SpecialRequestId)
                    .HasConstraintName("FK_OrderSpecialRequest_SpecialRequest");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

                entity.Property(e => e.PayDiscount).HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Payment1)
                    .HasMaxLength(20)
                    .HasColumnName("Payment");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.Property(e => e.RoomId)
                    .HasMaxLength(20)
                    .HasColumnName("RoomID");

                entity.Property(e => e.AdminId)
                    .HasMaxLength(20)
                    .HasColumnName("AdminID");

                entity.Property(e => e.HotelId).HasColumnName("HotelID");

                entity.Property(e => e.RoomClassId)
                    .HasMaxLength(20)
                    .HasColumnName("RoomClassID");

                entity.Property(e => e.RoomStatus).HasMaxLength(20);

                entity.HasOne(d => d.Admin)
                    .WithMany(p => p.Room)
                    .HasForeignKey(d => d.AdminId)
                    .HasConstraintName("FK_Room_RoomAdmin");

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.Room)
                    .HasForeignKey(d => d.HotelId)
                    .HasConstraintName("FK_Room_HotelIndustries");

                entity.HasOne(d => d.RoomClass)
                    .WithMany(p => p.Room)
                    .HasForeignKey(d => d.RoomClassId)
                    .HasConstraintName("FK_Room_RoomClass");
            });

            modelBuilder.Entity<RoomAdmin>(entity =>
            {
                entity.HasKey(e => e.AdminId);

                entity.Property(e => e.AdminId)
                    .HasMaxLength(20)
                    .HasColumnName("AdminID");

                entity.Property(e => e.AdminName).HasMaxLength(20);

                entity.Property(e => e.AdminPassword).HasMaxLength(20);
            });

            modelBuilder.Entity<RoomClass>(entity =>
            {
                entity.Property(e => e.RoomClassId)
                    .HasMaxLength(20)
                    .HasColumnName("RoomClassID");

                entity.Property(e => e.AddPrice).HasColumnType("decimal(10, 0)");

                entity.Property(e => e.HolidayPrice).HasColumnType("decimal(10, 0)");

                entity.Property(e => e.RoomClassName).HasMaxLength(20);

                entity.Property(e => e.WeekdayPrice).HasColumnType("decimal(10, 0)");
            });

            modelBuilder.Entity<RoomFacility>(entity =>
            {
                entity.HasKey(e => e.FacilityId)
                    .HasName("PK_Facilities");

                entity.Property(e => e.FacilityId).HasColumnName("FacilityID");

                entity.Property(e => e.FacilityImage).HasMaxLength(50);

                entity.Property(e => e.FacilityName).HasMaxLength(20);
            });

            modelBuilder.Entity<RoomImage>(entity =>
            {
                entity.HasKey(e => e.RoomImagesId);

                entity.Property(e => e.RoomImagesId).HasColumnName("RoomImagesID");

                entity.Property(e => e.RoomId)
                    .HasMaxLength(20)
                    .HasColumnName("RoomID");

                entity.Property(e => e.RoomImage1)
                    .HasMaxLength(50)
                    .HasColumnName("RoomImage");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.RoomImage)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK_RoomImage_Room");
            });

            modelBuilder.Entity<RoomMember>(entity =>
            {
                entity.HasKey(e => e.MemberId);

                entity.Property(e => e.MemberId)
                    .HasMaxLength(20)
                    .HasColumnName("MemberID");

                entity.Property(e => e.AdminId)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("AdminID");

                entity.Property(e => e.MemberBirthday).HasColumnType("date");

                entity.Property(e => e.MemberEmail).HasColumnName("MemberEMail");

                entity.Property(e => e.MemberGender).HasMaxLength(1);

                entity.Property(e => e.MemberIdentity).HasMaxLength(20);

                entity.Property(e => e.MemberLavel).HasMaxLength(50);

                entity.Property(e => e.MemberName).HasMaxLength(20);

                entity.Property(e => e.MemberOrderTotal).HasMaxLength(50);

                entity.Property(e => e.MemberPassword).HasMaxLength(20);

                entity.Property(e => e.MemberPhone).HasMaxLength(20);

                entity.Property(e => e.MemberPhoto).HasMaxLength(50);

                entity.HasOne(d => d.Admin)
                    .WithMany(p => p.RoomMember)
                    .HasForeignKey(d => d.AdminId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoomMember_RoomAdmin");
            });

            modelBuilder.Entity<SpecialRequest>(entity =>
            {
                entity.Property(e => e.SpecialRequestId).HasColumnName("SpecialRequestID");

                entity.Property(e => e.SpecialRequestName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}