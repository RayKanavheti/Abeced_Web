using Abeced_Data.Abeced.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abeced_Data.Repositery
{
  public class Repositery:IRepositery
    {

        AbecedEntity db = new AbecedEntity();


        public IList<Course> GetAllCourses()
        {
            IList<Course> courses = db.Courses.ToList();

            return courses; 
           
        }
        public Course GetCourse(int id)
        {
            return db.Courses.Find(id);

        }

        public void UpdateCourse(int id, Course course)
        {
            db.Entry(course).State = EntityState.Modified;
             db.SaveChanges();
           
        }

        public void SaveCourse( Course course)
        {
            db.Courses.Add(course);
            db.SaveChanges();
        }

        public void DeleteCourse(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
        }

        public List<Fact> GetAllFacts()
        {
            return db.Facts.ToList();
        }

        public Fact GetFact(int id)
        {
            return db.Facts.Find(id);
        }

        public void UpdateFact(int id, Fact fact)
        {
            db.Entry(fact).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void SaveFact(Fact fact)
        {
            db.Facts.Add(fact);
            db.SaveChanges();
        }

        public void DeleteFact(int id)
        {
            Fact fact = db.Facts.Find(id);
            db.Facts.Remove(fact);
            db.SaveChanges();
        }

        public List<Fact> getCourseFacts(int CourseId)
        {
            List<Fact> factList = new List<Fact>();
            factList = db.Facts.Where(x => x.courseID == CourseId).ToList();

            return factList;
        }
        public List<Fact> GetSelectedFacts(string factIds)
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

            return factList;

        }

        public List<MainCategory> GetAllMainCategories()
        {
             return db.MainCategories.ToList();
        }

        public MainCategory GetMainCategory(int id)
        {
            return db.MainCategories.Find(id);
        }

        public void UpdateMainCategory(int id, MainCategory mainCategory)
        {
            db.Entry(mainCategory).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void SaveMainCategory(MainCategory mainCategory)
        {
            db.MainCategories.Add(mainCategory);
            db.SaveChanges();
        }

        public void DeleteMainCategory(int id)
        {

            MainCategory mainCategory = db.MainCategories.Find(id);
            db.MainCategories.Remove(mainCategory);
            db.SaveChanges();

        }

        public List<Sharing> GetCardSharings()
        {
            return db.Sharings.ToList();
        }

        public void SaveSharing(Sharing sharing)
        {
            db.Sharings.Add(sharing);
            db.SaveChanges();
        }

        public List<SubCategory> GetAllSubCategories()
        {
            return db.SubCategories.ToList();
        }

        public SubCategory GetSubCategory(int id)
        {
           return db.SubCategories.Find(id);
            
        }

        public void UpdateSubCategory(int id, SubCategory subCategory)
        {
            db.Entry(subCategory).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void SaveSubCategory(SubCategory subCategory)
        {
            db.SubCategories.Add(subCategory);
            db.SaveChanges();
        }

        public void DeleteSubCategory(int id)
        {
         SubCategory subCategory =  db.SubCategories.Find(id);
            db.SubCategories.Remove(subCategory);
            db.SaveChanges();

        }


        public AspNetUser getPerson( string Email)
        {

            return db.AspNetUsers.Find(Email);

        }

        public List<AspNetUser> getAllAbecedUsers()
        {

            return db.AspNetUsers.ToList();

        }

        public List<Subject> getSubjects()
        {

            return db.Subjects.ToList();

        }

        public Subject getSubject(int id)
        {

            return db.Subjects.Find(id);


        }

        public void DeleteSubject(int id)
        {
            Subject subject = db.Subjects.Find(id);
            db.Subjects.Remove(subject);
            db.SaveChanges();
        }

        public void SaveSubject(Subject subject)
        {
            db.Subjects.Add(subject);
            db.SaveChanges();
        }

        public void UpdateSubject(int id, Subject subject)
        {
            db.Entry(subject).State = EntityState.Modified;
            db.SaveChanges();
        }

       


    }
}
