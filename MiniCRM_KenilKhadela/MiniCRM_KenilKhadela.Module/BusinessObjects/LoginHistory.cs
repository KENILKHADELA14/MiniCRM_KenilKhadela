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

[NavigationItem("Security")]
public class LoginHistory : BaseObject
{
    // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://docs.devexpress.com/eXpressAppFramework/113146/business-model-design-orm/business-model-design-with-xpo/base-persistent-classes).
    // Use CodeRush to create XPO classes and properties with a few keystrokes.
    // https://docs.devexpress.com/CodeRushForRoslyn/118557
    public LoginHistory(Session session)
        : base(session)
    {
    }
    public override void AfterConstruction()
    {
        base.AfterConstruction();
    }

    private string userName;
    public string UserName
    {
        get => userName;
        set => SetPropertyValue(nameof(UserName), ref userName, value);
    }

    private string operation; public string Operation { get => operation; set => SetPropertyValue(nameof(Operation), ref operation, value); }

    private DateTime loginTime;
    [ModelDefault("EditMask", "G")]
    [ModelDefault("DisplayFormat", "G")]
    [ModelDefault("EditMaskType", "DateTime")]
    public DateTime LoginTime
    {
        get => loginTime;
        set => SetPropertyValue(nameof(LoginTime), ref loginTime, value);
    }

    public string IPAddress = System.Net.IPAddress.Loopback.ToString();
    public string HostName = System.Net.Dns.GetHostName();

    private DateTime? logoutTime; public DateTime? LogoutTime { get => logoutTime; set => SetPropertyValue(nameof(LogoutTime), ref logoutTime, value); }

    private XPCollection<AuditDataItemPersistent> auditTrail;
    [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
    public XPCollection<AuditDataItemPersistent> AuditTrail
    {
        get
        {
            if (auditTrail == null)
            {
                auditTrail = AuditedObjectWeakReference.GetAuditTrail(Session, this);
            }
            return auditTrail;
        }
    }

}
