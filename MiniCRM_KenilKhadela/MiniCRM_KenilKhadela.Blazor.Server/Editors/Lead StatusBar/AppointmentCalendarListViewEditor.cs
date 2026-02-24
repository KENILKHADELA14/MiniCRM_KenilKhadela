using System.Collections;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Blazor;
using Microsoft.AspNetCore.Components;
using MiniCRM_KenilKhadela.Blazor.Server.Pages.Components;
using MiniCRM_KenilKhadela.Module.BusinessObjects;

namespace MiniCRM_KenilKhadela.Blazor.Server.Editors;

[ListEditor(typeof(Appointment))]
public class AppointmentCalendarListEditor : ListEditor, IComponentContentHolder, IComplexListEditor
{
    private RenderFragment componentContent;
    private CollectionSourceBase collectionSource;
    private XafApplication application;

    public AppointmentCalendarListEditor(IModelListView model) : base(model) { }

    public void Setup(CollectionSourceBase collectionSource, XafApplication application)
    {
        this.collectionSource = collectionSource;
        this.application = application;
    }
    
    protected override object CreateControlsCore()
    {
        componentContent = CreateComponentContent();
        return this;
    }

    protected override void AssignDataSourceToControl(object dataSource)
    {
        componentContent = CreateComponentContent();
    }

    public override void Refresh()
    {
        componentContent = CreateComponentContent();
    }

    private RenderFragment CreateComponentContent()
    {
        return builder =>
        {
            var appointments = new List<Appointment>();

            if (DataSource is IEnumerable collection)
            {
                foreach (var item in collection)
                {
                    if (item is Appointment apt)
                    {
                        appointments.Add(apt);
                    }
                }
            }

            IObjectSpace objectSpace = collectionSource?.ObjectSpace;

            builder.OpenComponent<LeadCalendar>(0);
            builder.AddAttribute(1, nameof(LeadCalendar.Appointments), appointments);
            builder.AddAttribute(2, nameof(LeadCalendar.ObjectSpace), objectSpace);
            builder.CloseComponent();
        };
    }

    RenderFragment IComponentContentHolder.ComponentContent => componentContent;
    public override SelectionType SelectionType => SelectionType.None;
    public override IList GetSelectedObjects() => Array.Empty<object>();
}
