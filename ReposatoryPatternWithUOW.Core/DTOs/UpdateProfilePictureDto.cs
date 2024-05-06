using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.DTOs
{
    public class UpdateProfilePictureDto
    {
        public IFormFile NewPicutre { get; set; }
    }
}
