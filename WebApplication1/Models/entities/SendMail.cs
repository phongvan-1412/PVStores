using System.Net.Mail;
using System.Net;

namespace WebApplication1.Models.entities
{
    public class SendMail
    {
        public string MailFrom { get; set; }
        public List<string> MailTo { get; set; }
        public MailMessage MessageBody { get; set; }
        public SmtpClient Client { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string HostMail { get; set; }
        public int PortMail { get; set; }
        public bool SSL { get; set; }

        public SendMail()
        {
            HostMail = "smtp.gmail.com";
            MailFrom = "bichvanphamnguyen1412@gmail.com";
            MailTo = new List<string>();
            MessageBody = new MailMessage();
            SSL = true;
            PortMail = 587;
        }

        public string SendMailCredential()
        {
            try
            {
                SmtpPermission connectAccess = new SmtpPermission(SmtpAccess.Connect);

                Client = new SmtpClient(HostMail, PortMail)
                {
                    EnableSsl = SSL,
                    Credentials = new NetworkCredential(MailFrom, Password),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Timeout = 500000
                };

                MessageBody.Sender = new MailAddress(MailFrom);
                MessageBody.From = new MailAddress(MailFrom);

                foreach (string item in MailTo)
                {
                    MessageBody.To.Add(new MailAddress(item));
                }

                ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                System.Security.Cryptography.X509Certificates.X509Chain chain,
                System.Net.Security.SslPolicyErrors sslPolicyErrors)
                 {
                     return true;
                 };

                Client.Send(MessageBody);
                Client.SendAsyncCancel();
                MessageBody.Dispose();
                return "Success";
            }
            catch (SmtpException ex)
            {
                string err = ex.Message;
                return "Failed";
            }
        }
    }
}
