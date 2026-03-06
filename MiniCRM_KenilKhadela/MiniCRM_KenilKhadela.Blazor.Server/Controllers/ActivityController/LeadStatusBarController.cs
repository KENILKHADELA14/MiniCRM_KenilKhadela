using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.StateMachine;
using MiniCRM_KenilKhadela.Blazor.Server.Editors.Lead_StatusBar;
using MiniCRM_KenilKhadela.Module.BusinessObjects;
using System;
using System.Linq;

namespace MiniCRM_KenilKhadela.Blazor.Server.Controllers
{
    public class LeadStatusBarController : ViewController<DetailView>
    {
        private Lead.LeadProcessStageEnum? previousStage;
        private Lead currentLead;

        public LeadStatusBarController()
        {
            TargetViewType = ViewType.DetailView;
        }

        protected override void OnActivated()
        {
            base.OnActivated();

            if (View.CurrentObject is Lead lead)
            {
                currentLead = lead;
            }
            //else if (View.CurrentObject is Opportunities opportunity)
            //{
            //    currentLead = opportunity.Lead;
            //}

            if (currentLead != null)
            {
                previousStage = currentLead.LeadProcessStage;
            }

            if (ObjectSpace != null)
                ObjectSpace.ObjectChanged += ObjectSpace_ObjectChanged;

            var stateMachineController = Frame?.GetController<StateMachineController>();
            if (stateMachineController != null)
            {
                var actions = stateMachineController.Actions
                    .OfType<SingleChoiceAction>()
                    .ToList();

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
            else
            {
                RefreshView();
            }
        }

        private void ObjectSpace_ObjectChanged(object sender, ObjectChangedEventArgs e)
        {
            if (currentLead == null)
                return;

            if (e.Object == currentLead &&
                e.PropertyName == nameof(Lead.LeadProcessStage))
            {
                if (previousStage != currentLead.LeadProcessStage)
                {
                    previousStage = currentLead.LeadProcessStage;
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
            if (ObjectSpace != null)
                ObjectSpace.ObjectChanged -= ObjectSpace_ObjectChanged;

            var stateMachineController = Frame?.GetController<StateMachineController>();
            if (stateMachineController != null)
            {
                var actions = stateMachineController.Actions
                    .OfType<SingleChoiceAction>()
                    .ToList();

                foreach (var action in actions)
                {
                    action.Execute -= StateMachineAction_Execute;
                }
            }

            base.OnDeactivated();
        }
    }
}
