using PasswordDll;
using System.Collections.Generic;


namespace MailSender
{
    public static class Senders
    {
        public static Dictionary<string, object> SendersDictionary { get; } = new Dictionary<string, object>
        {
            {"qwe@ewq.ru",  Encrypter.Encrypt("123")},
            {"qaz@zaq.ru",  Encrypter.Encrypt("321")}
        };
    }
}
