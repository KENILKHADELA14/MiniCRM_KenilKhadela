using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using Microsoft.AspNetCore.Components;
using MiniCRM_KenilKhadela.Module.BusinessObjects;
using MiniCRM_KenilKhadela.Blazor.Server.Pages.Components;
using DevExpress.ExpressApp.Blazor;

namespace MiniCRM_KenilKhadela.Blazor.Server.Editors;

public class CalendarContentHolder : IComponentContentHolder
{
    private readonly IEnumerable<Appointment> data;

    public CalendarContentHolder(IEnumerable<Appointment> data)
    {
        this.data = data;
    }


    RenderFragment IComponentContentHolder.ComponentContent =>
        builder =>
        {
            builder.OpenComponent<LeadCalendar>(0);
            builder.AddAttribute(1, "DataSource", data);
            builder.CloseComponent();
        };
}

[ViewItem(typeof(IModelViewItem))]
public class LeadCalendarViewItem : ViewItem
{
    public LeadCalendarViewItem(IModelViewItem model, Type objectType) : base(objectType, model.Id) { }

    protected override object CreateControlCore()
    {
        var appointments = View.ObjectSpace.GetObjects<Appointment>();

        return new CalendarContentHolder(appointments);
    }
}



