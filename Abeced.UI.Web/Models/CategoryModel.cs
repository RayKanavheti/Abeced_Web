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
        public string Answered { get; set; }
        public string Correct { get; set; }
        public double Score { get; set; }
        public IEnumerable<CourseModel> CourseList { get; set; }

    }
    public class StudySession
    {
        public int StudySessionId { get; set; }
        public System.DateTime dateStarted { get; set; }
        public DateTime? dateCompleted { get; set; }
        public bool isComplete { get; set; }
        public bool isRevision { get; set; }
        public string mainSessionID { get; set; }
        public string currentProgress { get; set; }
        public string currentScore { get; set; }
        public string lastUpdate { get; set; }
        public bool? isFavorite { get; set; }
        public string nextRevision { get; set; }
        public string previousRevision { get; set; }
        public string missedRevision { get; set; }
        public string numRevisionsDone { get; set; }
        public int userID { get; set; }
        public FactList factList { get; set; }

        public StudySession()
        {
            factList = new FactList();
        }

    }

    public class FactList
    {
        public int FactListId { get; set; }
        public string courseName { get; set; }
        public string courseDesc { get; set; }
        public string courseRating { get; set; }
        public string numFacts { get; set; }
        public string factsIDs { get; set; }
    }

    public class QuizSession
    {
        public int QuizSessionId { get; set; }
        public int UserId { get; set; }
        public string CourseName { get; set; }
        public string FactIds { get; set; }
        public double? AveragaScore { get; set; }
        public double? PointsEarned { get; set; }
        public int? Numfacts { get; set; }
        public int? NumRevised { get; set; }
    }

}