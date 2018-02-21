using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Abeced.UI.Web.Models
{
    public class MainCategory
    {
        public int MainCategoryId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public HttpPostedFileBase img { get; set; }
    
    }
    public class MainCategoryList
    {
        public int MainCategoryId { get; set; }
        public string name { get; set; }
        public string img { get; set; }
        public string description { get; set; }
    }

  

    public class SubCategory
    {

        public int SubCategoryId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public HttpPostedFileBase img { get; set; }
        public int mainCatID { get; set; }

        [NotMapped]
        public List<MainCategoryList> MainCat { get; set; }
        
        public SubCategory()
        {
            MainCat = new List<MainCategoryList>();

        }
    }

    public class CourseModel
    {
        public int CourseId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public HttpPostedFileBase img { get; set; }
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
        public List<FactModel> facts { get; set; }

        public CourseModel()
        {
            facts = new List<FactModel>();

        }


    }
}