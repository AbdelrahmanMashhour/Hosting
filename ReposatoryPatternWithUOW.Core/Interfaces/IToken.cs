

using RepositoryPatternWithUOW.Core.Models;
using RepositoryPatternWithUOW.Core.OptionsPatternClasses;

namespace RepositoryPatternWithUOW.Core.Interfaces
{
    public interface IToken
    {
        public static string GenerateToken( User? user = null,string?role=null, DateTime? expiresAt = null, TokenOptionsPattern? tokenOpts = null) { return string.Empty; }
        
    }

}
