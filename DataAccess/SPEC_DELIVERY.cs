//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class SPEC_DELIVERY
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SPEC_DELIVERY()
        {
            this.SPEC_OLIGO = new HashSet<SPEC_OLIGO>();
        }
    
        public int SPEC_DELIVERY_ID { get; set; }
        public System.DateTime CREATE_DTM { get; set; }
        public int SPEC_STATE_ID { get; set; }
        public Nullable<System.DateTime> GUAR_COMPLETE_DTM { get; set; }
        public int SPEC_ORDER_ID { get; set; }
        public Nullable<int> PARENT_SPEC_DELIVERY_ID { get; set; }
        public int PROCESS_PRIORITY { get; set; }
        public Nullable<int> REF_ID { get; set; }
        public Nullable<decimal> DECLARED_VALUE { get; set; }
        public int VERSION_NBR { get; set; }
        public System.DateTime VERSION_DTM { get; set; }
        public string PROJECT_NAME { get; set; }
        public Nullable<bool> REMAKE { get; set; }
        public string EXTERNAL_REF_ID { get; set; }
        public Nullable<int> DIRECTED_TYPE { get; set; }
        public Nullable<bool> DO_SHIP { get; set; }
        public Nullable<decimal> DECLARED_VALUE_NATURAL { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SPEC_OLIGO> SPEC_OLIGO { get; set; }
    }
}
