
using RepositoryPatternWithUOW.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.Dto
{

    public class SignUpDto
    {
        [StringLength(100)]

        public string FirstName { get; set; }
        [StringLength(100)]

        public string LastName { get; set; }
        [RegularExpression(@"\w+@\w+\.\w+(\.\w+)*",ErrorMessage ="Invalid Email")]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(100,MinimumLength =8)]
        public string Password { get; set; }

        [Compare("Password",ErrorMessage ="Dosn't same")]
        public string ComparePassword { get; set; }
        public string City { get; set; }
        [StringLength(11,MinimumLength =11)]
        public string Phone { get; set; }
        [StringLength(11, MinimumLength = 11)]

        public string DadPhone { get; set; }
      
        //[JsonConverter(typeof(JsonStringEnumConverter))]
        //public UserRole Role { get; set; }

    }
}
