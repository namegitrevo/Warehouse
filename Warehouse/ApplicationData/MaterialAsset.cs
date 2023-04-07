//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Warehouse.ApplicationData
{
    using System;
    using System.Collections.Generic;
    
    public partial class MaterialAsset
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MaterialAsset()
        {
            this.DocumentItems = new HashSet<DocumentItem>();
            this.Supplies = new HashSet<Supply>();
            this.Reserves = new HashSet<Reserve>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string ItemNumber { get; set; }
        public int MeasureUnitId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DocumentItem> DocumentItems { get; set; }
        public virtual MeasureUnit MeasureUnit { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Supply> Supplies { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reserve> Reserves { get; set; }
    }
}
