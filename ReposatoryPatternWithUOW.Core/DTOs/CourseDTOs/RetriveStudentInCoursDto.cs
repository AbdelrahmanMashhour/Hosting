using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.DTOs.CourseDTOs
{
    public class RetriveStudentInCoursDto
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string Email { get; set; }
    }
}
