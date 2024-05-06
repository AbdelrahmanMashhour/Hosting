using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.DTOs
{
    public class ResetPasswordDto
    {
        [RegularExpression(@"\w+@\w+\.\w+(\.\w+)*", ErrorMessage = "Invalid Email")]
        [StringLength(100)]

        public string Email { get; set; }
        [StringLength(100, MinimumLength = 8)]
        public string NewPassword { get; set; }
    }
}
