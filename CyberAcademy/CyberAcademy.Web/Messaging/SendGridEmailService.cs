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
        private readonly SendGridMessage _msg;
        private readonly SendGridClient client;

        public SendGridEmailService(String apiKey,
           String senderEmail = "info@cyberacademy.com",
           String senderName = "Cyberspace Academy")
        {
            _msg = new SendGridMessage();
            _msg.From = new EmailAddress(senderEmail, senderName);
            client = new SendGridClient(apiKey);
        }


        public async Task<string> SendMail(EmailMessage message, Boolean isHtml)
        {
            _msg.AddTo(message.Recipient);
            
            _msg.Subject = message.Subject;
            if (!isHtml)
            {
                _msg.PlainTextContent = message.Body;
            }
            else
            {
                _msg.HtmlContent = message.Body;
            }

            SendGrid.Response response = await client.SendEmailAsync(_msg);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return "Message Sent Successfully";

            else

                return "";
            
        }


        public void SendMail(EmailMessage message, Boolean isHtml, string fileName, Byte[] fileBytes)
        {
            _msg.AddTo(message.Recipient);
            _msg.Subject = message.Subject;

            if (!isHtml)
            {
                _msg.PlainTextContent = message.Body;
            }
            else
            {
                _msg.HtmlContent = message.Body;
            }
            if (!string.IsNullOrEmpty(fileName))
            {
                string fileContents = Convert.ToBase64String(fileBytes);
                _msg.AddAttachment(fileName, fileContents);
            }

            client.SendEmailAsync(_msg);
        }
    }
}