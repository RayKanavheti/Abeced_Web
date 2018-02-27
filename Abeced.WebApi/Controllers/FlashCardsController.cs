﻿using System;
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
        [ActionName("cards")]
        [Route("api/flashcards/selectedCards/{factIds}")]
        public HttpResponseMessage GetSelectedFacts(int[] factIds)
        {
            //string[] factids = factIds.Split(',');
         
            
            List<Fact> factList = new List<Fact>();


            for (int i = 0; i < factIds.Length; i++)
            {
                factList = db.Facts.Where(x => x.FactId == factIds[i]).ToList();
            }
                
            

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