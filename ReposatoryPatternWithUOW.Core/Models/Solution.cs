using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RepositoryPatternWithUOW.Core.Models
{
    public class Solution
    {
        public int StudentId { get; set; }
        public int AssignmentId { get; set; }

        public string? SolutionFileUrl { get; set; }
        public decimal? StudentDegree { get; set; }
        public virtual Student Student { get; set; }
        public virtual Assignment Assignment { get; set; }



    }
}
