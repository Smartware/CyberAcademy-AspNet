using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CyberAcademy.Web.Messaging
{
    public class SendGridEmailService
    {
        private readonly SendGridMessage msg;
        private readonly SendGridClient client;

        public SendGridEmailService(string apiKey,
            string senderEmail = "info@cyberacademy.com",
            string senderName = "Cyberspace Academy")
        {
            msg = new SendGridMessage();
            msg.From = new EmailAddress(senderEmail, senderName);
            client = new SendGridClient(apiKey);
        }

        public async Task<string> SendMail(EmailMessage message, Boolean isHtml)
        {
            msg.AddTo(message.Recipient);
            msg.Subject = message.Subject;
            if(isHtml)
            {
                msg.HtmlContent = message.Body;
            }
            else
            {
                msg.PlainTextContent = message.Body;
            }

            SendGrid.Response response = await client.SendEmailAsync(msg);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return "Message was sent successfully";
            else
                return string.Empty;

        }

    }
}