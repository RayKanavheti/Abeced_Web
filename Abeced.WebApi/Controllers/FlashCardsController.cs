using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Abeced.WebApi.Models.Abeced.Data;
using Abeced.WebApi.Models;

namespace Abeced.WebApi.Controllers
{
    public class FlashCardsController : ApiController
    {
        private AbecedEntities db = new AbecedEntities();

        // GET: api/FlashCards

        [HttpGet]
        [ActionName("cards")]
        [Route ("api/flashcards/cards/{courseId}")]
        public HttpResponseMessage GetCoursefacts(int CourseId)
        {
            
                List<Fact> factList = new List<Fact>();
                factList = db.Facts.Where(x => x.courseID == CourseId).ToList();

            List<FactsModel> factsModelList = factList.Select(x => new FactsModel
            {
                FactId = x.FactId,
                question = x.question,
                answer = x.answer,
                factsheet = x.factsheet
            }).ToList();

                HttpResponseMessage response;
                response = Request.CreateResponse(HttpStatusCode.OK, factsModelList);
                return response;
         

        }
        [HttpGet]
        [ActionName("selectedCards")]
        [Route("api/flashcards/selectedCards/{factIds}")]
        public HttpResponseMessage GetSelectedFacts(string factIds)
        {
          // changing the factIds string into an array of integers by using the seperator "," 
            var factids = factIds.Split(',').Select(x => Int32.Parse(x)).ToArray();


            List<Fact> factList = new List<Fact>();

            Fact fact;
            // looping through the factIds and adding them to the factList
            for (int i = 0; i < factids.Length; i++)
            {
                fact = db.Facts.Find(factids[i]);// finding a fact associated with the id
                factList.Add(fact);// if a fact with the id above is found, then it is added to the fact list
            }
              // mapping the factlist with the facts model  
            List<FactsModel> SelectedFactsList = factList.Select(x => new FactsModel {

                FactId = x.FactId,
                question = x.question,
                answer = x.answer,
                factsheet = x.factsheet,
                qAudio = x.qAudio,
                aAudio = x.aAudio,
                fsAudio = x.fsAudio,
                qImage = x.qImage,
                aImage = x.aImage,

            }).ToList();
            HttpResponseMessage response;
            response = Request.CreateResponse(HttpStatusCode.OK, SelectedFactsList);
            return response;
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FactExists(int id)
        {
            return db.Facts.Count(e => e.FactId == id) > 0;
        }
    }
}