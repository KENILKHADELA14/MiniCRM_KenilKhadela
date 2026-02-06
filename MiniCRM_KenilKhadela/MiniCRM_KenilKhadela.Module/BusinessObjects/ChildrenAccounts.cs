using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniCRM_KenilKhadela.Module.BusinessObjects
{


    public class ChildrenAccounts : BaseObject
    {
        public ChildrenAccounts(Session session):base(session) { }

        private string accountName;
        private string phone;
        private string city;
        private Contact primaryContact;
        private string email;
        private string owner;
        private DateTime modifiedOn;

        public string AccountName
        {
            get => accountName;
            set=> SetPropertyValue(nameof(AccountName),ref accountName, value);
        }

        public string Phone
        {
            get => phone;
            set => SetPropertyValue(nameof(Phone),ref phone, value);
        }

        public string City
        {
            get=> city;
            set=> SetPropertyValue(nameof(City),ref city, value);
        }

        public Contact PrimaryContact
        {
            get=> primaryContact;
            set=>SetPropertyValue(nameof(PrimaryContact),ref primaryContact, value);
        }

        public string Email
        {
            get => email;
            set => SetPropertyValue(nameof(Email),ref email, value);
        }

        public string Owner
        {
            get=> owner;
            set=> SetPropertyValue(nameof(Owner),ref owner, value);
        }

        public DateTime ModifiedOn
        {
            get=> modifiedOn;
            set=> SetPropertyValue(nameof(ModifiedOn),ref modifiedOn, value);
        }
        private Accounts accounts;
        [Association("ChilderenAccounts-Accounts")]
        public Accounts Accounts
        {
            get => accounts;
            set=> SetPropertyValue(nameof(Accounts),ref accounts, value);
        }
    }
}
