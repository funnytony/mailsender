using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MailSender
{
    public class EmailSendServiceClass
    {        
        private readonly string _to;

        public EmailSendServiceClass(string to)
        {            
           _to = to;
        }

        public void SendEmail(string login, string pass, string title, string text)
        {
            try
            {
                using (var message = new MailMessage(EmailSenderConfig.from, _to, title, text))
                using (var client = new SmtpClient(EmailSenderConfig.smtpServer, EmailSenderConfig.smtpPort) { EnableSsl = true, Credentials = new NetworkCredential(login, pass) })
                {
                    client.Send(message);
                }
            }
            catch (Exception e)
            {                
                var mess = new WindowMessage(e.Message);
                mess.ShowDialog();
            }
        }
    }
}
