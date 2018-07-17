using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MailSender.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace MailSender.ViewModel
{
    public class WpfMailSenderViewModel:ViewModelBase
    {
        private IDataAccessService _dataAccessService;
        private ObservableCollection<Emails> _emails = new ObservableCollection<Emails>();
        private Emails _currentEmail = new Emails();
        private int _currentTabIndex;
        private string _searchText;
        private KeyValuePair<string, object> _smptServer;
        private KeyValuePair<string, object> _sender;
        private string _message;
        private readonly ObservableCollection<KeyValuePair<string, object>> _sendersInfo = new ObservableCollection<KeyValuePair<string, object>>(Senders.SendersDictionary);
        

        public ObservableCollection<Emails> Emails { get => _emails; set => Set(ref _emails, value); }

        public Emails CurrentEmail { get => _currentEmail; set => Set(ref _currentEmail, value); }

        public int CurrentTabIndex { get => _currentTabIndex; set => Set(ref _currentTabIndex, value); }

        public string SearchText { get => _searchText; set => Set(ref _searchText, value); }

        public IEnumerable<KeyValuePair<string, object>> SendersInfo => _sendersInfo;

        public KeyValuePair<string, object> SmptServer
        {
            get => _smptServer;
            set
            {
                Set(ref _smptServer, value);
                EmailSenderConfig.smtpServer = value.Key;
                EmailSenderConfig.smtpPort = (int)value.Value;
            }
        }

        public string Message { get => _message; set => Set(ref _message, value); }

        public KeyValuePair<string, object> Sender { get => _sender; set => Set(ref _sender, value); }

        public RelayCommand ReadAllEmailsCommand { get; }

        public RelayCommand<Emails> SaveEmailCommand { get; }

        public RelayCommand FindeEmailsCommand { get; }

        public RelayCommand<KeyValuePair<string, object>> DeleteSenderCommand { get;  }

        public RelayCommand<object> MoveTabForwardCommand { get; }

        public RelayCommand MoveTabBackdCommand { get; }

        public RelayCommand ScheduleSwitchCommand { get;  }

        public RelayCommand SendEmailCommand { get; }
        

        public WpfMailSenderViewModel(IDataAccessService dataAccessService)
        {
            _dataAccessService = dataAccessService;
            ReadAllEmailsCommand = new RelayCommand(GetEmails);
            SaveEmailCommand = new RelayCommand<Emails>(SaveEmail);
            DeleteSenderCommand = new RelayCommand<KeyValuePair<string, object>>(DeleteSender);
            MoveTabForwardCommand = new RelayCommand<object>(MoveTabForward, CanExecuteForward);
            MoveTabBackdCommand = new RelayCommand(MoveTabBack, CanExecuteBack);
            ScheduleSwitchCommand = new RelayCommand(ScheduleSwitch);
            FindeEmailsCommand = new RelayCommand(FindeEmails);
            SendEmailCommand = new RelayCommand(SendEmail);

        }

        private void GetEmails() => Emails = _dataAccessService.GetEmails();

        private void SaveEmail(Emails email)
        {
            email.Id = _dataAccessService.CreateEmail(email);
            if (email.Id == 0) return;
            Emails.Add(email);
        }

        private void FindeEmails()
        {
            Emails = new ObservableCollection<Emails>(
                from email in Emails
                where email.Name.Contains(_searchText) || email.Value.Contains(_searchText)
                select email);
        }

        private void DeleteSender(KeyValuePair<string, object> sender)
        {
            if (SendersInfo.Contains(sender)) _sendersInfo.Remove(sender);

        }

        private void MoveTabForward(object tabControl)=>CurrentTabIndex++;

        private void MoveTabBack() => CurrentTabIndex--;

        private void ScheduleSwitch() => CurrentTabIndex = 1;

        private void SendEmail()
        {
            if (string.IsNullOrEmpty(_message))
            {
                var message = new WindowMessage("Не задан текст сообщения");
                message.ShowDialog();
                CurrentTabIndex = 2;
                return;
            }
            EmailSenderConfig.message = _message;
            (new EmailSendServiceClass(_sender.Key, (string)_sender.Value)).SendEmail(_currentEmail.Value);
        }

        private bool CanExecuteForward(object tabControl) => CurrentTabIndex < ((TabControl)tabControl).Items.Count-1;

        private bool CanExecuteBack() => CurrentTabIndex > 0;


    }
}
