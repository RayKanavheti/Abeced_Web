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
using System.Web.Hosting;
using System.Net.Http.Headers;
using Abeced.WebApi.Models;

namespace Abeced.WebApi.Controllers
{
    public class CoursesController : ApiController
    {
        private AbecedEntities db = new AbecedEntities();

       

            public HttpResponseMessage GetCourses()
        {
           
            List<Course> list = new List<Course>();
            list = db.Courses.ToList();
            CourseModel courseModel = new CourseModel();
            List<CourseModel> NewList = list.Select(x => new CourseModel {
                CourseId = x.CourseId,
                name = x.name, description = x.description,
                img = x.img,
                duration = x.duration,
                dateCreated = x.dateCreated,
                numFacts = x.numFacts,
                points = x.points,
                creatorName = x.creatorName,
                isApproved = x.isApproved,
                approvalDate = x.approvalDate,
                subCatID = x.subCatID,
                subCatName = x.subCatName,
                mainCatName = x.mainCatName,
                userID = x.userID,
                averageRating = x.averageRating })
                .ToList();
            
            HttpResponseMessage response;
            response = Request.CreateResponse(HttpStatusCode.OK, NewList);
            return response;

        }

        

        // GET: api/Courses/5
        [ResponseType(typeof(Course))]
        public IHttpActionResult GetCourse(int id)
        {
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }

        // PUT: api/Courses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCourse(int id, Course course)
        {
           

            if (id != course.CourseId)
            {
                return BadRequest();
            }

            db.Entry(course).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
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

        // POST: api/Courses
        //[ResponseType(typeof(Course))]
        //public IHttpActionResult PostCourse(Course course)
        //{
           
        //    db.Courses.Add(course);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = course.CourseId }, course);
        //}
        public async Task<HttpResponseMessage> PostCourse()
        {

            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string rootPath = HttpContext.Current.Server.MapPath("~/App_Files/Images");
            var provider = new MultipartFormDataStreamProvider(rootPath);
            int SubCatId = 0;
            string CourseImage = "";
            string description = "";
            //string userId = "";
            string CourseTitle = "";

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
                        CourseImage = fileFullPath.ToString();

                    }


                }
                // Show all the key-value pairs.
                foreach (var key in provider.FormData.AllKeys)
                {
                    foreach (var val in provider.FormData.GetValues(key))
                    {
                        if (key == "subCatID")
                        {
                            SubCatId = int.Parse(val);
                        }
                        else if (key == "name")
                        {
                            CourseTitle = val.ToString();
                        }
                        else if (key == "description")
                        {
                            description = val.ToString();
                        }

                    }
                }
                db.Courses.Add(new Course() { subCatID = SubCatId, name = CourseTitle, userID =1, description = description, img = CourseImage, subCatName = "Unknown" });
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }

        }


        // DELETE: api/Courses/5
        [ResponseType(typeof(Course))]
        public IHttpActionResult DeleteCourse(int id)
        {
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return NotFound();
            }

            db.Courses.Remove(course);
            db.SaveChanges();

            return Ok(course);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CourseExists(int id)
        {
            return db.Courses.Count(e => e.CourseId == id) > 0;
        }
    }
}