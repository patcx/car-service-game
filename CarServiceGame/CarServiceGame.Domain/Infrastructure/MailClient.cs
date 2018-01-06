using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using Microsoft.Extensions.Options;

namespace CarServiceGame.Domain.Infrastructure
{
    public class MailCredentialsOptions
    {
        public string User { get; set; }
        public string Password { get; set; }
    }

    public interface IMailClient
    {
        void SendMail(string to, string body, string subject);
    }

    public class GmailMailClient : IMailClient
    {
        protected readonly string _user;
        protected readonly string _password;

        public GmailMailClient(IOptions<MailCredentialsOptions> credentials )
        {
            _user = credentials.Value.User;
            _password = credentials.Value.Password;
        }

        public void SendMail(string to, string body, string subject)
        {
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(_user, _password);

            MailMessage mm = new MailMessage(_user, to, subject, body);
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            client.Send(mm);
        }
    }
}
