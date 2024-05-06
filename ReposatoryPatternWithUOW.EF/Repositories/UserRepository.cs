using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using RepositoryPatternWithUOW.Core.Dto;
using RepositoryPatternWithUOW.Core.DTOs;
using RepositoryPatternWithUOW.Core.Enums;
using RepositoryPatternWithUOW.Core.Interfaces;
using RepositoryPatternWithUOW.Core.Models;
using RepositoryPatternWithUOW.Core.OptionsPatternClasses;
using RepositoryPatternWithUOW.Core.ReturnedModels;
using RepositoryPatternWithUOW.Core.Tokens;
using RepositoryPatternWithUOW.EfCore.MailService;
using RepositoryPatternWithUOW.EfCore.Mapper;

namespace RepositoryPatternWithUOW.EF.Reposatories
{
    public class UserRepository : IUserRepository
    {
        AppDbContext context;
        TokenOptionsPattern options;
        Mapper mapper;
        ISenderService senderService;

        public UserRepository(AppDbContext context, TokenOptionsPattern options, Mapper mapper, ISenderService senderService)
        {
            this.context = context;
            this.options = options;
            this.mapper = mapper;
            this.senderService = senderService;
        }

        public async Task<LoginResult> LoginAsync(LoginDto loginDto)

        {
            context.ChangeTracker.LazyLoadingEnabled = false;
            if (loginDto.Email is null || loginDto.Password is null)
                return new() { Success = false };

            var user = await context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == loginDto.Email);
            if (user is { Blocked:true} )
            {
                return new() { Success = false };
            }
            if (user is null || !BCrypt.Net.BCrypt.EnhancedVerify(loginDto.Password, user.Password))
            {
                return new() { Success = false };
            }
            if (!user.EmailConfirmed)
            {
                return new()
                {
                    Success = true,
                    EmailConfirmed = false,
                };

            }
            var expirationOfJWT = DateTime.Now.AddMinutes(15);
            var expirationOfRefreshToken = DateTime.Now.AddHours(6);
            var refreshToken = new RefreshToken()
            {

                UserId = user.UserId,
                CreatedAt = DateTime.Now,
                ExpiresAt = expirationOfRefreshToken,
                Token = TokensGenerator.GenerateToken()

            };
            context.Attach(user);
            user.RefreshTokens.Add(refreshToken);


            return new()
            {
                ProfilePicture = user.ProfilePictureUrl,
                Success = true,
                EmailConfirmed = true,
                Jwt = TokensGenerator.GenerateToken(user,  expirationOfJWT, options),
                ExpirationOfJwt = expirationOfJWT,
                ExpirationOfRefreshToken = expirationOfRefreshToken,
                RefreshToken = refreshToken.Token,
                FirstName=user.FirstName,
                LastName=user.LastName,
                Role=user.Role.ToString(),
                Email=user.Email,
                Id=user.UserId
                
            };


        }

