using System;
using System.Configuration;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;

namespace VASJ.BI.Helpers
{
    /// <summary>
    /// Class help to send mail
    /// </summary>
    public class MailHelper
    {
        public static bool SendEmailNotResponse(string subject, string body, string toAddress)
        {
            var host = ConfigurationManager.AppSettings["mailhost"];          // setup here the host
            var userName = ConfigurationManager.AppSettings["emailusername"]; // setup here the username
            var password = ConfigurationManager.AppSettings["emaipassword"];  // setup here the password
            var from = ConfigurationManager.AppSettings["noreply"];
            var to = toAddress;
            SmtpClient client = new SmtpClient
            {
                Port = 587,
                Host = host,
                EnableSsl = true,
                Timeout = 100000,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(userName, password)
            };
            MailMessage mm = new MailMessage(from, to, subject, body);
            mm.BodyEncoding = Encoding.UTF8;
            //mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            try
            {
                client.Send(mm);
            }
            catch (Exception ex)
            {
                MyLogger.Log.ErrorFormat("ErrorEmail: {0}", ex.ToString());
                return false;
            }
            return true;
        }

        /// <summary>
        /// Method to send email using hotmail.com
        /// </summary>
        /// <param name="subjectEncodeUtf8"></param>
        /// <param name="bodyEncodeUtf8"></param>
        /// <param name="toMailAddress"></param>
        public static void SendMail(string subjectEncodeUtf8, string bodyEncodeUtf8, string toMailAddress)
        {
            SendMail(subjectEncodeUtf8, bodyEncodeUtf8, toMailAddress, null, null);
        }//End of SendMail method

        /// <summary>
        /// Method to send email using hotmail.com
        /// </summary>
        /// <param name="subjectEncodeUtf8"></param>
        /// <param name="bodyEncodeUtf8"></param>
        /// <param name="toMailAddress"></param>
        /// <param name="ccMailAddress"></param>
        /// <param name="attachedFilePath"></param>
        public static void SendMail(string subjectEncodeUtf8, string bodyEncodeUtf8, string toMailAddress, string ccMailAddress, string attachedFilePath)
        {
            try
            {
                SmtpSection section = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

                string from = section.From;
                string host = section.Network.Host;
                int port = section.Network.Port;
                bool enableSsl = section.Network.EnableSsl;
                string user = section.Network.UserName;
                string password = section.Network.Password;

                if (section.DeliveryMethod == SmtpDeliveryMethod.Network)
                {
                    #region Validate for mailSettings

                    if (subjectEncodeUtf8 == null)
                        throw new ArgumentNullException("subjectEncodeUtf8");

                    if (subjectEncodeUtf8.Trim() == String.Empty)
                        throw new ArgumentException("Input cannot be empty.", "subjectEncodeUtf8");

                    if (bodyEncodeUtf8 == null)
                        throw new ArgumentNullException("bodyEncodeUtf8");

                    if (bodyEncodeUtf8.Trim() == String.Empty)
                        throw new ArgumentException("Input cannot be empty.", "bodyEncodeUtf8");

                    //toMailAddress
                    if (toMailAddress == null)
                        throw new ArgumentNullException("toMailAddress");

                    if (toMailAddress.Trim() == String.Empty)
                        throw new ArgumentException("Input cannot be empty.", "toMailAddress");

                    //from
                    if (from == null)
                        throw new ArgumentNullException("system.net.mailSettings.smtp.from");

                    if (from.Trim() == String.Empty)
                        throw new ArgumentException("Input cannot be empty.", "system.net.mailSettings.smtp.from");

                    //host
                    if (host == null)
                        throw new ArgumentNullException("system.net.mailSettings.smtp.network.host");

                    if (host == String.Empty)
                        throw new ArgumentException("Input cannot be empty.", "system.net.mailSettings.smtp.network.host");

                    //port
                    if (port < 0)
                        throw new ArgumentNullException("system.net.mailSettings.smtp.network.port");

                    //user
                    if (user == null)
                        throw new ArgumentNullException("system.net.mailSettings.smtp.network.user");

                    if (user == String.Empty)
                        throw new ArgumentException("Input cannot be empty.", "system.net.mailSettings.smtp.network.user");

                    //password
                    if (password == null)
                        throw new ArgumentNullException("system.net.mailSettings.smtp.network.password");

                    if (password == String.Empty)
                        throw new ArgumentException("Input cannot be empty.", "system.net.mailSettings.smtp.network.password");

                    #endregion Validate for mailSettings

                    //Mail Message
                    MailMessage mailMessage = new MailMessage();

                    //Mail Address
                    mailMessage.From = new MailAddress(from);

                    //Receiver email id
                    mailMessage.To.Add(toMailAddress);

                    if (!string.IsNullOrWhiteSpace(ccMailAddress))
                    {
                        //CC email
                        mailMessage.CC.Add(ccMailAddress);
                    }

                    //Bcc email
                    mailMessage.Bcc.Add(from);

                    //Subject of the email
                    mailMessage.Subject = subjectEncodeUtf8;

                    //Add encode for Subject
                    mailMessage.SubjectEncoding = Encoding.UTF8;

                    if (System.IO.File.Exists(attachedFilePath))
                    {
                        //Deciding for the attachment
                        mailMessage.Attachments.Add(new Attachment(attachedFilePath));
                    }

                    //Add the body of the email
                    mailMessage.Body = bodyEncodeUtf8;

                    //Add encode for Body
                    mailMessage.BodyEncoding = Encoding.UTF8;

                    //Add priority
                    mailMessage.Priority = MailPriority.Normal;

                    //Content of Body Is Html
                    mailMessage.IsBodyHtml = true;

                    //SMTP client
                    SmtpClient smtpClient = new SmtpClient();

                    //Host for Hotmail
                    smtpClient.Host = host;

                    //Port number for Hot mail
                    smtpClient.Port = port;

                    //Credentials to login in to hotmail account
                    smtpClient.Credentials = new NetworkCredential(user, password);

                    //Enabled SSL
                    smtpClient.EnableSsl = enableSsl;

                    //Send an email
                    smtpClient.Send(mailMessage);
                }
                else if (section.DeliveryMethod == SmtpDeliveryMethod.SpecifiedPickupDirectory)
                {
                    throw new NotImplementedException();
                }
                else if (section.DeliveryMethod == SmtpDeliveryMethod.PickupDirectoryFromIis)
                {
                    throw new NotImplementedException();
                }
            }//End of try block
            catch (Exception ex)
            {
                //Log error to log file
                MyLogger.Log.Error(ex.ToString());
            }//End of catch
        }//End of SendMail method
    }
}