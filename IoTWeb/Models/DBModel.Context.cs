﻿//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace IoTWeb.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Buliding_ManagementEntities : DbContext
    {
        public Buliding_ManagementEntities()
            : base("name=Buliding_ManagementEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BulletinBoard> BulletinBoard { get; set; }
        public virtual DbSet<EquipFix> EquipFix { get; set; }
        public virtual DbSet<Equipment> Equipment { get; set; }
        public virtual DbSet<History> History { get; set; }
        public virtual DbSet<HolderDataTable> HolderDataTable { get; set; }
        public virtual DbSet<HTDataTable> HTDataTable { get; set; }
        public virtual DbSet<HumiTemperSenser> HumiTemperSenser { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<PackageCompany> PackageCompany { get; set; }
        public virtual DbSet<ParkingManagement> ParkingManagement { get; set; }
        public virtual DbSet<ResidentDataTable> ResidentDataTable { get; set; }
        public virtual DbSet<SensorTable> SensorTable { get; set; }
        public virtual DbSet<ShiftTable> ShiftTable { get; set; }
        public virtual DbSet<SmokeSenser> SmokeSenser { get; set; }
        public virtual DbSet<SmokeSenserData> SmokeSenserData { get; set; }
        public virtual DbSet<UserAccount> UserAccount { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<ManagementFee> ManagementFee { get; set; }
        public virtual DbSet<NoAccountResident> NoAccountResident { get; set; }
        public virtual DbSet<NoBindingResidentAccount> NoBindingResidentAccount { get; set; }
        public virtual DbSet<ResidentASPUsers> ResidentASPUsers { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<StaffDataTable> StaffDataTable { get; set; }
        public virtual DbSet<UserHeadImg> UserHeadImg { get; set; }
        public virtual DbSet<StaffASPUsers> StaffASPUsers { get; set; }
        public virtual DbSet<PackageTable> PackageTable { get; set; }
        public virtual DbSet<ReturnPackage> ReturnPackage { get; set; }
        public virtual DbSet<PublicSpace> PublicSpace { get; set; }
    }
}
