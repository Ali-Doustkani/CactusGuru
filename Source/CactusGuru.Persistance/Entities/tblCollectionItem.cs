//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class tblCollectionItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblCollectionItem()
        {
            this.tblSeedListItem = new HashSet<tblSeedListItem>();
            this.tblCollectionItemImage = new HashSet<tblCollectionItemImage>();
        }
    
        public System.Guid Id { get; set; }
        public System.Guid tblTaxonId { get; set; }
        public Nullable<System.Guid> tblCollectorId { get; set; }
        public Nullable<System.Guid> tblSupplierId { get; set; }
        public string Code { get; set; }
        public string SupplierCode { get; set; }
        public Nullable<int> Count { get; set; }
        public string FieldNumber { get; set; }
        public string Locality { get; set; }
        public Nullable<System.DateTime> IncomeDate { get; set; }
        public byte IncomeType { get; set; }
        public string Description { get; set; }
    
        public virtual tblCollector tblCollector { get; set; }
        public virtual tblSupplier tblSupplier { get; set; }
        public virtual tblTaxon tblTaxon { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblSeedListItem> tblSeedListItem { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblCollectionItemImage> tblCollectionItemImage { get; set; }
    }
}
