using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender
{
    public static class DataBase
    {
        private static MailSenderDataClassesDataContext _emailsDataContext = new MailSenderDataClassesDataContext();

        public static IQueryable<Emails> Emails => from email in _emailsDataContext.Emails select email;
    }
}
