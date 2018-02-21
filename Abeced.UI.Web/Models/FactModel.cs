using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Abeced.UI.Web.Models
{
    public class FactModel
    {
        public int FactId { get; set; }

        public int courseID { get; set; }

        [Required]
        [Display(Name = "Fact Question")]
        public string question { get; set; }

        [Display(Name = "Question Image")]
        //public MultipartFormDataContent qImage { get; set; }
        public string qImage { get; set; }

        [Display(Name = "Question Audio")]
        //public string qAudio { get; set; }
        public string qAudio { get; set; }

        [Required]
        [Display(Name = "Fact Answer")]
        public string answer { get; set; }


        [Display(Name = "Answer Image")]
        //public MultipartFormDataContent aImage { get; set; }
       public string aImage { get; set; }

        [Display(Name = "Answer Audio")]
       // public string aAudio { get; set; }
        public string aAudio { get; set; }

        [Required]
        [Display(Name = "FactSheet")]
        public string factsheet { get; set; }

        [Display(Name = "FactSheet Audio")]
        //public string fsAudio { get; set; }
        public string fsAudio { get; set; }

        public int? userID { get; set; }
        public string points { get; set; }
        public string flashcardRespTime { get; set; }
        public string quizRespTime { get; set; }
       public IEnumerable<CourseModel> CourseList { get; set; }
        
    }
}