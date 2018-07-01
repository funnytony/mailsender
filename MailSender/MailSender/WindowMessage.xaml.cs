
using System.Windows;


namespace MailSender
{
    /// <summary>
    /// Логика взаимодействия для WindowMessage.xaml
    /// </summary>
    public partial class WindowMessage : Window
    {
        public WindowMessage(string message)
        {
            InitializeComponent();
            MessageLable.Content = message;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        
    }
}
