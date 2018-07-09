using System.Collections.ObjectModel;


namespace MailSender.Services
{
    public interface IDataAccessService
    {
        ObservableCollection<Emails> GetEmails();
        int CreateEmail(Emails email);
    }
}
