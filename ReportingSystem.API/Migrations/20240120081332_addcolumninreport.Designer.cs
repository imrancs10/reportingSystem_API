﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReportingSystem.API.Data;

#nullable disable

namespace ReportingSystem.API.Migrations
{
    [DbContext(typeof(ReportingSystemContext))]
    [Migration("20240120081332_addcolumninreport")]
    partial class addcolumninreport
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ReportingSystem.API.Models.Organization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("DeletedBy")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LogoFileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Mobile")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PinCode")
                        .HasColumnType("int");

                    b.Property<bool>("ShowHeader")
                        .HasColumnType("bit");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("ReportingSystem.API.Models.PatientReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AorticKnuckle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AorticKnuckleCalcification")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AorticKnuckleUnfoldingofAorta")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BonyCage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BonyCageSide")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Bonylesion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BreastShadow")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BreastShadowAbnormal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BreastShadowSide")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BronchoVascularMarking")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BronchoVascularMarkingRegion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BronchoVascularMarkingSide")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CardiacShape")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CardiacShapeAbnormal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CardiacSize")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CostophrenicAngles")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CostophrenicAnglesSide")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("DeletedBy")
                        .HasColumnType("int");

                    b.Property<string>("Finding")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FractureRibNumber")
                        .HasColumnType("int");

                    b.Property<string>("FractureSide")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HemiDiaphragm")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HemiDiaphragmAbormal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HemiDiaphragmSide")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LymphNodes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pneumothorax")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PneumothoraxSide")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProminentHilumSpecify")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SoftTissue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SoftTissueAbnormal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SoftTissueSide")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UniqueId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<string>("XRayFileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("age")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("cavity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("cavityRegion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("cavitySide")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("date")
                        .HasColumnType("datetime2");

                    b.Property<string>("gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("hilum")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("hilumSide")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("masses")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("massesRegion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("massesSide")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("mediastinal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("mediastinalShiftSide")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("mobileNo")
                        .HasColumnType("bigint");

                    b.Property<string>("opacity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("opacityRegion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("opacitySide")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("refby")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("trachea")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tracheaShiftSide")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("uhid")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PatientReports");
                });

            modelBuilder.Entity("ReportingSystem.API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("DeletedBy")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailVerificationCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EmailVerificationCodeExpireOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsEmailVerified")
                        .HasColumnType("bit");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("bit");

                    b.Property<bool>("IsResetCodeInitiated")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordResetCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PasswordResetCodeExpireOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
