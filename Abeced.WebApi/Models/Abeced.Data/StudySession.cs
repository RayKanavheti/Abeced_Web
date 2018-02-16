//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Abeced.WebApi.Models.Abeced.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class StudySession
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StudySession()
        {
            this.Revisions = new HashSet<Revision>();
        }
    
        public int StudySessionId { get; set; }
        public System.DateTime dateStarted { get; set; }
        public Nullable<System.DateTime> dateCompleted { get; set; }
        public bool isComplete { get; set; }
        public bool isRevision { get; set; }
        public string mainSessionID { get; set; }
        public string currentProgress { get; set; }
        public string currentScore { get; set; }
        public string lastUpdate { get; set; }
        public Nullable<bool> isFavorite { get; set; }
        public string nextRevision { get; set; }
        public string previousRevision { get; set; }
        public string missedRevision { get; set; }
        public string numRevisionsDone { get; set; }
        public int userID { get; set; }
    
        public virtual FactList FactList { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Revision> Revisions { get; set; }
        public virtual User User { get; set; }
    }
}
