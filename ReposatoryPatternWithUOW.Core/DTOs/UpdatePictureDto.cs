using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Abdelrahman
namespace RepositoryPatternWithUOW.Core.Dto
{
    public class UpdatePictureDto
    {
        [RegularExpression(@"\w+@\w+\.\w+(\.\w+)*", ErrorMessage = "Invalid Email")]
        [StringLength(100)]
        public required string Email { get; set; }
        public  required IFormFile NewPicture { get; set; }
    }
}
