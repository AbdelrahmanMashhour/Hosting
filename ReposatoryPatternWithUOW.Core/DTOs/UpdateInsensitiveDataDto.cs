using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.Dto
{
    //Abdelrahman
    public class UpdateInsensitiveDataDto
    {
        [StringLength(100)]
        public string FirstName { get; set; }


        [StringLength(100)]
        public string LastName { get; set; }
    }
}
