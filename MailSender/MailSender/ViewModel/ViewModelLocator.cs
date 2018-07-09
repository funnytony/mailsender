using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using MailSender.Services;

namespace MailSender.ViewModel
{
    
    public class ViewModelLocator
    {
        
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<WpfMailSenderViewModel>();
            SimpleIoc.Default.Register<IDataAccessService, DataBaseAccessService>();
        }

        public WpfMailSenderViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<WpfMailSenderViewModel>();
            }
        }
        
        public static void Cleanup(){}
    }
}