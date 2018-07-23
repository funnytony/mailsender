using MailSender.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MailSender
{
    public class SchedulerClass
    {
        DispatcherTimer _timer = new DispatcherTimer();
        EmailSendServiceClass _sender;
        DateTime _timeSend;
        IQueryable<Emails> _emails;
        IProgress<double> _progress;

        public TimeSpan GetSendTime(string sendTime)
        {
            TimeSpan tsSendTime = new TimeSpan();
            try
            {
                tsSendTime = TimeSpan.Parse(sendTime);
            }
            catch(Exception e)
            {
                var messView = new WindowMessage();
                var mess = new WindowMessageViewModel("Ошибка отправки сообщения", e.Message);
                mess.ReqestClose += messView.Close;
                messView.DataContext = mess;
                messView.ShowDialog();
            }

            return tsSendTime;
        }

        public void SendEmails(DateTime dateTime, EmailSendServiceClass emailSender, IQueryable<Emails> emails, IProgress<double> progress)
        {
            _sender = emailSender;
            _timeSend = dateTime;
            _emails = emails;
            _progress = progress;
            _timer.Tick += Timer_Tick;
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_timeSend.ToShortTimeString() == DateTime.Now.ToShortTimeString())
            {
                _sender.SendEmails(_emails, _progress);
                _timer.Stop();
                var messView = new WindowMessage();
                var mess = new WindowMessageViewModel("Сообщение планировщика", "Сообщения отправленны");
                mess.ReqestClose += messView.Close;
                messView.DataContext = mess;
                messView.ShowDialog();
            }
        }

    }
}
