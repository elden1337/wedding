﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Wedding.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class boda_liveEntities : DbContext
    {
        public boda_liveEntities()
            : base("name=boda_liveEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<car> car { get; set; }
        public virtual DbSet<car_booking> car_booking { get; set; }
        public virtual DbSet<food> food { get; set; }
        public virtual DbSet<guest> guest { get; set; }
        public virtual DbSet<guest_booking> guest_booking { get; set; }
        public virtual DbSet<playlists> playlists { get; set; }
        public virtual DbSet<toastmessage> toastmessage { get; set; }
        public virtual DbSet<translation> translation { get; set; }
        public virtual DbSet<UserTokens> UserTokens { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<database_firewall_rules> database_firewall_rules { get; set; }
        public virtual DbSet<media> media { get; set; }
        public virtual DbSet<blog> blog { get; set; }
        public virtual DbSet<erikannamedia> erikannamedia { get; set; }
    }
}
