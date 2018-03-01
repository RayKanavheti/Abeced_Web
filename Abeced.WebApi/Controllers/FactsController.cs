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
using System.Threading.Tasks;
using System.Web;
using System.Diagnostics;
using System.IO;
using Abeced.WebApi.Models;

namespace Abeced.WebApi.Controllers
{
    public class FactsController : ApiController
    {
        private AbecedEntities db = new AbecedEntities();
        // GET: api/Facts

        [Authorize]
        public IQueryable<Fact> GetFacts()
        {
            return db.Facts;
        }

        // GET: api/Facts/5
        [ResponseType(typeof(Fact))]
        public IHttpActionResult GetFact(int id)
        {
            Fact fact = db.Facts.Find(id);
            if (fact == null)
            {
                return NotFound();
            }

            return Ok(fact);
        }

        // PUT: api/Facts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFact(int id, Fact fact)
        {
            
        
            if (id != fact.FactId)
            {
                return BadRequest();
            }

            db.Entry(fact).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FactExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

    
        [ResponseType(typeof(Fact))]
        public async Task<HttpResponseMessage> PostFact()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string rootPath = HttpContext.Current.Server.MapPath("~/App_Files/Images");
            var provider = new MultipartFormDataStreamProvider(rootPath);

            

            string qImage = "";
            string qAudio = "";
            string aImage = "";
            string aAudio = "";
            string fsAudio = "";
            string question = "";
            string answer = "";
            string factsheet = "";
            string courseId = "";
            //string userId = "";
            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                // This illustrates how to get the file names.
                foreach (MultipartFileData file in provider.FileData)
                {
                    string PostName = file.Headers.ContentDisposition.Name.Replace("\"","");
                    string name = file.Headers.ContentDisposition.FileName.Replace("\"", "");
                    string type = Path.GetExtension(name);
                    string newFileName = Guid.NewGuid() + Path.GetExtension(name);
                    File.Move(file.LocalFileName, Path.Combine(rootPath, newFileName));
                    string fileRelativePath = "~/App_Files/Images/" + newFileName;
                    
                    
                    Uri baseuri = new Uri(Request.RequestUri.AbsoluteUri.Replace(Request.RequestUri.PathAndQuery, string.Empty));
                    Uri fileFullPath = new Uri(baseuri, VirtualPathUtility.ToAbsolute(fileRelativePath));
                   
                    if (PostName == "qImage")
                    {
                        qImage = fileFullPath.ToString();

                    }
                    else if (PostName == "qAudio")
                    {
                        qAudio = fileFullPath.ToString();

                    }
                    else if (PostName == "aImage")
                    {
                        aImage = fileFullPath.ToString();

                    }
                    else if (PostName == "aAudio")
                    {

                        aAudio = fileFullPath.ToString();
                    }
                    else if (PostName == "fsAudio")
                    {
                        fsAudio = fileFullPath.ToString();


                    }
                }

                // Show all the key-value pairs.
                foreach (var key in provider.FormData.AllKeys)
                {
                    foreach (var val in provider.FormData.GetValues(key))
                    {
                        //Trace.WriteLine(string.Format("{0}: {1}", key, val));
                        if (key == "courseID")
                        {
                            courseId = val;
                            int.Parse(courseId);
                        }else if (key== "question")
                        {
                            question = val.ToString();
                        }
                        else if(key == "answer")
                        {
                            answer = val.ToString(); 

                        }else if(key == "factsheet")
                        {
                            factsheet = val.ToString();
                        }

                
                    }
                }

                db.Facts.Add(new Fact() { courseID = int.Parse(courseId), question = question, qImage = qImage, qAudio = qAudio, answer = answer, aImage= aImage, aAudio = aAudio, factsheet = factsheet, fsAudio = fsAudio});
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        // DELETE: api/Facts/5
        [ResponseType(typeof(Fact))]
        public IHttpActionResult DeleteFact(int id)
        {
            Fact fact = db.Facts.Find(id);
            if (fact == null)
            {
                return NotFound();
            }

            db.Facts.Remove(fact);
            db.SaveChanges();

            return Ok(fact);
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