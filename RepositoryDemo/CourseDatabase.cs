using Newtonsoft.Json;
using RepositoryDemo.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RepositoryDemo
{
    public class CourseDatabase
    {
        private readonly string _coursesFilePath;
        private readonly string _teachersFilePath;

        public CourseDatabase()
        {
            var folderPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            _coursesFilePath = Path.Combine(folderPath, "courses.json");
            _teachersFilePath = Path.Combine(folderPath, "teachers.json");
            Load();
        }

        public List<Course> Courses { get; set; } = new List<Course>();

        public List<Teacher> Teachers { get; set; } = new List<Teacher>();

        private void Load()
        {
            if (File.Exists(_coursesFilePath))
            {
                using (var reader = new StreamReader(_coursesFilePath))
                {
                    var coursesJson = reader.ReadToEnd();
                    Courses = JsonConvert.DeserializeObject<List<Course>>(coursesJson);
                }
            }

            if (File.Exists(_teachersFilePath))
            {
                using (var reader = new StreamReader(_teachersFilePath))
                {
                    var teachersJson = reader.ReadToEnd();
                    Teachers = JsonConvert.DeserializeObject<List<Teacher>>(teachersJson);
                }
            }
        }

        public void SaveChanges()
        {
            var coursesJson = JsonConvert.SerializeObject(Courses);
            var coursesFile = File.Create(_coursesFilePath);
            using (var writer = new StreamWriter(coursesFile))
            {
                writer.Write(coursesJson);
            }

            var teachersJson = JsonConvert.SerializeObject(Teachers);
            var teachersFile = File.Create(_teachersFilePath);
            using (var writer = new StreamWriter(teachersFile))
            {
                writer.Write(teachersJson);
            }
        }
    }
}
