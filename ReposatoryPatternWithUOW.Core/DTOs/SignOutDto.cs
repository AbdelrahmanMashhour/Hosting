using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.DTOs
{
    public class SignOutDto
    {
        public string Email { get; set; }
        public string refreshToken {  get; set; }
    }
}
