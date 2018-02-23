using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abeced.WebApi.Models
{
    public class CourseModel
    {
        public int CourseId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string img { get; set; }
        public string duration { get; set; }
        public DateTime? dateCreated { get; set; }
        public string numFacts { get; set; }
        public string points { get; set; }
        public string creatorName { get; set; }
        public bool? isApproved { get; set; }
        public DateTime? approvalDate { get; set; }
        public int subCatID { get; set; }
        public string subCatName { get; set; }
        public string mainCatName { get; set; }
        public int? userID { get; set; }
        public string averageRating { get; set; }
    }
}