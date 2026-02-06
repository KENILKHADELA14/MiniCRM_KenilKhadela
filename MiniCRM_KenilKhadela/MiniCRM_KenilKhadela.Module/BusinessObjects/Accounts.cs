using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using DevExpress.XtraReports.Native.CodeCompletion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniCRM_KenilKhadela.Module.BusinessObjects
{
    [XafDefaultProperty(nameof(AccountName))]
    [DefaultClassOptions]
    public class Accounts : BaseObject
    {

        public Accounts(Session session):base(session) { 
        }

        private string accountName;
        private string readablename;
        private string phone;
        private string fax;
        private string website;
        private string tickerSymbol;


        private Address address1;
        private Address address2;

        public string AccountName
        {
            get => accountName;
            set => SetPropertyValue(nameof(AccountName),ref accountName, value);
        }

        public string ReadableName
        {
            get => readablename;
            set => SetPropertyValue(nameof(ReadableName),ref readablename, value);
        }

        public string Phone
        {
            get => phone;
            set => SetPropertyValue(nameof(Phone),ref phone, value);
        }

        public string Fax
        {
            get=> fax;
            set=> SetPropertyValue(nameof(Fax),ref fax, value);
        }

        public string Website
        {
            get=> website;
            set=> SetPropertyValue(nameof(Website),ref website, value);
        }

        public string TickerSymbol
        {
            get => tickerSymbol;
            set => SetPropertyValue(nameof(TickerSymbol),ref tickerSymbol, value);
        }


        public Address Address1
        {
            get => address1;
            set => SetPropertyValue(nameof(Address1),ref address1, value);
        }

        public Address Address2
        {
            get=> address2;
            set=> SetPropertyValue(nameof(Address2),ref address2, value);
        }

        private Contact primaryContact;
        [VisibleInListView(false)]
        public Contact PrimaryContact
        {
            get=> primaryContact;
            set=> SetPropertyValue(nameof(PrimaryContact),ref primaryContact, value);
        }

        private Opportunities opportunities;
        [Association("Opportunities-Accounts")]
        public XPCollection<Opportunities> Opportunities => GetCollection<Opportunities>(nameof(Opportunities));

        private Contact contact;
        [Association("Contacts-Accounts")]
        public XPCollection<Contact> Contacts => GetCollection<Contact>(nameof(Contacts));

        private ChildrenAccounts childrenAccounts;
        [Association("ChilderenAccounts-Accounts")]
        public XPCollection<ChildrenAccounts> ChildrenAccounts=>GetCollection<ChildrenAccounts>(nameof(ChildrenAccounts));
    }
}
