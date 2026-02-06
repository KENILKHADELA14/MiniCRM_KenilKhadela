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
using DevExpress.XtraRichEdit.Commands;

namespace MiniCRM_KenilKhadela.Module.BusinessObjects;
public enum ActivityDirection
{
    Incoming = 0,
    Outgoing = 1
}

public enum ActivityState
{
    Open = 0,
    Made = 1,
    Canceled = 2,
    Received = 3
}
[DefaultClassOptions]
//[ImageName("BO_Contact")]
//[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
//[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
//[Persistent("DatabaseTableName")]
// Specify more UI options using a declarative approach (https://docs.devexpress.com/eXpressAppFramework/112701/business-model-design-orm/data-annotations-in-data-model).
public class Activity : BaseObject {
    // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://docs.devexpress.com/eXpressAppFramework/113146/business-model-design-orm/business-model-design-with-xpo/base-persistent-classes).
    // Use CodeRush to create XPO classes and properties with a few keystrokes.
    // https://docs.devexpress.com/CodeRushForRoslyn/118557
    public Activity(Session session)
        : base(session) {
    }

    private string subject;
    public string Subject
    {
        get => subject;
        set=> SetPropertyValue(nameof(Subject),ref subject,value);
    }

    private string description;
    [Size(SizeAttribute.Unlimited)]
    public string Description
    {
        get => description;
        set => SetPropertyValue(nameof(Description),ref description,value);
    }

    private DateTime startDate;
    [ModelDefault("EditMask","g")]
    [ModelDefault("DisplayFormat","g")]
    [ModelDefault("EditMaskType","DateTime")]
    public DateTime StartDate
    {
        get => startDate;
        set => SetPropertyValue(nameof(StartDate),ref startDate,value);
    }

    private DateTime endDate;
    [ModelDefault("EditMask", "g")]
    [ModelDefault("DisplayFormat", "g")]
    [ModelDefault("EditMaskType", "DateTime")]
    public DateTime EndDate
    {
        get=> endDate;
        set => SetPropertyValue(nameof(EndDate),ref endDate,value);
    }

    private ActivityState state;
    public ActivityState State
    {
        get => state;
        set => SetPropertyValue(nameof(State), ref state, value);
    }

    

    private Lead lead;
    [Association("Activities-Leads")]
    [VisibleInDetailView(false)]
    public Lead Lead
    {
        get => lead;
        set => SetPropertyValue(nameof(Lead), ref lead, value);
    }

    private Contact contact;
    [Association("Contact-Activities")]
    [VisibleInDetailView(false)]
    public Contact Contact
    {
        get => contact;
        set => SetPropertyValue(nameof(Contact), ref contact, value);
    }

    public override void AfterConstruction() {
        base.AfterConstruction();
    }
    
}