using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using System.Collections.Generic;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using AggregatedAttribute = DevExpress.Xpo.AggregatedAttribute;
namespace MiniCRM_KenilKhadela.Module.BusinessObjects;

[DefaultClassOptions]
[NavigationItem("Sales")]
[ImageName("BO_Lead")]
public class Lead : BaseObject
{
    public Lead(Session session)
        : base(session)
    {
    }


    public override void AfterConstruction()
    {
        base.AfterConstruction();
        if (SecuritySystem.CurrentUser is PermissionPolicyUser user)
        {
            Owner = Session.GetObjectByKey<PermissionPolicyUser>(user.Oid);
        }
        CreatedOn = DateTime.Now;
    }

    [PersistentAlias("concat(FirstName, '(',LastName,')')")]
    [ReadOnly(true)]
    [VisibleInListView(false)]
    public string FullName => $"{FirstName} {LastName}";

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

    private string subject;
    [RuleRequiredField]
    [XafDisplayName(nameof(Subject))]
    [Size(SizeAttribute.Unlimited)]
    public string Subject
    {
        get => subject;
        set => SetPropertyValue(nameof(Subject), ref subject, value);
    }
        
    private DateTime? createdOn;
    [XafDisplayName(nameof(CreatedOn))]
    public DateTime? CreatedOn
    {
        get => createdOn;
        set => SetPropertyValue(nameof(CreatedOn), ref createdOn, value);
    }

    private DateTime? modifiedOn;
    [XafDisplayName(nameof(ModifiedOn))]
    public DateTime? ModifiedOn
    {
        get => modifiedOn;
        set => SetPropertyValue(nameof(ModifiedOn), ref modifiedOn, value);
    }

    private string readableFirstName;
    [XafDisplayName("Readable FirstName")]
    [RuleRequiredField]
    [VisibleInListView(false)]
    public string ReadableFirstName
    {
        get => readableFirstName;
        set => SetPropertyValue(nameof(ReadableFirstName),ref  readableFirstName, value);
    }

    private string readableLastName;
    [XafDisplayName("Readable LastName")]
    [RuleRequiredField]
    [VisibleInListView(false)]
    public string ReadableLastName
    {
        get => readableLastName;
        set => SetPropertyValue(nameof(ReadableLastName), ref readableLastName, value);
    }

    private string readableMiddleName;
    [VisibleInListView(false)]
    [XafDisplayName("Readable MiddleName")]
    public string ReadableMiddleName
    {
        get=> readableMiddleName;
        set=> SetPropertyValue(nameof(ReadableMiddleName),ref readableMiddleName, value);
    }

    protected override void OnSaved()
    {
        base.OnSaving();
        ModifiedOn = DateTime.Now;
    }

    private PermissionPolicyUser owner;
    public PermissionPolicyUser Owner
    {
        get => owner;
        set => SetPropertyValue(nameof(Owner), ref owner, value);
    }

    private string title;
    [RuleRequiredField]
    [VisibleInListView(false)]
    public string Title
    {
        get=> title;
        set=> SetPropertyValue(nameof(Title), ref title, value);
    }

    private Address address1;
    private Address address2;

    [VisibleInListView(false)]
    [ImmediatePostData]
    [ExpandObjectMembers(ExpandObjectMembers.Never)]
    [RuleRequiredField]
    [Aggregated]
    public Address Address1
    {
        get => address1;
        set => SetPropertyValue(nameof(Address1), ref address1, value);
    }

    [VisibleInListView(false)]
    [ImmediatePostData]
    [RuleRequiredField]
    [ExpandObjectMembers(ExpandObjectMembers.Never)]
    public Address Address2
    {
        get => address2;
        set => SetPropertyValue(nameof(Address2), ref address2, value);
    }

    private string email;
    [VisibleInListView(false)]
    [RuleRequiredField]
    [RuleRegularExpression(
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            CustomMessageTemplate = "Invalid email")]
    public string Email
    {
        get => email;
        set => SetPropertyValue(nameof(Email), ref email, value);
    }

    private string phone;
    [RuleRegularExpression(@"^[0-9+\- ]{7-15}$",CustomMessageTemplate ="Invalid Phone Number")]
    [VisibleInListView(false)]
    public string Phone
    {
        get=> phone;
        set => SetPropertyValue(nameof(Phone), ref phone, value);
    }

    private LeadStatusEnum leadStatus;
    [XafDisplayName("State")]
    public LeadStatusEnum LeadStatus
    {
        get => leadStatus;
        set => SetPropertyValue(nameof(LeadStatus), ref leadStatus, value);
    }

    private LeadProcessStageEnum leadProcessStage;
    [XafDisplayName("Process Stage")]
    public LeadProcessStageEnum LeadProcessStage
    {

        get => leadProcessStage;
        set => SetPropertyValue(nameof(LeadProcessStage), ref leadProcessStage, value);

    }

    private string company;
    [XafDisplayName("Company")]
    [RuleUniqueValue]
    [VisibleInListView(false)]
    public string Company
    {
        get=> company;
        set=> SetPropertyValue(nameof(Company), ref company, value);
    }

    private string companyName;
    [XafDisplayName("Readable Company Name")]
    [RuleUniqueValue]
    [VisibleInListView(false)]
    public string CompanyName
    {
        get => companyName;
        set => SetPropertyValue(nameof(CompanyName), ref companyName, value);
    }

    private string website;
    [VisibleInListView(false)]
    public string Website
    {
        get=> website;
        set=> SetPropertyValue(nameof(Website), ref website, value);
    }

    private Accounts parentAccount;
    [VisibleInListView(false)]
    [ExpandObjectMembers(ExpandObjectMembers.Never)]
    public Accounts ParentAccount
    {
        get => parentAccount;
        set => SetPropertyValue(nameof(ParentAccount), ref parentAccount, value);
    }

    private Opportunities qualifyingOpportunities;
    [VisibleInListView(false)]
    [ExpandObjectMembers(ExpandObjectMembers.Never)]
    public Opportunities QualifyingOpportunities
    {
        get => qualifyingOpportunities;
        set => SetPropertyValue(nameof(QualifyingOpportunities),ref qualifyingOpportunities, value);
    }

    private Contact parentContact;
    [VisibleInListView(false)]
    [ExpandObjectMembers(ExpandObjectMembers.Never)]
    public Contact ParentContact
    {
        get => parentContact;
        set => SetPropertyValue(nameof(ParentContact), ref parentContact, value);
    }


    public enum LeadStatusEnum
    {
        Open = 0,
        Qualified = 1,
        DisQualified = 2
    }

    public enum LeadProcessStageEnum
    {
        Open = 0,
        Qualified = 1,
        Develop = 2,
        Propose = 3,
        Closed = 4
    }

}