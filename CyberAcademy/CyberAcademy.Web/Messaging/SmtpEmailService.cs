using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mail;

namespace CyberAcademy.Web.Messaging
{
    public class SmtpEmailService
    {
        public SmtpEmailService()
        {

        }

        public string SendEmail(EmailMessage msg, string attachmentPath = "" )
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", "smtp.gmail.com");
                mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", "465");
                mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", "2");
                mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
                mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "sterlingkioskapptest@gmail.com");
                mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "$terl1ngk1osk@pp");
                mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", "true");
                mail.From = "sterlingkioskapptest@gmail.com";//msg.From;//
                mail.To = msg.Recipient;
                mail.Subject = msg.Subject;//"Sterling Kiosk - Your one time password";
                mail.BodyFormat = MailFormat.Html;
                mail.Body = msg.Body;//CustomizeEmailBody(customerName, PIN);

                if (attachmentPath.Trim() != "")
                {
                    MailAttachment MyAttachment = new MailAttachment(attachmentPath);
                    mail.Attachments.Add(MyAttachment);
                    mail.Priority = MailPriority.High;
                }

                SmtpMail.SmtpServer = "smtp.gmail.com:465";
                SmtpMail.Send(mail);
                return "Mail sent successfully";
            }
            catch (Exception ex)
            {

                return "";
            }
        }
    }
}