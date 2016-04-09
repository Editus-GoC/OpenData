using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Tripatlux.Models
{
    public class MailMessage
    {
        public string To { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }

        public MailMessage(string to)
        {
            To = to;
        }

        public MailMessage(string to, string subject, string body) : this(to)
        {
            Subject = subject;
            Body = body;
        }

        public bool Send()
        {
            try
            {
                var message = new System.Net.Mail.MailMessage();
                message.To.Add(To);
                message.Subject = Subject;
                message.From = new MailAddress("no-reply@tripatlux.lu", "TripatLux");
                message.Body = Body;
                message.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.*********.lu", 587)
                {
                    Credentials = new System.Net.NetworkCredential("******@*****.lu", "*******"),
                };
                smtp.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "MailMessage.Send()");
                return false;
            }
        }
    }
}