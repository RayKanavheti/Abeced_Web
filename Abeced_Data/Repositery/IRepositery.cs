using Abeced_Data.Abeced.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abeced_Data.Repositery
{
  public interface IRepositery
    {

      IList<Course> GetAllCourses();

      Course GetCourse(int id);

      void  UpdateCourse(int id, Course course);

      void  SaveCourse(Course course);

      void DeleteCourse(int id);

        List<Fact> GetAllFacts();

        Fact GetFact(int id);

        void UpdateFact(int id, Fact fact);

        void SaveFact(Fact fact);

        void DeleteFact(int id);


        List<Fact> getCourseFacts(int CourseId);

        List<Fact> GetSelectedFacts(string factIds);


        List<MainCategory> GetAllMainCategories();

        MainCategory GetMainCategory(int id);

        void UpdateMainCategory(int id, MainCategory mainCategory);

        void SaveMainCategory(MainCategory mainCategory);

        void DeleteMainCategory(int id);

        List<Sharing> GetCardSharings();

        void SaveSharing(Sharing sharing);


        List<SubCategory> GetAllSubCategories();

        SubCategory GetSubCategory(int id);

        void UpdateSubCategory(int id, SubCategory subCategory);

        void SaveSubCategory(SubCategory subCategory);

        void DeleteSubCategory(int id);

        AspNetUser getPerson(string Id);

        List<AspNetUser> getAllAbecedUsers();

        List<Subject> getSubjects();

        Subject getSubject(int id);

        void DeleteSubject(int id);

        void SaveSubject(Subject subject);

        void UpdateSubject(int id, Subject subject) ;


        

        

    }
}
