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
    
    public partial class tblTaxon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblTaxon()
        {
            this.Species = "\"\"";
            this.Variety = "\"\"";
            this.SubSpecies = "\"\"";
            this.Forma = "\"\"";
            this.Cultivar = "\"\"";
            this.tblSeedListItem = new HashSet<tblSeedListItem>();
            this.tblCollectionItem = new HashSet<tblCollectionItem>();
        }
    
        public System.Guid Id { get; set; }
        public System.Guid tblGenusId { get; set; }
        public string Species { get; set; }
        public string Variety { get; set; }
        public string SubSpecies { get; set; }
        public string Forma { get; set; }
        public string Cultivar { get; set; }
    
        public virtual tblGenus tblGenus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblSeedListItem> tblSeedListItem { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblCollectionItem> tblCollectionItem { get; set; }
    }
}
