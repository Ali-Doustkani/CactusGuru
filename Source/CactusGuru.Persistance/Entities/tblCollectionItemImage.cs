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
    
    public partial class tblCollectionItemImage
    {
        public System.Guid Id { get; set; }
        public System.Guid tblCollectionItemId { get; set; }
        public string Description { get; set; }
        public System.DateTime DateAdded { get; set; }
        public byte[] Image { get; set; }
        public bool SharedOnInstagram { get; set; }
    
        public virtual tblCollectionItem tblCollectionItem { get; set; }
        public virtual tblCollectionItemFullImage tblCollectionItemFullImage { get; set; }
    }
}
