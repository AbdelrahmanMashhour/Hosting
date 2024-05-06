using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.ReturnedModels
{
    public class StudentExams
    {
        public string ExamName { get; set; }
        public int? FullMarkExam { get; set;}
        public decimal?  StudentGrade { get; set; }
        public string CourseName { get; set;}
    }
}
