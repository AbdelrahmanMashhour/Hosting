using RepositoryPatternWithUOW.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.DTOs.AssignmentDtos
{
    public class StudentSolutionsDto
    {
        public int AssignmentId { get; set; }
        public int StudentId { get; set; }
        public string? StudentName { get; set; }
        public string? AssignmentName { get; set; }
        public Stages? Stage { get; set; }
        public string? CourseName { get; set; }
        public int? FullMark { get; set; }
       
        public string? SolutionUrl { get; set; }
    }
}
