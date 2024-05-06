using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.DTOs.AssignmentDtos
{
    public class AssignmentDto
    {
        public int UnitId { get; set; }
        public string Name { get; set; }
        public IFormFile  AssFile { get; set; }
        
        public int? FullMark { get; set; }
    }
}
