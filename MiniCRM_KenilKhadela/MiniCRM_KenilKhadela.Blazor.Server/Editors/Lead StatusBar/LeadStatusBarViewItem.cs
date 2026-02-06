using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using Microsoft.AspNetCore.Components;
using MiniCRM_KenilKhadela.Blazor.Server.Pages.Components;
using MiniCRM_KenilKhadela.Module.BusinessObjects;
using static MiniCRM_KenilKhadela.Module.BusinessObjects.Lead;

namespace MiniCRM_KenilKhadela.Blazor.Server.Editors.Lead_StatusBar
{
    [ViewItem(typeof(IModelViewItem))]
    public class LeadStatusBarViewItem : ViewItem
    {
        public event EventHandler ValueChanged;

        public LeadProcessStageEnum? SelectedStage
        {
            get => (View?.CurrentObject as Lead)?.LeadProcessStage;
            set
            {
                if (View?.CurrentObject is Lead lead && lead.LeadProcessStage != value)
                {
                    lead.LeadProcessStage = value ?? LeadProcessStageEnum.Open;
                    ValueChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public LeadStatusBarViewItem(IModelViewItem model, Type objectType) : base(objectType, model.Id) { }

        protected override object CreateControlCore() => new LeadStatusBarComponentAdapter(this);
        public void TriggerRefresh()
        {
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }
    }


    public class LeadStatusBarComponentAdapter : IComponentContentHolder
    {
        private readonly LeadStatusBarViewItem viewItem;

        public LeadStatusBarComponentAdapter(LeadStatusBarViewItem viewItem)
        {
            this.viewItem = viewItem;
        }

        public RenderFragment ComponentContent => builder =>
        {
            builder.OpenComponent<MiniCRM_KenilKhadela.Blazor.Server.Pages.Components.LeadStatusBar>(0);
            builder.AddAttribute(1, "ViewItem", viewItem);
            builder.CloseComponent();
        };

    }
}
