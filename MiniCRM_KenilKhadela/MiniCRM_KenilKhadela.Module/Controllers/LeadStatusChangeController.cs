using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using MiniCRM_KenilKhadela.Module.BusinessObjects;
using static MiniCRM_KenilKhadela.Module.BusinessObjects.Lead;

namespace MiniCRM_KenilKhadela.Module.Controllers;

// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.ViewController.
public class LeadStatusChangeController : ViewController<DetailView> {
    private SingleChoiceAction changeStatusAction;
    // Use CodeRush to create Controllers and Actions with a few keystrokes.
    // https://docs.devexpress.com/CodeRushForRoslyn/403133/
    public LeadStatusChangeController() {
        TargetViewType = ViewType.DetailView;

        changeStatusAction = new SingleChoiceAction(this, "ChangeStatusAction","MyCustomActionContainer")
        {
            ItemType = SingleChoiceActionItemType.ItemIsOperation,
            SelectionDependencyType = SelectionDependencyType.RequireSingleObject
        };
        changeStatusAction.Items.Add(new ChoiceActionItem("Open", Lead.LeadStatusEnum.Open));
        changeStatusAction.Items.Add(new ChoiceActionItem("Qaulified", Lead.LeadStatusEnum.Qualified));
        changeStatusAction.Items.Add(new ChoiceActionItem("Disqualified", Lead.LeadStatusEnum.DisQualified));

        changeStatusAction.Execute += ChangeStatusAction_Execute;
    }

    private void ChangeStatusAction_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
    {
        Lead currentLead = (Lead)View.CurrentObject;
        if(currentLead!=null && e.SelectedChoiceActionItem != null)
        {
            LeadStatusEnum newStatus = (LeadStatusEnum)e.SelectedChoiceActionItem.Data;

            currentLead.LeadStatus = newStatus;

            ObjectSpace.CommitChanges();
            View.Refresh();
        } 

    }

    protected override void OnActivated() {
        base.OnActivated();
        // Perform various tasks depending on the target View,
        // customize view items: https://docs.devexpress.com/eXpressAppFramework/120092.
    }
    protected override void OnViewControlsCreated() {
        base.OnViewControlsCreated();
        // Access and customize the target View control.
    }
    protected override void OnDeactivated() {
        // Unsubscribe from previously subscribed events and release other references and resources.
        base.OnDeactivated();
    }
}
