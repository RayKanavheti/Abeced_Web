//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Abeced_Data.Abeced.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactList
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FactList()
        {
            this.FactInfoes = new HashSet<FactInfo>();
        }
    
        public int FactListId { get; set; }
        public string courseName { get; set; }
        public string courseDesc { get; set; }
        public string courseRating { get; set; }
        public string numFacts { get; set; }
        public string factsIDs { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FactInfo> FactInfoes { get; set; }
        public virtual StudySession StudySession { get; set; }
    }
}
