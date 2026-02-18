using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.StateMachine;
using MiniCRM_KenilKhadela.Blazor.Server.Editors.Lead_StatusBar;
using MiniCRM_KenilKhadela.Module.BusinessObjects;
using System.Linq;

namespace MiniCRM_KenilKhadela.Blazor.Server.Controllers
{
    public class LeadStatusBarController : ObjectViewController<DetailView, Lead>
    {
        private Lead.LeadProcessStageEnum? previousStage;

        public LeadStatusBarController()
        {
            TargetObjectType = typeof(Lead);
            TargetViewType = ViewType.DetailView;
        }

        protected override void OnActivated()
        {
            base.OnActivated();

            if (ViewCurrentObject != null)
                previousStage = ViewCurrentObject.LeadProcessStage;

            ObjectSpace.ObjectChanged += ObjectSpace_ObjectChanged;

            var stateMachineController = Frame.GetController<StateMachineController>();
            if (stateMachineController != null)
            {
                var actions = stateMachineController.Actions.OfType<SingleChoiceAction>().ToList();
                foreach (var action in actions)
                {
                    action.Execute += StateMachineAction_Execute;
                }
            }
        }

        private async void StateMachineAction_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            if (Application is BlazorApplication blazorApp)
            {
                await blazorApp.InvokeAsync(() =>
                {
                    RefreshView();
                });
            }
        }

        private void ObjectSpace_ObjectChanged(object sender, ObjectChangedEventArgs e)
        {
            if (e.Object is Lead lead && lead == ViewCurrentObject && e.PropertyName == nameof(Lead.LeadProcessStage))
            {
                if (previousStage != lead.LeadProcessStage)
                {
                    previousStage = lead.LeadProcessStage;
                    RefreshView();
                }
            }
        }

        private void RefreshView()
        {
            var statusBarItem = View?.FindItem("LeadStatusBar") as LeadStatusBarViewItem;
            statusBarItem?.Refresh();

            View?.Refresh();
        }

        protected override void OnDeactivated()
        {
            ObjectSpace.ObjectChanged -= ObjectSpace_ObjectChanged;

            var stateMachineController = Frame.GetController<StateMachineController>();
            if (stateMachineController != null)
            {
                var actions = stateMachineController.Actions.OfType<SingleChoiceAction>().ToList();
                foreach (var action in actions)
                {
                    action.Execute -= StateMachineAction_Execute;
                }
            }
            base.OnDeactivated();
        }
    }
}
