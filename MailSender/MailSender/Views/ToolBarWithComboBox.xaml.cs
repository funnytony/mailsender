
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        public static readonly DependencyProperty IndexProperty = DependencyProperty.Register("Index",
            typeof(int),
            typeof(ToolBarWithComboBox),
            new FrameworkPropertyMetadata(0, new PropertyChangedCallback(OnIndexPropertyChanged)));

        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register("Items",
            typeof(IEnumerable<KeyValuePair<string, object>>),
            typeof(ToolBarWithComboBox),
            new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnItemsPropertyChanged)),
            new ValidateValueCallback(OnValidateItemsProperty));

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem",
            typeof(KeyValuePair<string, object>),
            typeof(ToolBarWithComboBox),
            new FrameworkPropertyMetadata(new KeyValuePair<string, object>(), new PropertyChangedCallback(OnSelectedItemPropertyChanged)),
            new ValidateValueCallback(OnValidateSelectedItemProperty));

        public static readonly DependencyProperty DeleteCommandParametrProperty = DependencyProperty.Register("DeleteCommandParametr",
            typeof(KeyValuePair<string, object>),
            typeof(ToolBarWithComboBox),
            new FrameworkPropertyMetadata(new KeyValuePair<string, object>(), new PropertyChangedCallback(OnDeleteCommandParametrPropertyChanged)),
            new ValidateValueCallback(OnValidateDeleteCommandParametrProperty));

        public static readonly DependencyProperty AddCommandProperty = DependencyProperty.Register("AddCommand",
            typeof(ICommand),
            typeof(ToolBarWithComboBox));

        public static readonly DependencyProperty DeleteCommandProperty = DependencyProperty.Register("DeleteCommand",
            typeof(ICommand),
            typeof(ToolBarWithComboBox));

        public static readonly DependencyProperty EditeCommandProperty = DependencyProperty.Register("EditeCommand",
            typeof(ICommand),
            typeof(ToolBarWithComboBox));

        public string Title { get => (string)GetValue(TitleProperty); set => SetValue(TitleProperty, value); }

        public int Index { get=>(int)GetValue(IndexProperty); set=>SetValue(IndexProperty,value); }

        public IEnumerable<KeyValuePair<string, object>> Items { get => (IEnumerable<KeyValuePair<string, object>>)GetValue(ItemsProperty); set => SetValue(ItemsProperty, value); }

        public KeyValuePair<string, object> SelectedItem { get => (KeyValuePair<string, object>)GetValue(SelectedItemProperty); set => SetValue(SelectedItemProperty, value); }

        public KeyValuePair<string, object> DeleteCommandParametr { get => (KeyValuePair<string, object>)GetValue(DeleteCommandParametrProperty); set => SetValue(DeleteCommandParametrProperty, value); }

        public ICommand AddCommand
        {
            get => (ICommand)GetValue(AddCommandProperty);
            set => SetValue(AddCommandProperty, value);
        }

        public ICommand DeleteCommand
        {
            get => (ICommand)GetValue(DeleteCommandProperty);
            set => SetValue(DeleteCommandProperty, value);
        }

        public ICommand EditeCommand
        {
            get => (ICommand)GetValue(EditeCommandProperty);
            set => SetValue(EditeCommandProperty, value);
        }


        private static void OnTitleChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ToolBarWithComboBox toolBar = (ToolBarWithComboBox)obj;
            toolBar.Title = args.NewValue.ToString();
        }

        private static void OnIndexPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ToolBarWithComboBox toolBar = (ToolBarWithComboBox)obj;
            toolBar.Index = (int)args.NewValue;
        }

        private static void OnItemsPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ToolBarWithComboBox toolBar = (ToolBarWithComboBox)obj;            
            toolBar.Items = (IEnumerable<KeyValuePair<string, object>>)args.NewValue;
        }

        private static bool OnValidateItemsProperty (object value)
        {
            return (value == null?true: value is IEnumerable<KeyValuePair<string, object>>);
        }

        private static void OnSelectedItemPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ToolBarWithComboBox toolBar = (ToolBarWithComboBox)obj;            
            toolBar.SelectedItem = (KeyValuePair<string,object>)args.NewValue;
        }

        private static bool OnValidateSelectedItemProperty (object value)
        {
            return (value == null ? true : value is KeyValuePair<string, object>);
        }

        private static void OnDeleteCommandParametrPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ToolBarWithComboBox toolBar = (ToolBarWithComboBox)obj;
            toolBar.DeleteCommandParametr = (KeyValuePair<string, object>)args.NewValue;
        }

        private static bool OnValidateDeleteCommandParametrProperty(object value)
        {
            return (value == null ? true : value is KeyValuePair<string, object>);
        }
    }
}
