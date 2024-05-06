using Microsoft.AspNetCore.Http;
using RepositoryPatternWithUOW.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.ReturnedModels
{
    public class RetriveCourses
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int CoursePrice { get; set; }
        public int TotoalHoure { get; set; }
        public string CourseDescription { get; set; }
        public string profileUrl { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Stages CoursStage { get; set; }
    }
}
