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
            ViewBag.CourseTitle = CourseName;
            TempData["CourseId"] = Int32.Parse(CourseID);
            TempData.Keep();
            //SelectCards(newCourseId);
            return View();
        }

        // GET: FlashCard
        public ActionResult SelectCards()
        {
            IEnumerable<FactModel> factModelList = null;

            try
            {

                var response = DataAccess.WebClient.GetAsync("flashcards/cards/" + TempData["CourseId"]);
                response.Wait();
                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsAsync<List<FactModel>>();
                    readTask.Wait();
                    factModelList = readTask.Result;

                }
                else
                {
                    factModelList = Enumerable.Empty<FactModel>();
                    ModelState.AddModelError(string.Empty, "Server Error");

                }
                //ViewBag.Facts = factModelList;
                return Json(new {data =  factModelList},JsonRequestBehavior.AllowGet);


            }
            catch (Exception)
            {

                throw;
            }
        }



        public JsonResult GetData(string myIds)
        {
           //do whatever you want with the an array of selected cards
            var FactIds = myIds.Split(',').Select(x => Int32.Parse(x)).ToArray();
            //return result;
            return Json(new { factIds = myIds }, JsonRequestBehavior.AllowGet);

        }


        public ActionResult FactsToMatch(string SelectedCards)
        {
            var factIds = SelectedCards.Split(',').Select(x => Int32.Parse(x)).ToArray();


            IEnumerable<FactModel> selectedFactsList = null;
           // string[] factIds = SelectedCards.Split(',');
           

            var response = DataAccess.WebClient.GetAsync("flashcards/selectedCards/"+ factIds);
            
            response.Wait();

            var result = response.Result;
           
            if (result.IsSuccessStatusCode)
            {

                var readTask = result.Content.ReadAsAsync<List<FactModel>>();
                readTask.Wait();
                selectedFactsList = readTask.Result;
            }
            else
            {
                selectedFactsList = Enumerable.Empty<FactModel>();
                ModelState.AddModelError(string.Empty, "Server Error");
            }
            
            
            return View();

        }
    }
}