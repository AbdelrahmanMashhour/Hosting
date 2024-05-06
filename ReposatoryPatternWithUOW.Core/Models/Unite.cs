using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.Models
{
    public class Unite
    {
        public int UnitId { get; set; }
        public int CourseId { get; set; }
        public string? VocablaryUrl { get; set; }
        public string? VocablaryPdfUrl { get; set; }
        public string? SkillUrl { get; set; }
        public string? SkillPdfUrl { get; set; }
        public string? TranslationUrl { get; set; }
        public string? TranslationPdfUrl { get; set; }
        public string? ExamUrl { get; set; }

        public string? StoryUrl { get; set; }
        public string? StoryPdfUrl { get; set; }
        [JsonIgnore]
        public virtual Course Course { get; set; }
        public virtual List<Assignment> Assignment { get; set; } = new();
    }
}
