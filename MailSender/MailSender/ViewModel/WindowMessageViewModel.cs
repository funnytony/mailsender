using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.ViewModel
{
    public class WindowMessageViewModel: ViewModelBase
    {
        private string _title;
        private string _message;

        public WindowMessageViewModel(string title, string message)
        {
            Title = title;
            _message = message;
            CloseCommand = new RelayCommand(Close);

        }

        public string Title { get => _title; set => Set( ref _title, value); }

        public string Message { get => _message; set => Set(ref _message, value); }

        public Action ReqestClose;

        public RelayCommand CloseCommand { get; }

        private void Close()
        {
            ReqestClose?.Invoke();
        }


    }
}
