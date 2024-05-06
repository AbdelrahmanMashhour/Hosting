using RepositoryPatternWithUOW.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.ReturnedModels
{
    public class ProfileDataDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email    { get; set; }
        public string? Picture { get; set; }
        public string PhoneNumber { get; set; }
        public string DadPhone { get; set; }
        public string City { get; set; }
        public Stages Stages { get; set; }
    }
}
