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
    
    public partial class Rating
    {
        public int RatingId { get; set; }
        public string raterName { get; set; }
        public int raterID { get; set; }
        public string raterImg { get; set; }
        public int courseID { get; set; }
        public string contentName { get; set; }
        public int rate { get; set; }
        public string comment { get; set; }
        public Nullable<System.DateTime> date { get; set; }
    
        public virtual Course Course { get; set; }
    }
}
