using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender
{
    public static class SmtpServers
    {
        public static Dictionary<string, int> SmtpServersDictionary { get; } = new Dictionary<string, int>
        {
            {"smtp.gmail.com", 465 },
            {"smtp.mail.ru", 465 }
        };
    }
}
