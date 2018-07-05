using PasswordDll;
using System.Collections.Generic;


namespace MailSender
{
    public static class Senders
    {
        public static Dictionary<string, string> SendersDictionary { get; } = new Dictionary<string, string>
        {
            {"qwe@ewq.ru",  Encrypter.Deencrypt("123")},
            {"qaz@zaq.ru",  Encrypter.Deencrypt("321")}
        };
    }
}
