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


namespace MiniCRM_KenilKhadela.Module.BusinessObjects;
public enum Priority
{
    Low = 0,
    Medium = 1,
    High = 2
}



//public enum Direction
//{
//    Incoming=0,
//    Outgoing=1
//}

[DefaultClassOptions]
//[ImageName("BO_Contact")]
//[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
//[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
//[Persistent("DatabaseTableName")]
// Specify more UI options using a declarative approach (https://docs.devexpress.com/eXpressAppFramework/112701/business-model-design-orm/data-annotations-in-data-model).
public class ActivityPhone : Activity {
    // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://docs.devexpress.com/eXpressAppFramework/113146/business-model-design-orm/business-model-design-with-xpo/base-persistent-classes).
    // Use CodeRush to create XPO classes and properties with a few keystrokes.
    // https://docs.devexpress.com/CodeRushForRoslyn/118557
    public ActivityPhone(Session session)
        : base(session) {
    }
    public override void AfterConstruction()
    {
        base.AfterConstruction();
        // Place your initialization code here (https://docs.devexpress.com/eXpressAppFramework/112834/getting-started/in-depth-tutorial-winforms-webforms/business-model-design/initialize-a-property-after-creating-an-object-xpo?v=22.1).
    }

    private Priority priority;
    public Priority Priority
    {
        get => priority;
        set => SetPropertyValue(nameof(Priority), ref priority, value);
    }

    //private string subject;
    //[Size(SizeAttribute.Unlimited)]
    //public string Subject
    //{
    //    get => subject;
    //    set => SetPropertyValue(nameof(Subject), ref subject, value);
    //}

    //private string description;
    //[Size(SizeAttribute.Unlimited)]
    //public string Description
    //{
    //    get => description;
    //    set => SetPropertyValue(nameof(Description), ref description, value);
    //}

    private string phone;
    [XafDisplayName("Number")]
    public string Phone
    {
        get=> phone;
        set=> SetPropertyValue(nameof(Phone), ref phone, value);
    }

    
   

}