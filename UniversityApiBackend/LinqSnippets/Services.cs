using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.LinqSnippets
{
    public class Services
    {
        static public void SearchByEmail()
        {
            var users = new[]{ new User()
            {

                Name = "Yoel",
                LastName = "Ferrera",
                Email = "yoelferrera@gmail.com",
                Password = "12344"
            } };
            var userList = users.SelectMany(user => user.Email);
        }

        static public void IsAdult()
        {
            var students = new[] { new Student()
            {
                FirstName = "Juan",
                LastName = "Gonzalez",
                Dob = DateTime.Now
            }
            };

            var mayorDeEdad = students.Where(student => student.Dob>=DateTime.Now);
            }
        static public void StrudentAlLeastACouse()
        {
            var students = new[] { new Student()
            {
                FirstName = "Juan",
                LastName = "Gonzalez",
                Dob = DateTime.Now,
                Courses= new[] { new Course() {
                

                } }
            }
            };
        }



    }

  


 }

