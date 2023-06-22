using SendGrid.Helpers.Mail;
using SendGrid;
using System.Net.Mail;

namespace Project_Exam.Web.Helper
{
    public static class PasswordReset
    {
        public static async void PasswordResetSendEmail(string link, string toEmail)
        {
            MailMessage mail = new MailMessage();

            SmtpClient smtpClient = new SmtpClient();
            mail.From = new MailAddress("dneme111111@gmail.com");
            mail.To.Add(toEmail);
            mail.Subject = $"www.Project.com::Şifre Yenileme";
            mail.Body = "<h2>Şifrenizi yenilemek için lütfen aşağıdaki linke tıklayınız.</h2><hr/>";
            mail.Body += $"<a href='{link}'>Şifre yenileme linki</a>";
            mail.IsBodyHtml = true;
            smtpClient.Port = 587;
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new System.Net.NetworkCredential("dneme111111@gmail.com", "klyarlvwmemguvny");

            smtpClient.Send(mail);
        }
    }
}
