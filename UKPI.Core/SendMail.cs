using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mail;
using System.Web;
using System.Configuration;

using log4net;

namespace UKPI.Core
{
    public class SendMail
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(SendMail));

        public string SmtpServer { get; set; }

        public int SmtpServerPort { get; set; }

        public int SendUsing { get; set; }

        public int SmtpAuthenticate { get; set; }

        public string SendUsername { get; set; }

        public string SendPassword { get; set; }

        public string From { get; set; }

        public string Subject { get; set; }

        public string ContentHeaderLine1 { get; set; }
        public string ContentHeaderLine2 { get; set; }
        public string ContentHeaderLine3 { get; set; }
        public string ContentDetailRejectList { get; set; }
        public string ContentDetailPendingList { get; set; }
        public string ContentDetailFooterLine1 { get; set; }
        public string ContentDetailFooterLine2 { get; set; }

        public bool Send(string from, string to, string cc, string bcc, string subject, string body, string attachment)
        {
            MailMessage mail = new MailMessage();
            string[] arrAtt;

            try
            {

                mail.Fields["http://schemas.microsoft.com/cdo/configuration/smtpserver"] = this.SmtpServer;
                mail.Fields["http://schemas.microsoft.com/cdo/configuration/smtpserverport"] = this.SmtpServerPort;
                mail.Fields["http://schemas.microsoft.com/cdo/configuration/sendusing"] = this.SendUsing;
                mail.Fields["http://schemas.microsoft.com/cdo/configuration/smtpauthenticate"] = this.SmtpAuthenticate;
                mail.Fields["http://schemas.microsoft.com/cdo/configuration/sendusername"] = this.SendUsername;
                mail.Fields["http://schemas.microsoft.com/cdo/configuration/sendpassword"] = this.SendPassword;

                mail.From = from;
                mail.To = to;
                mail.Cc = cc;
                mail.Bcc = bcc;
                mail.Subject = subject;
                mail.Body = body;
                mail.BodyFormat = MailFormat.Html;

                if (attachment != null && !attachment.Trim().Equals(String.Empty))
                {
                    arrAtt = attachment.Trim().Split(new char[] { ';' });

                    foreach (string att in arrAtt)
                        mail.Attachments.Add(new MailAttachment(att));
                }

                SmtpMail.Send(mail);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);

                return false;
            }

            return true;
        }

        public bool Send(string from, string to, string cc, string bcc, string subject, string body)
        {
            return this.Send(from, to, cc, bcc, subject, body, null);
        }

        public bool Send(string from, string to, string cc, string subject, string body)
        {
            return this.Send(from, to, cc, string.Empty, subject, body);
        }

        public bool Send(string to, string cc, string subject, string body)
        {
            return this.Send(this.From, to, cc, string.Empty, subject, body);
        }
    }
}
