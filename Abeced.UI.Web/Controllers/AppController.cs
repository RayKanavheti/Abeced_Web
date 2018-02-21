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

        public ActionResult AddorEditCourse(int id=0)
        {

            CourseModel course = new CourseModel();
            IEnumerable<SubCategory> subCategoryList;
            try
            {
                HttpResponseMessage response = DataAccess.WebClient.GetAsync("SubCategories").Result;

                subCategoryList = response.Content.ReadAsAsync<IEnumerable<SubCategory>>().Result.ToList();
                course.subCategoryList = subCategoryList.ToList();
                //ViewBag.MainCatList = course.subCategoryList;
                return View(course);
            }
            catch (Exception ex)
            {

                Console.Write(ex.Message);
                return View();
            }
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

            SubCategory subCategory = new SubCategory();
            IEnumerable<MainCategoryList> mainCategoryList;
            try
            {
                HttpResponseMessage response = DataAccess.WebClient.GetAsync("MainCategories").Result;

                mainCategoryList = response.Content.ReadAsAsync<IEnumerable<MainCategoryList>>().Result.ToList();
                subCategory.MainCat = mainCategoryList.ToList();
                ViewBag.MainCatList = subCategory.MainCat;
                return View(subCategory);
            }
            catch (Exception ex)
            {
                
                Console.Write(ex.Message);
                return View();
            }

          
           
            

        }
        [HttpPost]
        public ActionResult AddorEditSubCategory(SubCategory subCategory)
        {
            return View();


        }


    }
}