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
        public IFormFile? Vocablary { get; set; }
        public IFormFile? VocablaryPdf { get; set; }
        public IFormFile? Skill { get; set; }
        public IFormFile? SkillPdf { get; set; }
        public IFormFile? Translation { get; set; }
        public IFormFile? TranslationPdf { get; set; }
        public IFormFile? Exam { get; set; }
        public IFormFile? Story { get; set; }
        public IFormFile? StoryPdf { get; set; }
    }
}
