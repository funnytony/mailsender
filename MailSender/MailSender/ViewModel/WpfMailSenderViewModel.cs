﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MailSender.Services;
using System;
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
        private readonly ObservableCollection<KeyValuePair<string, object>> _sendersInfo;
        private readonly ObservableCollection<KeyValuePair<string, object>> _smptInfo;
        private Action ReqestCancel;
        private double _progressBar;
        private IProgress<double> _progress;
        private ObservableCollection<Emails> _selectedEmails = new ObservableCollection<Emails>();


        public ObservableCollection<Emails> Emails { get => _emails; set => Set(ref _emails, value); }

        public Emails CurrentEmail { get => _currentEmail; set => Set(ref _currentEmail, value); }

        public int CurrentTabIndex { get => _currentTabIndex; set => Set(ref _currentTabIndex, value); }

        public string SearchText { get => _searchText; set => Set(ref _searchText, value); }

        public IEnumerable<KeyValuePair<string, object>> SendersInfo => _sendersInfo;

        public IEnumerable<KeyValuePair<string, object>> SmptInfo => _smptInfo;

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

        public double ProgressBar { get => _progressBar; set => Set(ref _progressBar, value); }

        public RelayCommand ReadAllEmailsCommand { get; }

        public RelayCommand<Emails> SaveEmailCommand { get; }

        public RelayCommand FindeEmailsCommand { get; }

        public RelayCommand<KeyValuePair<string, object>> DeleteSenderCommand { get; }

        public RelayCommand<KeyValuePair<string, object>> DeleteServerCommand { get; }

        public RelayCommand<object> MoveTabForwardCommand { get; }

        public RelayCommand MoveTabBackdCommand { get; }

        public RelayCommand ScheduleSwitchCommand { get;  }

        public RelayCommand SendEmailCommand { get; }

        public RelayCommand CancelSendEmailCommand { get; }

        public RelayCommand<object> SelectEmailsCommand { get; }
        

        public WpfMailSenderViewModel(IDataAccessService dataAccessService)
        {
            _dataAccessService = dataAccessService;
            _sendersInfo = new ObservableCollection<KeyValuePair<string, object>>(Senders.SendersDictionary);
            _sender = _sendersInfo.FirstOrDefault();
            _smptInfo = new ObservableCollection<KeyValuePair<string, object>>(SmtpServers.SmtpServersDictionary);
            _smptServer = _smptInfo.FirstOrDefault();
            ReadAllEmailsCommand = new RelayCommand(GetEmails);
            SaveEmailCommand = new RelayCommand<Emails>(SaveEmail);
            DeleteSenderCommand = new RelayCommand<KeyValuePair<string, object>>(DeleteSender);
            DeleteServerCommand = new RelayCommand<KeyValuePair<string, object>>(DeleteServer);
            MoveTabForwardCommand = new RelayCommand<object>(MoveTabForward, CanExecuteForward);
            MoveTabBackdCommand = new RelayCommand(MoveTabBack, CanExecuteBack);
            ScheduleSwitchCommand = new RelayCommand(ScheduleSwitch);
            FindeEmailsCommand = new RelayCommand(FindeEmails);
            SendEmailCommand = new RelayCommand(SendEmail);
            CancelSendEmailCommand = new RelayCommand(CancelSendEmail);
            SelectEmailsCommand = new RelayCommand<object>(SelectEmails);

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
            else _sendersInfo.Remove(_sender);

        }

        private void DeleteServer(KeyValuePair<string, object> server)
        {
            if (SmptInfo.Contains(server)) _smptInfo.Remove(server);
            else _smptInfo.Remove(_smptServer);

        }

        private void MoveTabForward(object tabControl)=>CurrentTabIndex++;

        private void MoveTabBack() => CurrentTabIndex--;

        private void ScheduleSwitch() => CurrentTabIndex = 1;

        private void SendEmail()
        {
            if (string.IsNullOrEmpty(_message))
            {
                var messageView = new WindowMessage();
                var message = new WindowMessageViewModel("Ошибка отправки", "Текст сообщения пуст");
                message.ReqestClose += messageView.Close;
                messageView.DataContext = message;
                messageView.ShowDialog();                
                CurrentTabIndex = 2;
                return;
            }
            EmailSenderConfig.message = _message;
            var emailSender = new EmailSendServiceClass(_sender.Key, (string)_sender.Value);
            ReqestCancel += emailSender.Cancel;
            if (_selectedEmails.Count > 1)
            {
                _progress = new Progress<double>(d => ProgressBar = d);
                emailSender.SendEmails(_selectedEmails, _progress);
            }
            else emailSender.SendEmail(_currentEmail.Value);
        }        

        private void CancelSendEmail()
        {
            ReqestCancel?.Invoke();
            ReqestCancel = null;
        }

        private void SelectEmails(object emails)
        {
            System.Collections.IList collection = (System.Collections.IList)emails;
            _selectedEmails = new ObservableCollection<Emails>(collection.Cast<Emails>());
        }

        private bool CanExecuteForward(object tabControl) => CurrentTabIndex < ((TabControl)tabControl).Items.Count-1;

        private bool CanExecuteBack() => CurrentTabIndex > 0;


    }
}
