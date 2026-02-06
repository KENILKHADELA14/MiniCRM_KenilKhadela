using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using MiniCRM_KenilKhadela.Blazor.Server.Editors.Lead_StatusBar;
using MiniCRM_KenilKhadela.Module.BusinessObjects;
using static MiniCRM_KenilKhadela.Module.BusinessObjects.Lead;

namespace MiniCRM_KenilKhadela.Blazor.Server.Controllers
{
    public class LeadStateMachineController
        : ObjectViewController<DetailView, Lead>
    {
        private SingleChoiceAction stateMachineAction;
        private LeadStatusBarViewItem statusBarViewItem;

        public LeadStateMachineController()
        {
            stateMachineAction = new SingleChoiceAction(
                this,
                "LeadStateMachine",
                DevExpress.Persistent.Base.PredefinedCategory.Edit)
            {
                Caption = "State Machine",
                ItemType = SingleChoiceActionItemType.ItemIsOperation
            };

            stateMachineAction.Execute += StateMachineAction_Execute;
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            statusBarViewItem = View.FindItem("LeadStatusBar") as LeadStatusBarViewItem;

            if (statusBarViewItem != null)
                statusBarViewItem.ValueChanged += (s, e) => { UpdateItems(); View.Refresh(); };

            UpdateItems();
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            UpdateItems();
        }

        protected override void OnDeactivated()
        {
            statusBarViewItem = null;
            base.OnDeactivated();
        }

        private void UpdateItems()
        {
            stateMachineAction.Items.Clear();
            var lead = View.CurrentObject as Lead;
            if (lead == null) return;


            if (lead.LeadProcessStage < LeadProcessStageEnum.Closed)
            {
                stateMachineAction.Items.Add(new ChoiceActionItem("Next Stage", "Next"));
            }

            if (lead.LeadProcessStage > LeadProcessStageEnum.Open)
            {
                stateMachineAction.Items.Add(new ChoiceActionItem("Previous Stage", "Previous"));
            }
        }

        private void StateMachineAction_Execute(
            object sender,
            SingleChoiceActionExecuteEventArgs e)
        {
            var lead = View.CurrentObject as Lead;
            if (lead == null) return;

            if ((string)e.SelectedChoiceActionItem.Data == "Next")
            {
                lead.LeadProcessStage++;
            }
            else
            {
                lead.LeadProcessStage--;
            }

            lead.LeadStatus = lead.LeadProcessStage == LeadProcessStageEnum.Open
                ? LeadStatusEnum.Open
                : LeadStatusEnum.Qualified;

            statusBarViewItem?.TriggerRefresh();

            ObjectSpace.CommitChanges();
            UpdateItems();
        }
    }
}
