using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.Models
{
    public class StudentCourse
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateOnly JoinedAt { get; set; }

        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
    }
}
