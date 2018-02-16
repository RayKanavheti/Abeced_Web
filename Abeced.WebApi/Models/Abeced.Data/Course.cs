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
    
    public partial class Course
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Course()
        {
            this.Facts = new HashSet<Fact>();
            this.Ratings = new HashSet<Rating>();
        }
    
        public int CourseId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string img { get; set; }
        public string duration { get; set; }
        public Nullable<System.DateTime> dateCreated { get; set; }
        public string numFacts { get; set; }
        public string points { get; set; }
        public string creatorName { get; set; }
        public Nullable<bool> isApproved { get; set; }
        public Nullable<System.DateTime> approvalDate { get; set; }
        public int subCatID { get; set; }
        public string subCatName { get; set; }
        public string mainCatName { get; set; }
        public Nullable<int> userID { get; set; }
        public string averageRating { get; set; }
    
        public virtual SubCategory SubCategory { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Fact> Facts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
