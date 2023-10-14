using SendGrid;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FIT5032_MyProject.Utils
{
    public class EmailSender
    {
        // Please use your SendGrid API KEY here.
        private const String API_KEY = "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";

        public void Send(String toEmail, String subject, String contents)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("jlou0008@student.monash.edu", "FIT5032 Notification");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var response = client.SendEmailAsync(msg);
        }

        public void SendWithAttachment(String toEmail, String subject, String contents, String attachmentPath)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("jlou0008@student.monash.edu", "FIT5032 Notification");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var bytes = File.ReadAllBytes(attachmentPath);
            var file = Convert.ToBase64String(bytes);
            msg.AddAttachment(Path.GetFileName(attachmentPath), file);

            var response = client.SendEmailAsync(msg);
        }
    }
}
