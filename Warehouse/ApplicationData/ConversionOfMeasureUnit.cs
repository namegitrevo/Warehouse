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
    
    public partial class ConversionOfMeasureUnit
    {
        public int Id { get; set; }
        public int MaterialAssetId { get; set; }
        public int AtMeasureUnitId { get; set; }
        public decimal TransfarmationRatio { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
    }
}
