using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryDemo.Data
{
    public class Course
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Teacher Teacher { get; set; }
    }
}
