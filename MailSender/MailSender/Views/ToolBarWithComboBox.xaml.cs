using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MailSender.Views
{
    /// <summary>
    /// Логика взаимодействия для ToolBarWithComboBox.xaml
    /// </summary>
    public partial class ToolBarWithComboBox : UserControl
    {
        public ToolBarWithComboBox()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string),
            typeof(ToolBarWithComboBox),
            new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnTitleChanged)));

        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register("Items",
            typeof(Dictionary<string, object>),
            typeof(ToolBarWithComboBox),
            new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnItemsChanged)));

        public string Title { get => (string)GetValue(TitleProperty); set => SetValue(TitleProperty, value); }

        public Dictionary<string, object> Items { get => (Dictionary<string, object>)GetValue(ItemsProperty); set => SetValue(ItemsProperty, value); }
        
        private static void OnTitleChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ToolBarWithComboBox toolBar = (ToolBarWithComboBox)obj;
            toolBar.Title = args.NewValue.ToString();
        }

        private static void OnItemsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ToolBarWithComboBox toolBar = (ToolBarWithComboBox)obj;
            toolBar.Items = (Dictionary<string, object>)args.NewValue;
        }
    }
}
