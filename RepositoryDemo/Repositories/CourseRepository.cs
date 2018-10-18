using RepositoryDemo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryDemo.Repositories
{
    public class CourseRepository
    {
        private readonly CourseDatabase _courseDatabase;

        public CourseRepository(CourseDatabase courseDatabase)
        {
            _courseDatabase = courseDatabase;
        }

        public List<Course> Get()
        {
            return _courseDatabase.Courses;
        }

        public Course Get(int id)
        {
            return _courseDatabase.Courses.Single(c => c.Id == id);
        }

        public void Add(Course Course)
        {
            if (_courseDatabase.Courses.Any())
            {
                Course.Id = _courseDatabase.Courses.Max(c => c.Id) + 1;
            }
            else
            {
                Course.Id = 1;
            }
            _courseDatabase.Courses.Add(Course);
            _courseDatabase.SaveChanges();
        }

        public void Update(Course Course)
        {
            var dbCourse = _courseDatabase.Courses.Single(c => c.Id == Course.Id);
            dbCourse.Name = Course.Name;

            var dbTeacher = _courseDatabase.Teachers.Single(t => t.Id == Course.Teacher.Id);
            dbCourse.Teacher = dbTeacher;

            _courseDatabase.SaveChanges();
        }

        public void Remove(int id)
        {
            var dbCourse = _courseDatabase.Courses.Single(c => c.Id == id);
            _courseDatabase.Courses.Remove(dbCourse);
            _courseDatabase.SaveChanges();
        }
    }
}
