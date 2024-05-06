namespace RepositoryPatternWithUOW.EfCore.MailService
{
    public interface ISenderService
    {
        public Task SendEmailAsync(string emailTo,string  subject, string body);
    }
}
