using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Blazor.Editors;
using MiniCRM_KenilKhadela.Module.BusinessObjects;

namespace MiniCRM_KenilKhadela.Blazor.Server.Controllers
{
    public class SwitchFilterActionController : ObjectViewController<ListView, Lead>
    {
        private SimpleAction toggleFilterRowAction;

        public SwitchFilterActionController()
        {
            toggleFilterRowAction = new SimpleAction(this, "ToggleFilterRow", "View")
            {
                ImageName = "Action_Filter",
                PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.CaptionAndImage
            };
            toggleFilterRowAction.Execute += ToggleFilterRowAction_Execute;
        }

        private void ToggleFilterRowAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (View.Editor is DxGridListEditor gridListEditor)
            {
                var gridModel = gridListEditor.GetGridAdapter().GridModel;

                gridModel.ShowFilterRow = !gridModel.ShowFilterRow;
            }
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }
    }
}