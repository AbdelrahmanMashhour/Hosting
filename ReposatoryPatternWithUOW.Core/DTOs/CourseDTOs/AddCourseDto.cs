
using Microsoft.AspNetCore.Http;
using RepositoryPatternWithUOW.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.DTOs.CourseDTOs
{
    public class AddCourseDto
    {
        public string CourseName { get; set; }
        public int CoursePrice { get; set; }
        public int TotoalHoure { get; set; }
        public string CourseDescription { get; set; }
        public IFormFile? Profile { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Stages CoursStage { get; set; }
    }
}
