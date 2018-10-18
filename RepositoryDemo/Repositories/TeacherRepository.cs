using RepositoryDemo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryDemo.Repositories
{
    public class TeacherRepository
    {
        private readonly CourseDatabase _courseDatabase;

        public TeacherRepository(CourseDatabase courseDatabase)
        {
            _courseDatabase = courseDatabase;
        }

        public List<Teacher> Get()
        {
            return _courseDatabase.Teachers;
        }

        public Teacher Get(int id)
        {
            return _courseDatabase.Teachers.Single(t => t.Id == id);
        }

        public void Add(Teacher teacher)
        {
            if (_courseDatabase.Teachers.Any())
            {
                teacher.Id = _courseDatabase.Teachers.Max(t => t.Id) + 1;
            }
            else
            {
                teacher.Id = 1;
            }
            _courseDatabase.Teachers.Add(teacher);
            _courseDatabase.SaveChanges();
        }

        public void Edit(Teacher teacher)
        {
            var dbTeacher = _courseDatabase.Teachers.Single(t => t.Id == teacher.Id);
            dbTeacher.LastName = teacher.LastName;
            dbTeacher.FirstName = teacher.FirstName;
            _courseDatabase.SaveChanges();
        }

        public void Remove(int id)
        {
            var dbTeacher = _courseDatabase.Teachers.Single(t => t.Id == id);
            _courseDatabase.Teachers.Remove(dbTeacher);
            _courseDatabase.SaveChanges();
        }
    }
}