        public async Task<bool> SignUpAsync(SignUpDto signupDto)
        {
            if (signupDto is null || await context.Users.AnyAsync(x => x.Email == signupDto.Email))
                return false;
            try
            {

                Student user = mapper.MapToStudent(signupDto);
                var studentPhones= new StudentPhones {StudentId=user.UserId, Phone=signupDto.Phone,DadPhone=signupDto.DadPhone };
                user.StudentPhones.Add(studentPhones);
                user.EmailConfirmed = false;
                var hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(user.Password);
                user.Password = hashedPassword;
                await context.AddAsync(user);
                return true;

            }
            catch
            {
                return false;
            }
        }
        public async Task<string?> SendVerficationCode(string email, bool? IsForResetingPassword = false)
        {
            context.ChangeTracker.LazyLoadingEnabled = false;
            var user = await context.Users.AsNoTracking().Include(x => x.EmailVerificationCode).Include(x=>x.IdentityTokenVerification).FirstOrDefaultAsync(x => x.Email == email);

            if (user is null)
                return null;
            if (user.EmailVerificationCode is not null && user.EmailVerificationCode.ExpiresAt < DateTime.Now.AddSeconds(-5))
                context.Remove(user.EmailVerificationCode);
            else if (user.EmailVerificationCode is not null)
            {
                return "sent";
            }
            var rand = new Random();
            var verificationNum = rand.Next(100000, int.MaxValue);
            user.EmailVerificationCode = new() { ExpiresAt = DateTime.Now.AddHours(1), Code = verificationNum.ToString() };
            context.Update(user);
            string body;
            string subject;
            if (IsForResetingPassword is null ||  IsForResetingPassword ==false)
            {
                body = $"Dear {email} ,\n you have signed up on our Educationl application, \nand we have sent to you a verification code which is : <b>{verificationNum}</b> ";
                subject = "Email Confirmation";
            }
            else
            {
                body = $"Dear {email} ,\nThere was a request to reset your password on our Educationl application! \nIf you did not make this request then please ignore this email,\nand we have sent to you a verification code which is : <b>{verificationNum}</b> ";
                subject = "Reset Password";
            }
            Task t1, t2;
            if(user.IdentityTokenVerification is not null)
            context.Remove(user.IdentityTokenVerification);
            var identityToken = TokensGenerator.GenerateToken();
            user.IdentityTokenVerification = new()
            {
                ExpirationDate = DateTime.Now.AddMinutes(25),
                UserId = user.UserId,
                Token = identityToken
            };
          
            t1=senderService.SendEmailAsync(email, subject, body);
            t2 = context.SaveChangesAsync();
            await Task.WhenAll(t1, t2);

            return identityToken;

        }
        public async Task<bool> ValidateCode(string email,string identityToken, string code, bool isForResetPassword = false)
        {
            context.ChangeTracker.LazyLoadingEnabled = false;
            
            var user = await context.Users.Include(x=>x.EmailVerificationCode).Include(x=>x.IdentityTokenVerification).FirstOrDefaultAsync(x => x.IdentityTokenVerification.Token == identityToken&&x.Email==email);

            if (user is null || user.EmailVerificationCode is null||user.IdentityTokenVerification.ExpirationDate<DateTime.Now)
                return false;

            if (user.EmailVerificationCode.Code != code)
            {

                if (user.EmailVerificationCode.ExpiresAt < DateTime.Now)
                {
                    context.Remove(user.EmailVerificationCode);
                }
                return false;
            }
            if (user.EmailVerificationCode.ExpiresAt < DateTime.Now)
            {
                context.Remove(user.EmailVerificationCode);
                return false;
            }
            if (!isForResetPassword)
            {
                user.EmailConfirmed = true;
                context.Remove(user.IdentityTokenVerification);
                context.Update(user);
            }
            context.Remove(user.EmailVerificationCode);
            return true;

        }
        public async Task<bool> ResetPassword(ResetPasswordDto resetPasswordDto, string identityToken)
        {
            context.ChangeTracker.LazyLoadingEnabled = false;
            var user = await context.Users.Include(x=>x.IdentityTokenVerification).AsNoTracking().FirstOrDefaultAsync(x => x.Email == resetPasswordDto.Email&&x.IdentityTokenVerification.Token==identityToken);
            if (user is null)
                return false;
            if (user.IdentityTokenVerification.ExpirationDate < DateTime.Now)
                return false;
            user.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(resetPasswordDto.NewPassword);
            context.Update(user);
            context.IdentityTokenVerifications.Remove(user.IdentityTokenVerification);
           
            return true;

        }
        public async Task<bool> UpdatePasswordAsync(string email, UpdatePasswordDto updatePasswordDto)
        {
            context.ChangeTracker.LazyLoadingEnabled = false;

            var user = await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
            if (user is null || !BCrypt.Net.BCrypt.EnhancedVerify(updatePasswordDto.OldPassword, user.Password))
            {
                return false;
            }
            user.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(updatePasswordDto.NewPassword);
            context.Users.Update(user);

            return true;
        }

