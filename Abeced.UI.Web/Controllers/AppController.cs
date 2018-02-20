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

            //MultipartFormDataContent content = new MultipartFormDataContent();
            //byte[] Bytes = new byte[qImageUpload.InputStream.Length + 1];
            //qImageUpload.InputStream.Read(Bytes, 0 , Bytes.Length);
            //var fileContent = new ByteArrayContent(Bytes);
            //fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName =qImageUpload.FileName};

            //byte[] Bytes2 = new byte[aImageUpload.InputStream.Length + 1];
            //aImageUpload.InputStream.Read(Bytes2, 0, Bytes2.Length);
            //var fileContent2 = new ByteArrayContent(Bytes2);
            //fileContent2.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = aImageUpload.FileName };
            //content.Add(fileContent);
            //content.Add(fileContent2);
            //fact.aImage = content;
            //fact.aImage = content2;

            // HttpResponseMessage response = DataAccess.WebClient.PostAsJsonAsync("facts", fact).Result;

            // return RedirectToAction("Login", "account");
            //return Json(new { success = true, html = "", message = "success" }, JsonRequestBehavior.AllowGet);
        }

    }
}