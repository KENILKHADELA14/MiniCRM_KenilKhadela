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

namespace MiniCRM_KenilKhadela.Module.BusinessObjects
{
    [NavigationItem("Security")]
    public class LoginHistory : BaseObject
    {
        public LoginHistory(Session session) : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (Session.IsNewObject(this))
            {
                LoginTime = DateTime.Now;
            }
        }

        private string _userName;
        [Size(255)]
        [Persistent("UserName")]
        [Indexed(Name = "UserNameIndex")]
        public string UserName
        {
            get { return _userName; }
            set { SetPropertyValue(nameof(UserName), ref _userName, value); }
        }

        private string _operation;
        [Size(50)]
        [Persistent("Operation")]
        public string Operation
        {
            get { return _operation; }
            set { SetPropertyValue(nameof(Operation), ref _operation, value); }
        }

        private DateTime _loginTime;
        [Persistent("LoginTime")]
        [ModelDefault("EditMask", "G")]
        [ModelDefault("DisplayFormat", "G")]
        [ModelDefault("EditMaskType", "DateTime")]
        [Indexed(Name = "LoginTimeIndex")]
        public DateTime LoginTime
        {
            get { return _loginTime; }
            set { SetPropertyValue(nameof(LoginTime), ref _loginTime, value); }
        }

        private string _iPAddress;
        [Size(50)]
        [Persistent("IPAddress")]
        [ModelDefault("AllowEdit", "False")]
        public string IPAddress
        {
            get { return _iPAddress; }
            set { SetPropertyValue(nameof(IPAddress), ref _iPAddress, value); }
        }

        private string _hostName;
        [Size(255)]
        [Persistent("HostName")]
        [ModelDefault("AllowEdit", "False")]
        public string HostName
        {
            get { return _hostName; }
            set { SetPropertyValue(nameof(HostName), ref _hostName, value); }
        }

        private DateTime? _logoutTime;
        [Persistent("LogoutTime")]
        [ModelDefault("EditMask", "G")]
        [ModelDefault("DisplayFormat", "G")]
        [ModelDefault("EditMaskType", "DateTime")]
        public DateTime? LogoutTime
        {
            get { return _logoutTime; }
            set { SetPropertyValue(nameof(LogoutTime), ref _logoutTime, value); }
        }

        [Persistent("IsActive")]
        private bool _isActive;
        [PersistentAlias("LogoutTime == null")]
        [ModelDefault("AllowEdit", "False")]
        public bool IsActive
        {
            get { return LogoutTime == null; }
        }

        private XPCollection<AuditDataItemPersistent> _auditTrail;
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<AuditDataItemPersistent> AuditTrail
        {
            get
            {
                if (_auditTrail == null)
                {
                    _auditTrail = AuditedObjectWeakReference.GetAuditTrail(Session, this);
                }
                return _auditTrail;
            }
        }
    }
}
