using System;
using System.Diagnostics;
using Connectivity;
using MailKit.Net.Smtp;
using MimeKit;
using Payloads;

namespace QuoteBuilder.Droid
{
    public class EmailClient : IEmailClient
    {
        public void Send(EmailDependencies email)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(email.FromName, email.FromAddress));

                message.To.Add(new MailboxAddress(email.ToName, email.ToAddress));
                message.To.Add(new MailboxAddress(email.FromName, email.FromAddress));

                message.Subject = email.Title;
                message.Body = new TextPart("html") { Text = email.content };

                using (var client = new SmtpClient())
                {
                    client.Connect("mail.bizmonger.net", 587, false);

                    // Note: since we don't have an OAuth2 token, disable
                    // the XOAUTH2 authentication mechanism.
                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    // Note: only needed if the SMTP server requires authentication
                    client.Authenticate(Configuration.ServerEmail, Configuration.Password);

                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.GetBaseException().Message;
                Debug.WriteLine(errorMessage);
            }
        }
    }
}