using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SportDay.Units
{
    public class EmailSender
    {
        // Please use your API KEY here.
        private const String API_KEY = "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";

        public void Send(String toEmail, String subject, String contents)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("ivory99421@gmail.com", "FIT5032 Example Email User");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg);
        }

        public void SendFeedback(String fromemail, String subject, String contents)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress(fromemail, "");
            var to = new EmailAddress("ivory99421@gmail.com", "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg);
        }

    }
}