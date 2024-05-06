using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.ReturnedModels
{
    public class LoginResult
    {
        public string? ProfilePicture { get; set; }
        public string? Jwt { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? ExpirationOfJwt { get; set; }
        public DateTime? ExpirationOfRefreshToken { get; set; }
        public bool Success { get; init; }
        public bool EmailConfirmed { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public int? Id { get;set; }
        

    }
}
