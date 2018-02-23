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
    public class SubCategoriesController : ApiController
    {
        private AbecedEntities db = new AbecedEntities();

        // GET: api/SubCategories
        public IQueryable<SubCategory> GetSubCategories()
        {
            return db.SubCategories;
        }

        // GET: api/SubCategories/5
        [ResponseType(typeof(SubCategory))]
        public IHttpActionResult GetSubCategory(int id)
        {
            SubCategory subCategory = db.SubCategories.Find(id);
            if (subCategory == null)
            {
                return NotFound();
            }

            return Ok(subCategory);
        }

        // PUT: api/SubCategories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSubCategory(int id, SubCategory subCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subCategory.SubCategoryId)
            {
                return BadRequest();
            }

            db.Entry(subCategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubCategoryExists(id))
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

        // POST: api/SubCategories
        //[ResponseType(typeof(SubCategory))]
        //public IHttpActionResult PostSubCategory(SubCategory subCategory)
        //{
        //    db.SubCategories.Add(subCategory);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = subCategory.SubCategoryId }, subCategory);
        //}

            public async Task<HttpResponseMessage> PostSubCategory()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string rootPath = HttpContext.Current.Server.MapPath("~/App_Files/Images");
            var provider = new MultipartFormDataStreamProvider(rootPath);
            int MainCatId = 0;
            string SubCatName = "";
            string SubCatImage = "";
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
                        SubCatImage = fileFullPath.ToString();

                    }


                }
                // Show all the key-value pairs.
                foreach (var key in provider.FormData.AllKeys)
                {
                    foreach (var val in provider.FormData.GetValues(key))
                    {
                        if(key == "mainCatID")
                        {
                            MainCatId = int.Parse(val);
                        }
                        else if (key == "name")
                        {
                            SubCatName = val.ToString();
                        }
                        else if (key == "description")
                        {
                            description = val.ToString();
                        }

                    }
                }
                db.SubCategories.Add(new SubCategory() { mainCatID = MainCatId, name = SubCatName, description = description, img = SubCatImage });
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }

        }


        // DELETE: api/SubCategories/5
        [ResponseType(typeof(SubCategory))]
        public IHttpActionResult DeleteSubCategory(int id)
        {
            SubCategory subCategory = db.SubCategories.Find(id);
            if (subCategory == null)
            {
                return NotFound();
            }

            db.SubCategories.Remove(subCategory);
            db.SaveChanges();

            return Ok(subCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SubCategoryExists(int id)
        {
            return db.SubCategories.Count(e => e.SubCategoryId == id) > 0;
        }
    }
}