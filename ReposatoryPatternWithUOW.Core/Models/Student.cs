using RepositoryPatternWithUOW.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.Models
{
    public class Student:User
    {

        public Stages Stages { get; set; }
        public string City { get; set; }
        public decimal? Grade { get; set; } = 0;
        public virtual List<StudentCourse> StudentCourses { get; set; }
        public virtual List<Course> Courses { get; set; } = new();
        public virtual List<Solution> Solution { get; set; }=new();
        [JsonIgnore]
        public virtual List<StudentPhones> StudentPhones { get; set; }=new();
        public virtual List<Assignment> Assignments { get; set; } = new();
        //public virtual List<StudentAssignment> StudentAssignments { get; set; } = new();


    }
}
