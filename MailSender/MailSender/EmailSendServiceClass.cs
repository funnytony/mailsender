using MailSender.ViewModel;
using PasswordDll;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MailSender
{
    public class EmailSendServiceClass
    {        
        private readonly string _login;
        private readonly string _pass;
        private CancellationTokenSource _tokenSource = new CancellationTokenSource();

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
                //var messView = new WindowMessage();
                //var mess = new WindowMessageViewModel("Ошибка отправки сообщения", e.Message);
                //mess.ReqestClose += messView.Close;
                //messView.DataContext = mess;
                //messView.ShowDialog();
            }
        }

        private async Task SendEmailAsync(string mailto)
        {
            try
            {
                using (var message = new MailMessage(_login, mailto, EmailSenderConfig.title, EmailSenderConfig.message))
                using (var client = new SmtpClient(EmailSenderConfig.smtpServer, EmailSenderConfig.smtpPort) { EnableSsl = true, Credentials = new NetworkCredential(_login, Encrypter.Deencrypt(_pass)) })
                {
                    await client.SendMailAsync(message).ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                //var messView = new WindowMessage();
                //var mess = new WindowMessageViewModel("Ошибка отправки сообщения", e.Message);
                //mess.ReqestClose += messView.Close;
                //messView.DataContext = mess;
                //messView.ShowDialog();
            }
        }

        public void SendEmails(IEnumerable<Emails> mailsto, IProgress<double> progress)
        {
            
            int i = 0;
            if(_tokenSource.IsCancellationRequested)
            { _tokenSource = new CancellationTokenSource(); }
            mailsto.AsParallel()
                .WithCancellation(_tokenSource.Token)
                .WithDegreeOfParallelism(Environment.ProcessorCount)
                .ForAll(async mailto =>
                {
                    await SendEmailAsync(mailto.Value);
                    if(_tokenSource.IsCancellationRequested)
                    {
                        progress?.Report(0d);
                        MessageBox.Show("Отправка отменена");
                        return;
                    }
                    i++;
                    progress?.Report((i*100) / mailsto.Count());
                    });
            //progress?.Report(100d);
        }

        public async Task SendEmailsAsync(IEnumerable<Emails> mailsto, IProgress<double> progress)
        {

            int i = 0;
            foreach (var mailto in mailsto)
            {
                if (!_tokenSource.IsCancellationRequested)
                {                  
                    await SendEmailAsync(mailto.Value).ConfigureAwait(false);
                    i++;
                    progress?.Report((i * 100) / mailsto.Count()); 
                }
                else
                {
                    progress?.Report(0d);
                    MessageBox.Show("Отправка отменена");
                    return;
                }
            }
        }

        public void Cancel()
        {
            _tokenSource.Cancel();
        }
    }
}
