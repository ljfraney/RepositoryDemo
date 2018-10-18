using RepositoryDemo.Data;
using RepositoryDemo.Repositories;
using System;
using System.Linq;

namespace RepositoryDemo
{
    class Program
    {
        private static TeacherRepository _teacherRepository;
        private static CourseRepository _courseRepository;

        static void Main(string[] args)
        {
            var courseDatabase = new CourseDatabase();
            _teacherRepository = new TeacherRepository(courseDatabase);
            _courseRepository = new CourseRepository(courseDatabase);

            var menuOption = MainMenu();

            while (menuOption < 5)
            {
                switch (menuOption)
                {
                    case 1:
                        GetAllTeachers();
                        break;
                    case 2:
                        GetAllCourses();
                        break;
                    case 3:
                        AddTeacher();
                        break;
                    case 4:
                        AddCourse();
                        break;
                    case 5:
                        break;
                    default:
                        Console.WriteLine("You chose an invalid option.");
                        Clear();
                        break;
                }

                if (menuOption != 5)
                {
                    menuOption = MainMenu();
                }
            }
        }

        static int MainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("   +-------------------------+");
            Console.WriteLine("   |   Main Menu             |");
            Console.WriteLine("   |   1. Get all teachers   |");
            Console.WriteLine("   |   2. Get all courses    |");
            Console.WriteLine("   |   3. Add a teacher      |");
            Console.WriteLine("   |   4. Add a course       |");
            Console.WriteLine("   |   5. Exit               |");
            Console.WriteLine("   +-------------------------+");
            Console.WriteLine();
            Console.Write("Enter an option: ");

            var option = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(option))
            {
                return 5;
            }

            return int.Parse(option);
        }

        static void GetAllTeachers()
        {
            var teachers = _teacherRepository.Get();

            if (!teachers.Any())
            {
                Console.WriteLine("No teachers have been added.");
            }
            else
            {
                foreach (var teacher in teachers)
                {
                    Console.WriteLine($"{teacher.Id}: {teacher.FirstName} {teacher.LastName}");
                }
            }

            Clear();
        }

        static void GetAllCourses()
        {
            var courses = _courseRepository.Get();

            if (!courses.Any())
            {
                Console.WriteLine("No courses have been added.");
            }
            else
            {
                foreach (var course in courses)
                {
                    Console.WriteLine($"{course.Id}: {course.Name}, Teacher: {course.Teacher.FirstName} {course.Teacher.LastName}");
                }
            }

            Clear();
        }

        static void AddTeacher()
        {
            Console.Write("Last Name: ");
            var lastName = Console.ReadLine();
            Console.Write("First Name: ");
            var firstName = Console.ReadLine();

            var newTeacher = new Teacher
            {
                LastName = lastName,
                FirstName = firstName
            };

            _teacherRepository.Add(newTeacher);

            Console.WriteLine($"Teacher {firstName} {lastName} was added!");

            Clear();
        }

        static void AddCourse()
        {
            var teachers = _teacherRepository.Get();

            if (!teachers.Any())
            {
                Console.Write("No teachers have been added. You must add a teacher before you add a course.");
            }
            else
            {
                Console.Write("Course name: ");
                var courseName = Console.ReadLine();

                foreach (var teacher in teachers)
                {
                    Console.WriteLine($"   {teacher.Id}: {teacher.FirstName} {teacher.LastName}");
                }
                Console.Write("Enter the number of the teacher: ");
                var teacherId = int.Parse(Console.ReadLine());

                var newCourse = new Course
                {
                    Name = courseName,
                    Teacher = teachers.Single(t => t.Id == teacherId)
                };

                _courseRepository.Add(newCourse);
                Console.WriteLine($"Course {courseName} was added!");
            }

            Clear();
        }

        static void Clear()
        {
            Console.Write("Press <Enter> to continue...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
