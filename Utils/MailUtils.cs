using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.ComponentModel;

static public class MailUtils
{
    static public void SendMail (
        string host,
        int port,
        string from,
        string pass,
        string to,
        string subject,
        string body,
        string filePath = "",
        SendCompletedEventHandler callback = null
    ) {

        //  Validation mail parameters Process.
        bool invalidMail =
            string.IsNullOrWhiteSpace (host) ||
            string.IsNullOrWhiteSpace (from) ||
            string.IsNullOrWhiteSpace (pass) ||
            string.IsNullOrWhiteSpace (to);

        if (invalidMail)
        {
            var e = new Exception ("Invalid mail settings.");
            if (callback != null)
                callback.Invoke (
                    sender: null,
                    new AsyncCompletedEventArgs (e, true, null)
                );
            throw e;
        }


        // Create the mail message
        using (MailMessage mail = new MailMessage())
        {
            mail.IsBodyHtml = true;
            mail.From = new MailAddress(from);
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;


            // Add the attachment

            if (!string.IsNullOrEmpty(filePath))
            {
                Attachment data = new Attachment(
                    filePath,
                    MediaTypeNames.Application.Octet
                );

                // Add time stamp information for the file.
                ContentDisposition disposition = data.ContentDisposition;
                disposition.CreationDate = File.GetCreationTime(filePath);
                disposition.ModificationDate = File.GetLastWriteTime(filePath);
                disposition.ReadDate = File.GetLastAccessTime(filePath);

                mail.Attachments.Add(data);
            }

            // Send the email
            using ( var smtpClient = new SmtpClient (host))
            {

                smtpClient.Port = port;
                smtpClient.Credentials = new NetworkCredential (from, pass);
                smtpClient.EnableSsl = true;

                //  Add callback.
                if (callback != null)
                    smtpClient.SendCompleted += callback;

                try
                {
                    smtpClient.SendAsync (mail, mail);
                }
                catch (Exception e)
                {
                    //  Invoke Callback.
                    if (callback != null)
                        callback.Invoke (
                            sender: mail,
                            new AsyncCompletedEventArgs (e, true, mail)
                        );
                }
            }
        }
    }
}
