using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.DTOs.PayProcess
{
    public class PayInputDto
    {
        public string Email { get; set; }
        public int CourseId { get; set; }
    }
}
