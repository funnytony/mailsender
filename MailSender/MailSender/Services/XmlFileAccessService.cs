using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;

namespace MailSender.Services
{
    public class XmlFileAccessService : IDataAccessService
    {
        private readonly XElement _emailsXML = XElement.Load("./Data/Emails.xml");
        public int CreateEmail(Emails email)
        {
            var emailsXml = from emailXml in _emailsXML.Elements()
                            where emailXml.Element("Id").Value == email.Id.ToString() &&
                            emailXml.Element("Name").Value == email.Name &&
                            emailXml.Element("Value").Value == email.Value
                            select emailXml;
            if (emailsXml.Count() > 0) return email.Id;
            _emailsXML.Add(new XElement("Email",
                new XElement("Id", email.Id.ToString()),
                new XElement("Name", email.Name),
                new XElement("Value", email.Value)));
            _emailsXML.Save("./Data/Emails.xml");
           
            return email.Id;
        }

        public ObservableCollection<Emails> GetEmails()
        {
            return new ObservableCollection<Emails>(
                from email in _emailsXML.Elements()
                select new Emails
                {
                    Id = int.Parse(email.Element("Id").Value),
                    Name = email.Element("Name").Value,
                    Value = email.Element("Value").Value
                });

        }
        
    }
}
