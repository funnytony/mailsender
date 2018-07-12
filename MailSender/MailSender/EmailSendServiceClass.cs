using PasswordDll;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;


namespace MailSender
{
    public class EmailSendServiceClass
    {        
        private readonly string _login;
        private readonly string _pass;

        public EmailSendServiceClass(string login, string pass)
        {
            _login = login;
            _pass = pass;
        }

        public void SendEmail(string mailto)
        {
            try
            {
                using (var message = new MailMessage(_login, mailto, EmailSenderConfig.title, EmailSenderConfig.message))
                using (var client = new SmtpClient(EmailSenderConfig.smtpServer, EmailSenderConfig.smtpPort) { EnableSsl = true, Credentials = new NetworkCredential(_login, Encrypter.Deencrypt(_pass)) })
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

        public void SendEmails(IEnumerable<Emails> mailsto)
        {
            foreach(Emails mailto in mailsto)
            {
                SendEmail(mailto.Value);
            }
        }
    }
}
