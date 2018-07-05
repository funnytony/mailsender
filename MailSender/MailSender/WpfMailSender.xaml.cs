using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace MailSender
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class WpfMailSender : Window
    {
        public WpfMailSender()
        {
            InitializeComponent();
        }

        private void TabSwitcherControl_OnBack(object sender, RoutedEventArgs e)
        {
            if (MainTabControl.SelectedIndex == 0) return;
            MainTabControl.SelectedIndex--;
        }

        private void TabSwitcherControl_OnForward(object sender, RoutedEventArgs e)
        {
            if (MainTabControl.SelectedIndex == MainTabControl.Items.Count - 1) return;
            MainTabControl.SelectedIndex++;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(IsRichBoxEmpty(MessageTextBox))
            {
                var message = new WindowMessage("Не задан текст сообщения");
                message.ShowDialog();
                MainTabControl.SelectedIndex = 2;
                return;
            }
            EmailSenderConfig.smtpServer = ((KeyValuePair<string, int>)ServerComboBox.SelectedItem).Key;
            EmailSenderConfig.smtpPort = ((KeyValuePair<string, int>)ServerComboBox.SelectedItem).Value;
            EmailSenderConfig.from = ((KeyValuePair<string, string>)SenderComboBox.SelectedItem).Key;
        }

        private bool IsRichBoxEmpty(RichTextBox richTextBox)
        {
            if (richTextBox.Document.Blocks.Count == 0) return true;
            TextPointer startPointer = richTextBox.Document.ContentStart.GetNextInsertionPosition(LogicalDirection.Forward);
            TextPointer endPointer = richTextBox.Document.ContentEnd.GetNextInsertionPosition(LogicalDirection.Backward);
            return startPointer.CompareTo(endPointer) == 0;
        }

        private void Schedule_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedIndex = 1;
        }
    }
}
