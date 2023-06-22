using SendGrid.Helpers.Mail;
using SendGrid;

namespace Project_Exam.Web.Api
{
    public class SendGridMailService : IMailService
    {

        private IConfiguration _configuration;

        public SendGridMailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMailAsync(string toEmail, string subject, string content)
        {
            var apiKey = _configuration["SendGridEmailSetings"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("dneme111111@gmail.com", "test");
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
