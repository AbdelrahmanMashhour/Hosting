using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.DTOs.CourseDTOs
{
    public class UnitDto
    {
        public int CourseId { get; set; }
        public string? Vocablary { get; set; }
        public IFormFile? VocablaryPdf { get; set; }
        public string? Skill { get; set; }
        public IFormFile? SkillPdf { get; set; }
        public string? Translation { get; set; }
        public IFormFile? TranslationPdf { get; set; }
        public string? Exam { get; set; }
        public string? Story { get; set; }
        public IFormFile? StoryPdf { get; set; }
    }
}
