using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MiniCRM_KenilKhadela.Module.BusinessObjects;

[DefaultClassOptions]
public class Contact : BaseObject {
   
    public Contact(Session session)
        : base(session) {
    }
    public override void AfterConstruction() {
        base.AfterConstruction();
    }

    private string firstName;
    [RuleRequiredField]
    [XafDisplayName(nameof(FirstName))]
    public string FirstName
    {
        get => firstName;
        set => SetPropertyValue(nameof(FirstName), ref firstName, value);
    }

    private string lastName;
    [RuleRequiredField]
    [XafDisplayName(nameof(LastName))]
    public string LastName
    {
        get => lastName;
        set => SetPropertyValue(nameof(LastName), ref lastName, value);
    }

    private string accountName;
    public string AccountName
    {
        get => accountName;
        set => SetPropertyValue(nameof(AccountName), ref accountName, value);
    }

    private string phoneNumber;
    [XafDisplayName("Phone")]
    public string PhoneNumber
    {
        get => phoneNumber;
        set => SetPropertyValue(nameof(PhoneNumber), ref phoneNumber, value);
    }

    private string mobileNumber;
    [XafDisplayName("Mobile")]
    [VisibleInListView(false)]
    public string MobileNumber
    {
        get => mobileNumber;
        set => SetPropertyValue(nameof(MobileNumber), ref mobileNumber, value);
    }

    private string fax;
    [XafDisplayName("Fax")]
    public string Fax
    {
        get => fax;
        set => SetPropertyValue(nameof(Fax), ref fax, value);
    }

    private string readableFirstName;
    [XafDisplayName("Readable First Name")]
    [VisibleInListView(false)]
    public string ReadableFirstName
    {
        get=> readableFirstName;
        set=> SetPropertyValue(nameof(ReadableFirstName), ref readableFirstName, value);
    }

    private string readableLastName;
    [XafDisplayName("Readable Last Name")]
    [VisibleInListView(false)]
    public string ReadableLastName
    {
        get => readableLastName;
        set => SetPropertyValue(nameof(ReadableLastName), ref readableLastName, value);
    }

    private string title;
    [XafDisplayName("Title")]
    [VisibleInListView(false)]
    public string Title
    {
        get => title;
        set => SetPropertyValue(nameof(Title), ref title, value);
    }

    private string email;
    [XafDisplayName(nameof(Email))]
    public string Email
    {
        get => email;
        set => SetPropertyValue(nameof(Email), ref email, value);
    }
    private PermissionPolicyUser owner;
    public PermissionPolicyUser Owner
    {
        get => owner;
        set => SetPropertyValue(nameof(Owner), ref owner, value);
    }

    private DateTime? createdOn;
    [XafDisplayName(nameof(CreatedOn))]
    [VisibleInListView(false)]
    public DateTime? CreatedOn
    {
        get => createdOn;
        set => SetPropertyValue(nameof(CreatedOn), ref createdOn, value);
    }

    private Address address1;
    [XafDisplayName("Address1")]
    [VisibleInListView(false)]
    public Address Address1
    {
        get => address1;
        set => SetPropertyValue(nameof(Address1), ref address1, value);
    }

    private Address address2;
    [XafDisplayName("Address2")]
    [VisibleInListView(false)]
    public Address Address2
    {
        get => address2;
        set => SetPropertyValue(nameof(Address2), ref address2, value);
    }

    //private Opportunities opportunities;
    //[VisibleInDetailView(false)]
    //[ExpandObjectMembers(ExpandObjectMembers.Never)]
    //public Opportunities Opportunities
    //{
    //    get => opportunities;
    //    set => SetPropertyValue(nameof(Opportunities), ref opportunities, value);
    //}

    private DateTime? modifiedOn;
    [XafDisplayName(nameof(ModifiedOn))]
    public DateTime? ModifiedOn
    {
        get => modifiedOn;
        set => SetPropertyValue(nameof(ModifiedOn), ref modifiedOn, value);
    }

    [Association("Contact-Opportunities")]
    public XPCollection<Opportunities> Opportunities
    {
        get
        {
            return GetCollection<Opportunities>(nameof(Opportunities));
        }
    }

    [Association("Contacts-Accounts")]
    public XPCollection<Accounts> Accounts
    {
        get
        {
            return GetCollection<Accounts>(nameof(Accounts));
        }
    }

    public enum ContactState
    {
        New = 0,
        Active = 1,
        InActive = 2
    }

}