using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MailSender.Services;
using System.Collections.ObjectModel;
using System.Linq;

namespace MailSender.ViewModel
{
    public class WpfMailSenderViewModel:ViewModelBase
    {
        private IDataAccessService _dataAccessService;
        private ObservableCollection<Emails> _emails = new ObservableCollection<Emails>();
        private Emails _currentEmail = new Emails();
        private int _currentTabIndex;
        private string _searchText;


        public ObservableCollection<Emails> Emails { get => _emails; set => Set(ref _emails, value); }

        public Emails CurrentEmail { get => _currentEmail; set => Set(ref _currentEmail, value); }

        public int CurrentTabIndex { get => _currentTabIndex; set => Set(ref _currentTabIndex, value); }

        public string SearchText { get => _searchText; set => Set(ref _searchText, value); }

        public RelayCommand ReadAllEmailsCommand { get; }

        public RelayCommand<Emails> SaveEmailCommand { get; }

        public RelayCommand FindeEmailsCommand { get; }

        public RelayCommand MoveTabForwardCommand { get; }

        public RelayCommand MoveTabBackdCommand { get; }

        public RelayCommand ScheduleSwitchCommand { get;  }
        

        public WpfMailSenderViewModel(IDataAccessService dataAccessService)
        {
            _dataAccessService = dataAccessService;
            ReadAllEmailsCommand = new RelayCommand(GetEmails);
            SaveEmailCommand = new RelayCommand<Emails>(SaveEmail);
            MoveTabForwardCommand = new RelayCommand(MoveTabForward, CanExecuteForward);
            MoveTabBackdCommand = new RelayCommand(MoveTabBack, CanExecuteBack);
            ScheduleSwitchCommand = new RelayCommand(ScheduleSwitch);
            FindeEmailsCommand = new RelayCommand(FindeEmails);

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

        private void MoveTabForward()=>CurrentTabIndex++;

        private void MoveTabBack() => CurrentTabIndex--;

        private void ScheduleSwitch() => CurrentTabIndex = 1;

        private bool CanExecuteForward() => CurrentTabIndex < 2;

        private bool CanExecuteBack() => CurrentTabIndex > 0;


    }
}