        public async Task<bool> UpdateProfilePicture(UpdatePictureDto updatePictureDto)
        {
            context.ChangeTracker.LazyLoadingEnabled = false;
            var user = await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == updatePictureDto.Email);
            if (user is null)
                return false;
            var extintion = Path.GetExtension(updatePictureDto.NewPicture.FileName);
            var fileName = Guid.NewGuid().ToString() + extintion;
            string? filePath = Path.Combine("wwwroot/PersonalProfiles", fileName);
            user.ProfilePictureUrl = $"/PersonalProfiles/{fileName}";
           
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await updatePictureDto.NewPicture.CopyToAsync(stream);

            }

            context.Update(user);

            return true;

        }
        public async Task<bool> UpdateInsensitiveData(JsonPatchDocument<User> patchDocument, string email)
        {
            if (patchDocument.Operations.Exists(x=>x.path.ToLowerInvariant() is not "firstName" and not "lastName"))
            {
                return false;
            }

            var user = await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return false;
            try
            {
                
                patchDocument.ApplyTo(user);
                context.Update(user);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<UpdatedTokens> UpdateTokens(UpdateTokensDto updateTokensDto)
        {
            context.ChangeTracker.LazyLoadingEnabled = false;
            var tokenObj = await context.Set<RefreshToken>().Include(x => x.User).FirstOrDefaultAsync(x => x.Token == updateTokensDto.RefreshToken);
            if (tokenObj is null || updateTokensDto.Email != tokenObj.User.Email)
                throw new Exception("Invalid Token");
            else if (!tokenObj.IsActive)
            {
                context.Remove(tokenObj);
                return new UpdatedTokens() { Success = false };
            }

            var expirationOfJwt = DateTime.Now.AddMinutes(15);
            var expirationOfRefreshToken = DateTime.Now.AddHours(24);
            var jwt = TokensGenerator.GenerateToken(tokenObj.User, expirationOfJwt, options);
            var newRefreshToken = TokensGenerator.GenerateToken();
            
            context.Remove(tokenObj);

            await context.AddAsync(new RefreshToken
            {
                Token = newRefreshToken ,
                ExpiresAt = expirationOfRefreshToken,
                CreatedAt = DateTime.Now,
                UserId=tokenObj.UserId
            });

            return new()
            {
                ExpirationOfJwt = expirationOfJwt,
                ExpirationOfRefreshToken = expirationOfRefreshToken,
                Jwt = jwt,
                RefreshToken = newRefreshToken,
                Success = true
            };

        }

        public async Task<bool> SignOut(string refreshToken, string email)
        {

            int result=await context.Set<RefreshToken>().Where(r=>r.Token==refreshToken&&r.User.Email==email).ExecuteDeleteAsync();
            return result > 0;



            //var refToken = await context.Set<RefreshToken>().AsNoTracking().Include(x => x.User).FirstOrDefaultAsync(x => x.Token == refreshToken);

            //if (refToken is null || refToken.User.Email != email)
            //    return false;
            //context.Remove(refToken);
           // return true;

        }
        [Authorize(Roles = "Admine")]

        public async Task<IEnumerable<StudentDto>> GetAllStudents(bool bloked)
        {
            //var stud = await context.Students.ToListAsync();
            //var users = await context.Users.ToListAsync();
            var allStudents= await context.Students.Where(x=>x.Blocked==bloked&&x.EmailConfirmed).AsNoTracking().ToListAsync();

            var studentsDto= new List<StudentDto>();
            foreach (var student in allStudents)
            {
                studentsDto.Add(new StudentDto
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Email = student.Email,
                    Phone = student.StudentPhones.ToList()
                }) ;
            }
            return studentsDto;
        }
        [Authorize(Roles = "Admine")]

        public async Task<bool> AddToBlackList(int id)
        {

            var result = await context.Students.Where(x => x.UserId == id).ExecuteUpdateAsync(x => x.SetProperty(p=>p.Blocked,true));
            if (result>0)
            {
                var user = await context.Students.FirstOrDefaultAsync(x => x.UserId == id);
                var allref=user.RefreshTokens.Where(x=>x.IsActive).ToList();
                foreach (var item in allref)
                {
                    item.ExpiresAt = DateTime.Now;
                }
            }
            return result > 0;

            
        }
        [Authorize(Roles ="Admine")]
        public async Task<bool> RemoveFromBlackList(int id)
        {
            var result = await context.Users.Where(x => x.UserId == id).ExecuteUpdateAsync(x => x.SetProperty(p => p.Blocked, false));
            return result > 0;

        }

        public async Task<ProfileDataDto> GetProfileData(int studentId)
        {
            var profileData = new ProfileDataDto();
            var student= await context.Students.FirstOrDefaultAsync(s=>s.UserId == studentId&&s.EmailConfirmed==true);
            if (student == null)
                return profileData;
            profileData.FirstName= student.FirstName;
            profileData.LastName= student.LastName;
            profileData.Email = student.Email;
            profileData.PhoneNumber = student.StudentPhones.FirstOrDefault().Phone;
            profileData.DadPhone = student.StudentPhones.FirstOrDefault().DadPhone;
            profileData.City = student.City;
            profileData.Picture = student.ProfilePictureUrl;
            return profileData;

        }


    }
}