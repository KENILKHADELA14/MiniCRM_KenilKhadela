using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniCRM_KenilKhadela.Module.BusinessObjects
{
    public class Country : BaseObject
    {
        public Country(Session session) : base(session) { }

        private string countryName;
        public string CountryName
        {
            get => countryName;
            set => SetPropertyValue(nameof(CountryName), ref countryName, value);
        }

    }
}
