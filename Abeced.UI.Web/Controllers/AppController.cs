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
using Abeced_Data.Repositery;
using System.Net;
using System.Web.Http;


namespace Abeced.UI.Web.Controllers
{
    public class AppController : Controller
    {
        //// GET: App


        private static readonly string[] _validImageTypes = new string[] { "image/png", "image/jpg", "image/jpeg", "image/gif" };

        public ActionResult AddorEditCard(int i = 0)
        {

            FactModel fact = new FactModel();
            //IEnumerable<CourseModel> courseList;
            //try
            //{
            //    HttpResponseMessage response = DataAccess.WebClient.GetAsync("Courses").Result;

            //    courseList = response.Content.ReadAsAsync<IEnumerable<CourseModel>>().Result.ToList();
            //    fact.CourseList = courseList.ToList();
            //    return View(fact);
            //}
            //catch (Exception ex)
            //{

            //    Console.Write(ex.Message);
            //    return View();
            //}

            Repositery _repo = new Repositery();
                IList<Abeced_Data.Abeced.Data.Course> course = _repo.GetAllCourses();

            List<CourseModel> list = course.Select(x => new CourseModel {

                CourseId = x.CourseId,
                name = x.name

            }).ToList();
            fact.CourseList = list;


            return View(fact);

        }
       

