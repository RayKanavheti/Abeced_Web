using Abeced.UI.Web.Helpers;
using Abeced.UI.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Abeced.UI.Web.Controllers
{
    public class FlashCardController : Controller
    {

        //int CourseId;
        public ActionResult SelectCardIndex(string CourseID, string CourseName)
        {

            ViewBag.CourseName = CourseName;
            //ViewData["CouseName"] = CourseName;
            TempData["CourseId"] = Int32.Parse(CourseID);
            TempData.Keep();
            //SelectCards(newCourseId);
            return View();
        }

        // GET: FlashCard
        public ActionResult SelectCards()
        {
            IEnumerable<FactModelRetrieve> factModelList = null;

            try
            {

                var response = DataAccess.WebClient.GetAsync("flashcards/cards/" + TempData["CourseId"]);
                response.Wait();
                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsAsync<List<FactModelRetrieve>>();
                    readTask.Wait();
                    factModelList = readTask.Result;

                }
                else
                {
                    factModelList = Enumerable.Empty<FactModelRetrieve>();
                    ModelState.AddModelError(string.Empty, "Server Error");

                }
                TempData["SelectedCards"] = factModelList;
                TempData.Keep();
                //ViewBag.Facts = factModelList;
                return Json(new {data =  factModelList},JsonRequestBehavior.AllowGet);


            }
            catch (Exception)
            {

                throw;
            }
        }


        //returning a string of Ids in Json format
        public JsonResult GetData(string myIds)
        {
           //do whatever you want with the an array of selected cards
           // var FactsIds = myIds.Split(',').Select(x => Int32.Parse(x)).ToArray();
            //return result;
            return Json(new { factIds = myIds }, JsonRequestBehavior.AllowGet);

        }


        public ActionResult FactsToMatch(string SelectedCards)
        {
            //factIds variable to be consistent with the parameter in the web apis
            string factIds = SelectedCards;

            IEnumerable<FactModelRetrieve> selectedFactsList = null;
            var response = DataAccess.WebClient.GetAsync("flashcards/selectedCards/"+ factIds);
            
            response.Wait();

            var result = response.Result;
           
            if (result.IsSuccessStatusCode)
            {

                var readTask = result.Content.ReadAsAsync<List<FactModelRetrieve>>();
                readTask.Wait();
                selectedFactsList = readTask.Result;
            }
            else
            {
                selectedFactsList = Enumerable.Empty<FactModelRetrieve>();
                ModelState.AddModelError(string.Empty, "Server Error");
            }
            
            
            return View(selectedFactsList);

        }


        public ActionResult Quizes(string SelectedCards, string CourseName)
        {
            TempData["SelectedFactsToMatchIds"] = SelectedCards;
            TempData.Keep();
            ViewData["CourseTitle"] = CourseName;

            return View();
        }


        public ActionResult QuizesJSONData()
        {

            string factIds = TempData["SelectedFactsToMatchIds"] as string;

            IEnumerable<FactModelRetrieve> selectedFactsList = null;
            var response = DataAccess.WebClient.GetAsync("flashcards/selectedCards/" + factIds);

            response.Wait();

            var result = response.Result;

            if (result.IsSuccessStatusCode)
            {

                var readTask = result.Content.ReadAsAsync<List<FactModelRetrieve>>();
                readTask.Wait();
                selectedFactsList = readTask.Result;
            }
            else
            {
                selectedFactsList = Enumerable.Empty<FactModelRetrieve>();
                ModelState.AddModelError(string.Empty, "Server Error");
            }


            return Json(new { factList = selectedFactsList }, JsonRequestBehavior.AllowGet);

        }



        public ActionResult flashcards(string SelectedCards)
        {

            TempData["SelectedFlashCardIds"] = SelectedCards;
            TempData.Keep();

            return View();
            //Since you have a string of selected cards under your disposal choose either if you want to return a view with a list of 
            // selected cards or Json formatted data like what i did for the quizes.. how to return a json data look at Facts To match
            // It all depends on the dynamics of the feature you want to develop

        }


        
    }
}