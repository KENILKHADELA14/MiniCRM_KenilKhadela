using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AggregatedAttribute = DevExpress.Xpo.AggregatedAttribute;

public enum CountryList
{
    India=0,
    USA=1,
    Japan=2
}

namespace MiniCRM_KenilKhadela.Module.BusinessObjects
{
    [XafDefaultProperty(nameof(FullAddress))]
    public class Address : BaseObject
    {
        public Address(Session session) : base(session) { }

        //private CountryList country;
        private string zipCode;
        private string city;
        private string street;
        private string phone1;
        private string phone2;
        private string addressType;



        //public CountryList Country
        //{
        //    get => country;
        //    set => SetPropertyValue(nameof(Country), ref country, value);
        //}

        private Country country;
        public Country Country
        {
            get => country;
            set => SetPropertyValue(nameof(Country), ref country, value);
        }

        [XafDisplayName("Zip/PostalCode")]
        public string ZipCode
        {
            get => zipCode;
            set => SetPropertyValue(nameof(ZipCode), ref zipCode, value);
        }

        private string state;
        [XafDisplayName("State/Province")]
        public string State
        {
            get => state;
            set => SetPropertyValue(nameof(State), ref state, value);
        }

        public string City
        {
            get => city;
            set => SetPropertyValue(nameof(City), ref city, value);
        }

        public string Street
        {
            get => street;
            set => SetPropertyValue(nameof(Street), ref street, value);
        }

        public string Phone1
        {
            get => phone1;
            set => SetPropertyValue(nameof(Phone1), ref phone1, value);
        }

        public string Phone2
        {
            get => phone2;
            set => SetPropertyValue(nameof(Phone2), ref phone2, value);
        }

        [NonPersistent]
        public string FullAddress => $"{Country?.CountryName} {State} {City} {Street} {ZipCode}";

        public string AddressType
        {
            get => addressType;
            set => SetPropertyValue(nameof(AddressType), ref addressType, value);
        }

    }
}
