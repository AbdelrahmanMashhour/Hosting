using RepositoryPatternWithUOW.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Password { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string Role { get; set; } = UserRole.Student.ToString();
        public bool Blocked { get; set; } = false;
        public virtual EmailVerificationCode EmailVerificationCode { get; set; }
        public virtual List<RefreshToken> RefreshTokens { get; set; } = new();
        public virtual IdentityTokenVerification IdentityTokenVerification { get; set; }



    }
}
