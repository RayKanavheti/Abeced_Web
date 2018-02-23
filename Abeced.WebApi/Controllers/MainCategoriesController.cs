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
using System.IO;

namespace Abeced.WebApi.Controllers
{
    public class MainCategoriesController : ApiController
    {
        private AbecedEntities db = new AbecedEntities();

        // GET: api/MainCategories
        public IQueryable<MainCategory> GetMainCategories()
        {
            return db.MainCategories;
        }

        // GET: api/MainCategories/5
        [ResponseType(typeof(MainCategory))]
        public IHttpActionResult GetMainCategory(int id)
        {
            MainCategory mainCategory = db.MainCategories.Find(id);
            if (mainCategory == null)
            {
                return NotFound();
            }

            return Ok(mainCategory);
        }

        // PUT: api/MainCategories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMainCategory(int id, MainCategory mainCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mainCategory.MainCategoryId)
            {
                return BadRequest();
            }

            db.Entry(mainCategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MainCategoryExists(id))
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

        // POST: api/MainCategories
        [ResponseType(typeof(MainCategory))]
        //public IHttpActionResult PostMainCategory(MainCategory mainCategory)
        //{
           

        //    db.MainCategories.Add(mainCategory);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = mainCategory.MainCategoryId }, mainCategory);
        //}

        public async Task<HttpResponseMessage> PostMainCategory()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string rootPath = HttpContext.Current.Server.MapPath("~/App_Files");
            var provider = new MultipartFormDataStreamProvider(rootPath);

            string mainCatName = "";
            string CatImage = "";
            string description = ""; 
            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);
                // This illustrates how to get the file names.
                foreach (MultipartFileData file in provider.FileData)
                {
                    string PostName = file.Headers.ContentDisposition.Name.Replace("\"", "");
                    string name = file.Headers.ContentDisposition.FileName.Replace("\"", "");
                    string newFileName = Guid.NewGuid() + Path.GetExtension(name);
                    File.Move(file.LocalFileName, Path.Combine(rootPath, newFileName));
                    string fileRelativePath = "~/App_Files/Images/" + newFileName;

                    Uri baseuri = new Uri(Request.RequestUri.AbsoluteUri.Replace(Request.RequestUri.PathAndQuery, string.Empty));
                    Uri fileFullPath = new Uri(baseuri, VirtualPathUtility.ToAbsolute(fileRelativePath));

                    if (PostName == "img")
                    {
                        CatImage = fileFullPath.ToString(); 

                    }


                }
                // Show all the key-value pairs.
                foreach (var key in provider.FormData.AllKeys)
                {
                    foreach (var val in provider.FormData.GetValues(key))
                    {
                        //Trace.WriteLine(string.Format("{0}: {1}", key, val));
                        if (key == "name")
                        {
                            mainCatName = val.ToString();
                        }
                        else if (key == "description")
                        {
                            description = val.ToString();
                        }
                      
                    }
                }
                db.MainCategories.Add(new MainCategory() { name = mainCatName, img = CatImage, description = description });
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception e)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }


        }


        // DELETE: api/MainCategories/5
        [ResponseType(typeof(MainCategory))]
        public IHttpActionResult DeleteMainCategory(int id)
        {
            MainCategory mainCategory = db.MainCategories.Find(id);
            if (mainCategory == null)
            {
                return NotFound();
            }

            db.MainCategories.Remove(mainCategory);
            db.SaveChanges();

            return Ok(mainCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MainCategoryExists(int id)
        {
            return db.MainCategories.Count(e => e.MainCategoryId == id) > 0;
        }
    }
}