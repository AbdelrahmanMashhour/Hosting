using Newtonsoft.Json;
using RepositoryPatternWithUOW.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Text.Json.Serialization;

using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public int AdminId { get; set; }
        public string CourseDescription { get; set; }
        public string? ProfileUrl { get; set; }
        public string CourseName { get; set; }
        public int CoursePrice { get; set; }
        public int TotoalHoure { get; set; }
        public Stages CoursStage { get; set; }
        
        public virtual Admin Admin { get; set; }

        
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
        public virtual ICollection<Student> Students { get; set; }

        public virtual Unite Unite { get; set; } = null!;



    }
}
