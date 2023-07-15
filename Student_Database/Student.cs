using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Database
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Image { get; set; }
        public int Age { get; set; }
        public double GPA { get; set; }

        public string Name
        {
            get { return (FirstName + " " + LastName); }
        }
    }
}
