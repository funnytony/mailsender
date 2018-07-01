
using System.Windows;
using System.Net.Mail;
using System.Net;
using System;

namespace MailSender
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs eve)
        {           

            var login = UserLoginTextBox.Text;
            var pass = UserPassworBox.Password;

            var title = TitleTextBox.Text;
            var message = MessageTextBox.Text;

            var emailSender = new EmailSendServiceClass("user@gmail.com");
            emailSender.SendEmail(login, pass, title, message);

        }
    }
}
