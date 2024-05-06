using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.DTOs.AssignmentDtos
{
    public class SolutionDto
    {
        public int StudentId { get; set; }
        public int AssignmentId { get; set; }

        public IFormFile SolutionFileUrl { get; set; }
    }
}
