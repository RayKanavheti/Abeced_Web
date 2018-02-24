using Abeced.UI.Web.Helpers;
using Abeced.UI.Web.Models;
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


        public ActionResult SelectCardIndex()
        {
            
            return View();
        }

        // GET: FlashCard
        public ActionResult SelectCards(int CourseId, string CourseName)
        {
            IEnumerable<FactModel> factModelList = null;

            try
            {

                var response = DataAccess.WebClient.GetAsync("flashcards/cards/" + CourseId);
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
                ViewBag.Facts = factModelList;
                return View();


            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}