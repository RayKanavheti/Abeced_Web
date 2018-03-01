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
        public string img { get; set; }
        public int mainCatID { get; set; }
        public IEnumerable<MainCategoryList> MainCat { get; set; }


    }

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
        public string averageRating { get; set; }
        public int? userID { get; set; }


        public IEnumerable<SubCategory> subCategoryList { get; set; }

    }

    public class FactModelRetrieve{

        public int FactId { get; set; }
        public int courseID { get; set; }
        public string question { get; set; }
        public string qImage { get; set; }
        public string qAudio { get; set; }
        public string answer { get; set; }
        public string aImage { get; set; }
        public string aAudio { get; set; }
        public string factsheet { get; set; }
        public string fsAudio { get; set; }
        public int? userID { get; set; }
        public string points { get; set; }
        public string flashcardRespTime { get; set; }
        public string quizRespTime { get; set; }
        public IEnumerable<CourseModel> CourseList { get; set; }

    }
        
        
        
        
        

}