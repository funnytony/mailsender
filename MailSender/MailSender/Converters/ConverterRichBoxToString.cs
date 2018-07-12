using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Documents;

namespace MailSender.Converters
{
    public class ConverterRichBoxToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            FlowDocument flowDocument = new FlowDocument();
            flowDocument.Blocks.Add(new Paragraph(new Run((string)value)));
            return flowDocument;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TextRange textRange = new TextRange(((FlowDocument)value).ContentStart, ((FlowDocument)value).ContentEnd);
            return textRange.Text;
        }
    }
}
