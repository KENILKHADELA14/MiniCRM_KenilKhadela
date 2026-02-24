using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using Microsoft.AspNetCore.Components;
using MiniCRM_KenilKhadela.Blazor.Server.Pages.Components;
using MiniCRM_KenilKhadela.Module.BusinessObjects;

namespace MiniCRM_KenilKhadela.Blazor.Server.Editors.Lead_StatusBar
{
    [ViewItem(typeof(IModelViewItem))]
    public class LeadStatusBarViewItem : ViewItem, IComponentContentHolder
    {
        public LeadStatusBar ComponentInstance { get; private set; }

        public LeadStatusBarViewItem(IModelViewItem model, Type objectType) : base(objectType, model.Id) { }

        protected override object CreateControlCore() => this;

        public override void Refresh()
        {
            base.Refresh();
            ComponentInstance?.RefreshUI();
        }   

        RenderFragment IComponentContentHolder.ComponentContent => builder =>
        {

            var currentObject = View?.CurrentObject;

            if (currentObject is Lead || currentObject is Opportunities)
            {
                builder.OpenComponent<LeadStatusBar>(0);
                builder.AddAttribute(1, nameof(LeadStatusBar.ViewItem), this);

                builder.AddComponentReferenceCapture(2, inst => ComponentInstance = (LeadStatusBar)inst);

                builder.CloseComponent();
            }
        };
    }
}