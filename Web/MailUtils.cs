using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.ComponentModel;
using BricksBucket.Core;

namespace BricksBucket
{
    static public class MailUtils
    {

        /// <summary>
        /// Sends an e-mail to a single receiver.
        /// </summary>
        /// <param name="host">Host of the sender mail account.</param>
        /// <param name="port">Port of the sender mail account.</param>
        /// <param name="sender">Sender mail account.</param>
        /// <param name="pass">Password of the sender's mail.</param>
        /// <param name="receiver">Receiver of the mail.</param>
        /// <param name="subject">Subject of the mail.</param>
        /// <param name="body">Body of the mail.</param>
        /// <param name="file">File path of the attachment.</param>
        /// <param name="callback">Callback method.</param>
        static public void SendMail (
            string host,
            int port,
            string sender,
            string pass,
            string receiver,
            string subject,
            string body,
            string file = "",
            SendCompletedEventHandler callback = null
        ) {

            //  Validation mail parameters Process.

            //  Sender Validation.
            if (string.IsNullOrWhiteSpace (sender) || !sender.HasEmailFormat ())
            {
                ExceptionCallback ("Invalid e-mail sender.", callback);
                return;
            }

            //  Reciver Validation.
            if (string.IsNullOrWhiteSpace (receiver)|| !receiver.HasEmailFormat ())
            {
                ExceptionCallback ("Invalid e-mail receiver.", callback);
                return;
            }

            //  Validating Password
            if (string.IsNullOrWhiteSpace (pass))
            {
                ExceptionCallback ("Empty Password.", callback);
                return;
            }


            //  Validating host.
            if (string.IsNullOrWhiteSpace (host))
            {
                ExceptionCallback ("Invalid Host.", callback);
                return;
            }


            // Create the mail message
            using (MailMessage mail = new MailMessage ())
            {
                mail.IsBodyHtml = true;
                mail.From = new MailAddress (sender);
                mail.To.Add (receiver);
                mail.Subject = subject;
                mail.Body = body;


                // Add the attachment

                if (!string.IsNullOrEmpty (file))
                {
                    Attachment data = new Attachment (
                        file,
                        MediaTypeNames.Application.Octet
                    );

                    // Add time stamp information for the file.
                    ContentDisposition disposition = data.ContentDisposition;
                    disposition.CreationDate = File.GetCreationTime (file);
                    disposition.ModificationDate = File.GetLastWriteTime (file);
                    disposition.ReadDate = File.GetLastAccessTime (file);

                    mail.Attachments.Add (data);
                }

                // Send the email
                using (var smtpClient = new SmtpClient (host))
                {

                    smtpClient.Port = port;
                    smtpClient.Credentials = new NetworkCredential (
                        userName: sender,
                        password: pass
                    );
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

        /// <summary>
        /// Calls an exception or a callback.
        /// </summary>
        /// <param name="message">Message of the exception.</param>
        /// <param name="callback">Callback Method.</param>
        private static void
        ExceptionCallback (string message, SendCompletedEventHandler callback)
        {
            var e = new Exception (message);
            if (callback != null)
                callback.Invoke (
                    sender: null,
                    new AsyncCompletedEventArgs (e, true, null)
                );
            throw e;
        }
    }
}
