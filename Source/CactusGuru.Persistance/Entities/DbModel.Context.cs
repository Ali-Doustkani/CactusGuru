﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CactusGuru.Persistance.Entities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CactusGuruEntities : DbContext
    {
        public CactusGuruEntities()
            : base("name=CactusGuruEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblCollector> tblCollector { get; set; }
        public virtual DbSet<tblGenus> tblGenus { get; set; }
        public virtual DbSet<tblSeedList> tblSeedList { get; set; }
        public virtual DbSet<tblSeedListItem> tblSeedListItem { get; set; }
        public virtual DbSet<tblSupplier> tblSupplier { get; set; }
        public virtual DbSet<tblTaxon> tblTaxon { get; set; }
        public virtual DbSet<tblCollectionItem> tblCollectionItem { get; set; }
        public virtual DbSet<tblCollectionItemFullImage> tblCollectionItemFullImage { get; set; }
        public virtual DbSet<tblCollectionItemImage> tblCollectionItemImage { get; set; }
    }
}
