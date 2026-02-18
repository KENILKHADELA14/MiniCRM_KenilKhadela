using DevExpress.Charts.Native;
using DevExpress.CodeParser;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Scheduler;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.General;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MiniCRM_KenilKhadela.Module.BusinessObjects;
public enum Reasons
{
    Free=0,
    Tentative=1,
    Completed=2,
    Canceled=3,
    Busy=4,
    OutOfOffice=5
}
[DefaultClassOptions]
[NavigationItem("Activity")]
[ImageName("BO_Appointment")]
public class Appointment : Activity {

    public Appointment(Session session)
        : base(session) {
    }
    public Appointment() : base(new Session()) { }
    public override void AfterConstruction() {
        base.AfterConstruction();
        this.Type = 0;
        this.StartDate = DateTime.Now;
        this.EndDate = DateTime.Now.AddHours(1);
        if (AllDay)
        {
            this.StartDate = DateTime.Today;
            this.EndDate = DateTime.Today.AddDays(1).AddSeconds(-1);
        }
    }


        [Browsable(false)]
    public object AppointmentId{
        get => Oid;
        set { }
    }

    //[PersistentAlias(nameof(StartDate))]
    //public DateTime StartOn { get => StartDate; set => StartDate = value; }
    //[PersistentAlias(nameof(EndDate))]
    //public DateTime EndOn { get => EndDate; set => EndDate = value; }

    private int type;
    public int Type
    {
        get => type;
        set => SetPropertyValue(nameof(Type), ref type, value);
    }

    private bool allDay;
    [XafDisplayName("IsAllDay")]
    public bool AllDay
    {
        get => allDay;
        set
        {
            if (SetPropertyValue(nameof(AllDay), ref allDay, value))
            {
                if (!IsLoading && !IsSaving)
                {
                    if (value)
                    {
                        this.StartDate = this.StartDate.Date;
                        this.EndDate = this.StartDate.AddDays(1).AddSeconds(-1);
                    }
                    else
                    {
                        this.EndDate = this.StartDate.AddHours(1);
                    }
                }
            }
        }
    }

    private string resourceId;
    [Size(SizeAttribute.Unlimited)]
    public string ResourceId
    {
        get => resourceId;
        set => SetPropertyValue(nameof(ResourceId), ref resourceId, value);
    }

    private string recurrenceInfoXml;
    [Size(SizeAttribute.Unlimited)]
    public string RecurrenceInfoXml
    {
        get => recurrenceInfoXml;
        set => SetPropertyValue(nameof(RecurrenceInfoXml), ref recurrenceInfoXml, value);
    }


    private int label;
    public int Label
    {
        get => label;
        set => SetPropertyValue(nameof(Label), ref label, value);
    }

    //private int status;
    //public int Status
    //{
    //    get => status;
    //    set => SetPropertyValue(nameof(Status), ref status, value);
    //}


    private Priority priority;
    public Priority Priority
    {
        get => priority;
        set => SetPropertyValue(nameof(Priority), ref priority, value);
    }

    private Reasons reasons;
    public Reasons Reasons
    {
        get => reasons;
        set => SetPropertyValue(nameof(Reasons), ref reasons, value);
    }

    private string location;
    public string Location
    {
        get => location;
        set => SetPropertyValue(nameof(Location), ref location, value);
    }
}