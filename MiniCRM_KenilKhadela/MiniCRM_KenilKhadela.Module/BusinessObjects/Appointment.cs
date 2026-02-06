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
using DevExpress.Persistent.Base.General;
using DevExpress.ExpressApp.Scheduler;
using DevExpress.Charts.Native;
using DevExpress.CodeParser;

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
public class Appointment : Activity,IEvent {

    public Appointment(Session session)
        : base(session) {
    }
    public override void AfterConstruction() {
        base.AfterConstruction();
        this.Type = 0;
        this.StartOn = DateTime.Now;
        this.EndOn = DateTime.Now.AddHours(1);
    }


    [Browsable(false)]
    public object AppointmentId => Oid;

    public DateTime StartOn { get => StartDate; set => StartDate = value; }
    public DateTime EndOn { get => EndDate; set => EndDate = value; }
    string IEvent.Subject { get => Subject; set => Subject = value; }
    string IEvent.Description { get => Description; set => Description = value; }

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
        set => SetPropertyValue(nameof(AllDay), ref allDay, value);
    }

    private string resourceId;
    [Size(SizeAttribute.Unlimited)]
    public string ResourceId
    {
        get => resourceId;
        set => SetPropertyValue(nameof(ResourceId), ref resourceId, value);
    }

    [Size(SizeAttribute.Unlimited)]
    public string RecurrenceInfoXml { get; set; }

    private int label;
    public int Label
    {
        get => label;
        set => SetPropertyValue(nameof(Label), ref label, value);
    }

    private int status;
    public int Status
    {
        get => status;
        set => SetPropertyValue(nameof(Status), ref status, value);
    }


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