using SendGrid.Helpers.Mail;
using SendGrid;
using System.Net.Mail;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Project_Exam.Web.Api;

namespace Project_Exam.Web.Helper
{
    public static class EmailConfirmation
    {

        public static async void SendEmail(string link, string toEmail)
        {
            //MailMessage mail = new MailMessage();
            //SmtpClient smtpClient = new SmtpClient();
            //mail.From = new MailAddress("dneme111111@gmail.com");
            //mail.To.Add(toEmail);
            //mail.Subject = $"www.Project.com:Email doğrulama";
            //mail.Body = "<h2>Email adresinizi doğrulamak için lütfen aşağıdaki linke tıklayınız.</h2><hr/>";
            //mail.Body += $"<a href='{link}'>Email doğrulama linki</a>";
            //mail.IsBodyHtml = true;
            //smtpClient.Port = 587;
            //smtpClient.Host = "smtp.gmail.com";
            //smtpClient.EnableSsl = true;
            //smtpClient.Credentials = new System.Net.NetworkCredential("dneme111111@gmail.com", "klyarlvwmemguvny");
            //smtpClient.Send(mail);

            //*************************************************************************************************************/

            //var apiKey = "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";
            //var client = new SendGridClient(apiKey);
            //var from = new EmailAddress("dneme111111@gmail.com", "Example User");
            //var subject = "www.Project.com:Email doğrulama";
            //var to = new EmailAddress(toEmail);
            //var body = "<h2>Email adresinizi doğrulamak için lütfen aşağıdaki linke tıklayınız.</h2><hr/>";
            //body += $"<a href='{link}'>Email doğrulama linki</a>";
            //var msg = MailHelper.CreateSingleEmail(from, to, subject, body, body);
            //var response = await client.SendEmailAsync(msg);
            //var result = Task.Run(() => client.SendEmailAsync(msg)).GetAwaiter().GetResult();
            //var test = await response.Body.ReadAsStringAsync();
            //Console.WriteLine(response.StatusCode);
            //Console.WriteLine(response.Body.ReadAsStringAsync());
            /***************************************************************************************************************/

            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            MailMessage mail = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            mail.From = new MailAddress("dneme111111@gmail.com");
            mail.To.Add(toEmail);
            mail.Subject = $"www.Project.com:Email doğrulama";
            mail.Body = "<h2>Email adresinizi doğrulamak için lütfen aşağıdaki linke tıklayınız.</h2><hr/>";
            mail.Body += $"<a href='{link}'>Email doğrulama linki</a>";
            mail.IsBodyHtml = true;
            smtpClient.Port = 587;
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new System.Net.NetworkCredential("dneme111111@gmail.com", "klyarlvwmemguvny");
            smtpClient.Send(mail);

        }
    }
}