        [HttpPost]
        public ActionResult AddorEditCard(FactModel fact)
        {
            try
            {

                Repositery _repo = new Repositery();
                if (fact.aImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(fact.aImage.FileName);
                    string extension = Path.GetExtension(fact.aImage.FileName);
                    filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                    string fileRelativePath = "~/App_Files/Images/" + filename;

                    Uri baseuri = new Uri(Request.Url.ToString());
                    Uri fileFullPath = new Uri(baseuri, VirtualPathUtility.ToAbsolute(fileRelativePath));
                    fileFullPath.ToString().Trim('{'); fileFullPath.ToString().Trim('}');
                    fact.aImagePath = fileFullPath.ToString();
                    fact.aImage.SaveAs(Path.Combine(Server.MapPath("~/App_Files/Images/"), filename));


                }
                else
                {
                    fact.aImagePath = null;
                }
                if (fact.qImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(fact.qImage.FileName);
                    string extension = Path.GetExtension(fact.qImage.FileName);
                    filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                    string fileRelativePath = "~/App_Files/Images/" + filename;

                    Uri baseuri = new Uri(Request.Url.ToString());
                    Uri fileFullPath = new Uri(baseuri, VirtualPathUtility.ToAbsolute(fileRelativePath));

                    fileFullPath.ToString().Trim('{'); fileFullPath.ToString().Trim('}');
                    fact.qImagePath = fileFullPath.ToString();
                    fact.qImage.SaveAs(Path.Combine(Server.MapPath("~/App_Files/Images/"), filename));


                }
                else
                {
                    fact.qImagePath = null;
                }

                if (fact.qAudio != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(fact.qAudio.FileName);
                    string extension = Path.GetExtension(fact.qAudio.FileName);
                    filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                    string fileRelativePath = "~/App_Files/audios/" + filename;

                    Uri baseuri = new Uri(Request.Url.ToString());
                    Uri fileFullPath = new Uri(baseuri, VirtualPathUtility.ToAbsolute(fileRelativePath));

                    fileFullPath.ToString().Trim('{'); fileFullPath.ToString().Trim('}');
                    fact.qAudioPath = fileFullPath.ToString();
                    fact.qAudio.SaveAs(Path.Combine(Server.MapPath("~/App_Files/audios/"), filename));


                }
                else
                {
                    fact.qAudioPath = null;
                }

                if (fact.aAudio != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(fact.aAudio.FileName);
                    string extension = Path.GetExtension(fact.aAudio.FileName);
                    filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                    string fileRelativePath = "~/App_Files/audios/" + filename;

                    Uri baseuri = new Uri(Request.Url.ToString());
                    Uri fileFullPath = new Uri(baseuri, VirtualPathUtility.ToAbsolute(fileRelativePath));

                    fileFullPath.ToString().Trim('{'); fileFullPath.ToString().Trim('}');
                    fact.aAudioPath = fileFullPath.ToString();
                    fact.aAudio.SaveAs(Path.Combine(Server.MapPath("~/App_Files/audios/"), filename));


                }
                else
                {
                    fact.aAudioPath = null;
                }

                if (fact.fsAudio != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(fact.fsAudio.FileName);
                    string extension = Path.GetExtension(fact.fsAudio.FileName);
                    filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                    string fileRelativePath = "~/App_Files/audios/" + filename;

                    Uri baseuri = new Uri(Request.Url.ToString());
                    Uri fileFullPath = new Uri(baseuri, VirtualPathUtility.ToAbsolute(fileRelativePath));

                    fileFullPath.ToString().Trim('{'); fileFullPath.ToString().Trim('}');
                    fact.fsAudioPath = fileFullPath.ToString();
                    fact.fsAudio.SaveAs(Path.Combine(Server.MapPath("~/App_Files/audios/"), filename));


                }
                else
                {
                    fact.fsAudioPath = null;
                }

                Abeced_Data.Abeced.Data.Fact myfact = new Abeced_Data.Abeced.Data.Fact
                {
                    courseID = fact.courseID,
                    question = fact.question,
                    qImage = fact.qImagePath,
                    qAudio = fact.qAudioPath,
                    answer = fact.answer,
                    aImage = fact.aImagePath,
                    aAudio = fact.aAudioPath,
                    factsheet = fact.factsheet,
                    fsAudio = fact.fsAudioPath,
                    userID = 1


                };
                _repo.SaveFact(myfact);

                return Json(new { returnmsg = "Success" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }



        }

        public ActionResult AddorEditCourse(int id=0)
        {
            try
            {
                CourseModel course = new CourseModel();

                Repositery _repo = new Repositery();
                IList<Abeced_Data.Abeced.Data.Subject> subject = _repo.getSubjects();

                List<SubjectModel> list = subject.Select(x => new SubjectModel
                {

                    SubjectId = x.SubjectId,
                    SubjectName = x.SubjectName

                }).ToList();
                course.SubjectList = list;


                return View(course);
            }
            catch (Exception)
            {

                throw;
            }
            




        }
        [HttpPost]
        public ActionResult AddorEditCourse(CourseModel course)
        {
            try
            {
                if (course.imgFile != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(course.imgFile.FileName);
                    string extension = Path.GetExtension(course.imgFile.FileName);
                    filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                    string fileRelativePath = "~/App_Files/Images/" + filename;

                    Uri baseuri = new Uri(Request.Url.ToString());
                    Uri fileFullPath = new Uri(baseuri, VirtualPathUtility.ToAbsolute(fileRelativePath));
                    fileFullPath.ToString().Trim('{'); fileFullPath.ToString().Trim('}');
                    course.img = fileFullPath.ToString();
                    course.imgFile.SaveAs(Path.Combine(Server.MapPath("~/App_Files/Images/"), filename));


                }


                Abeced_Data.Abeced.Data.Course cor = new Abeced_Data.Abeced.Data.Course
                {
                    name = course.name,
                    description = course.description,
                    img = course.img,
                    duration = course.duration,
                    dateCreated = DateTime.Now,
                    numFacts = course.numFacts,
                    SubjectIds = course.SubjectIds,
                    AbecedUserId = "ed08da0c-f25f-4da1-b2e3-ba39e5cd1246" // Session["UserId"].ToString()


                };
                Repositery _repo = new Repositery();
                _repo.SaveCourse(cor);

                return Json(new { returnmsg = "Success" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
           


           
        }

        public ActionResult AddorEditMainCategory()
        {
            return View();

        }


        [HttpPost]
        public ActionResult AddorEditMainCategory(MainCategory mainCategory)
        {
            try
            {
                if (mainCategory.imgFile != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(mainCategory.imgFile.FileName);
                    string extension = Path.GetExtension(mainCategory.imgFile.FileName);
                    filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                    string fileRelativePath = "~/App_Files/Images/" + filename;

                    Uri baseuri = new Uri(Request.Url.ToString());
                    Uri fileFullPath = new Uri(baseuri, VirtualPathUtility.ToAbsolute(fileRelativePath));
                    fileFullPath.ToString().Trim('{'); fileFullPath.ToString().Trim('}');
                    mainCategory.img = fileFullPath.ToString();
                    mainCategory.imgFile.SaveAs(Path.Combine(Server.MapPath("~/App_Files/Images/"), filename));


                }


                Abeced_Data.Abeced.Data.MainCategory MainCats = new Abeced_Data.Abeced.Data.MainCategory
                {
                    name = mainCategory.name,
                    description = mainCategory.description,
                    img = mainCategory.img,
               
                };

                Repositery _repo = new Repositery();
                _repo.SaveMainCategory(MainCats);
                return Json(new { returnmsg = "Success" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }

            

        }
       
        public ActionResult AddorEditSubCategory( int id=0)
        {

            //SubCategory subCategory = new SubCategory();
            //IEnumerable<MainCategoryList> mainCategoryList;
            //try
            //{
            //    HttpResponseMessage response = DataAccess.WebClient.GetAsync("MainCategories").Result;

            //    mainCategoryList = response.Content.ReadAsAsync<IEnumerable<MainCategoryList>>().Result.ToList();
            //    subCategory.MainCat = mainCategoryList.ToList();
            //    ViewBag.MainCatList = subCategory.MainCat;
            //    return View(subCategory);
            //}
            //catch (Exception ex)
            //{

            //    Console.Write(ex.Message);
            //    return View();
            //}
            try
            {
                SubCategory Subcats = new SubCategory();

                Repositery _repo = new Repositery();
                IList<Abeced_Data.Abeced.Data.MainCategory> mainCats = _repo.GetAllMainCategories();

                List<MainCategoryList> list = mainCats.Select(x => new MainCategoryList
                {

                    MainCategoryId = x.MainCategoryId,
                    name = x.name

                }).ToList();

                Subcats.MainCat = list;


                return View(Subcats);
            }
            catch (Exception)
            {

                throw;
            }



        }
        [HttpPost]
        public ActionResult AddorEditSubCategory(SubCategory subCategory)
        {


            try
            {
                if (subCategory.ImageFile != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(subCategory.ImageFile.FileName);
                    string extension = Path.GetExtension(subCategory.ImageFile.FileName);
                    filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                    string fileRelativePath = "~/App_Files/Images/" + filename;

                    Uri baseuri = new Uri(Request.Url.ToString());
                    Uri fileFullPath = new Uri(baseuri, VirtualPathUtility.ToAbsolute(fileRelativePath));
                    fileFullPath.ToString().Trim('{'); fileFullPath.ToString().Trim('}');
                    subCategory.img = fileFullPath.ToString();
                    subCategory.ImageFile.SaveAs(Path.Combine(Server.MapPath("~/App_Files/Images/"), filename));


                }


                Abeced_Data.Abeced.Data.SubCategory subCats = new Abeced_Data.Abeced.Data.SubCategory
                {
                    name = subCategory.name,
                    description = subCategory.description,
                    img = subCategory.img,
                    mainCatID = subCategory.mainCatID
                    

                };
                Repositery _repo = new Repositery();
                _repo.SaveSubCategory(subCats);
                ViewBag.SubcategorySuccess = "Sub Cat Posted successfully";
                return Json(new { returnmsg = "Success" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }



            
        }
   
        public ActionResult GetAllCourses()
        {
            
            try
            {
                IRepositery _repo = new Repositery();
                IList<Abeced_Data.Abeced.Data.Course> AllCourses = _repo.GetAllCourses();
                List<CourseModel> CourseList = AllCourses.Select(x => new CourseModel
                {
                    CourseId = x.CourseId,
                    name = x.name,
                    description = x.description,
                    img = x.img,
                    duration = x.duration,
                    dateCreated = x.dateCreated,
                    numFacts = x.numFacts,
                    points = x.points,
                    creatorName = x.creatorName,
                    subCatID = x.subCatID,
                    subCatName = x.subCatName,
                    mainCatName = x.mainCatName,
                    userID = x.userID,
                    averageRating = x.averageRating

                }).ToList();


                return View(CourseList);

            }
            catch (Exception)
            {

                throw;
            }
          
        }


    }
}