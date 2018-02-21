using Abeced.UI.Web.Helpers;
using Abeced.UI.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace Abeced.UI.Web.Controllers
{
    public class AppController : Controller
    {
        // GET: App
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddCard()
        {
            return View();

        }
        [HttpPost]
        public ActionResult AddCard(FactModel fact)
        {

            if (ModelState.IsValid)
            {
               // HttpResponseMessage response = DataAccess.WebClient.PostAsJsonAsync("Facts", fact).Result;
            }
            return View(fact);

        }

        public ActionResult AddorEditCard()
        {

            return View(new FactModel());
        }
        [HttpPost]
        public ActionResult AddorEditCard(FactModel fact)
        {
      
            if (Session["UserId"] != null) {
                fact.userID = Convert.ToInt32(Session["UserId"]);
                
            }
            else
            {
                fact.userID = null;

            }
            return View();

        }

        public ActionResult AddorEditCourse()
        {


            return View();
        }
        [HttpPost]
        public ActionResult AddorEditCourse(CourseModel course)
        {


            return View();
        }

        public ActionResult AddorEditMainCategory()
        {
            return View();

        }


        [HttpPost]
        public ActionResult AddorEditMainCategory(MainCategory mainCategory)
        {
           

            return View();

        }

        public ActionResult AddorEditSubCategory( int id=0)
        {
            
           
            IEnumerable<MainCategoryList> mainCategoryList;
           
            SubCategory subCategory = new SubCategory();
            HttpResponseMessage response = DataAccess.WebClient.GetAsync("MainCategories").Result;
          
            mainCategoryList = response.Content.ReadAsAsync<IEnumerable<MainCategoryList>>().Result;
            subCategory.MainCat = mainCategoryList.ToList();

            ViewBag.MainCatList = subCategory.MainCat;

            return View();
            

        }
        [HttpPost]
        public ActionResult AddorEditSubCategory(SubCategory subCategory)
        {
            return View();


        }


    }
}