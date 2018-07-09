using System.Collections.ObjectModel;
using System.Linq;


namespace MailSender.Services
{
    public class DataBaseAccessService : IDataAccessService
    {
        private readonly MailSenderDataClassesDataContext _emailsDataContext = new MailSenderDataClassesDataContext();
        public int CreateEmail(Emails email)
        {
            if (_emailsDataContext.Emails.Contains(email)) return email.Id;
            _emailsDataContext.Emails.InsertOnSubmit(email);
            _emailsDataContext.SubmitChanges();
            return email.Id;
        }

        public ObservableCollection<Emails> GetEmails() => new ObservableCollection<Emails>(_emailsDataContext.Emails);
        
    }
}
