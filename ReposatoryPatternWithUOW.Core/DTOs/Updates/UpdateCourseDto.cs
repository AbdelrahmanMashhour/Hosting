using Microsoft.AspNetCore.Http;
using RepositoryPatternWithUOW.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.DTOs.Updates
{
    public class UpdateCourseDto
    {
        public string? CourseDescription { get; set; }
        public string? CourseName { get; set; }
        public int? CoursePrice { get; set; }
        public int? TotoalHoure { get; set; }
        public Stages? CoursStage { get; set; }
    }
}
