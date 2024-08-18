using Microsoft.AspNetCore.JsonPatch;
using RepositoryPatternWithUOW.Core.Dto;
using RepositoryPatternWithUOW.Core.DTOs;
using RepositoryPatternWithUOW.Core.Models;
using RepositoryPatternWithUOW.Core.ReturnedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.Interfaces
{
    public interface IUserRepository
    {
        public Task<bool> SignUpAsync(SignUpDto obj);
        public Task<LoginResult> LoginAsync(LoginDto obj);
        public Task<bool> SendVerficationCode(string email, bool? IsForResetingPassword = false);
        public Task<string> ValidateCode(string email, string code, bool isForResetPassword = false);
        public Task<bool> ResetPassword(ResetPasswordDto resetPasswordDto,string identityToken);
        public Task<bool> UpdatePasswordAsync(string email, UpdatePasswordDto updatePasswordDto);

        public Task<bool> UpdateProfilePicture(UpdatePictureDto updatePictureDto);
        public Task<bool> UpdateInsensitiveData(JsonPatchDocument<User> patchDocument, string email);

        public Task<UpdatedTokens> UpdateTokens(UpdateTokensDto updateTokenDto);
        public Task<bool> SignOut(string refreshToken, string email);

        public Task<ProfileDataDto> GetProfileData(int studentId);
        public Task<IEnumerable<StudentDto>> GetAllStudents(bool bloked);
        public  Task<IEnumerable<int>> GetAllStudentsId();
        public Task<bool> AddToBlackList(int id);
        public Task<bool> RemoveFromBlackList(int id);
        public Task<int> GetUserId(string email);
        



    }
}
