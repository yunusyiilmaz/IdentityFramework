namespace Project_Exam.Web.Api
{
    public interface IMailService
    {
        Task SendMailAsync(string toEmail,string subject, string content);
    }
}
