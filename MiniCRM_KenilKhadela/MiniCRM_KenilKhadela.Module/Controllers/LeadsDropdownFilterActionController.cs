using DevExpress.Data.Filtering;
using DevExpress.Export;
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
using DevExpress.XtraPrinting;
using MiniCRM_KenilKhadela.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static MiniCRM_KenilKhadela.Module.BusinessObjects.Lead;

namespace MiniCRM_KenilKhadela.Module.Controllers;

// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.ViewController.
public class LeadsDropdownFilterActionController : ViewController<ListView> {
    // Use CodeRush to create Controllers and Actions with a few keystrokes.
    // https://docs.devexpress.com/CodeRushForRoslyn/403133/
    private SingleChoiceAction leadFilterAction;
    public LeadsDropdownFilterActionController() {
        // Target required Views (via the TargetXXX properties) and create their Actions.
        // TargetViewType = ViewType.ListView;
        // SimpleAction simpleAction1 = new SimpleAction(this, "SimpleAction1", PredefinedCategory.View);
        // simpleAction1.Execute += (s, e) {
        //    // Implement business logic: https://docs.devexpress.com/eXpressAppFramework/113711.
        // }

        TargetObjectType = typeof(Lead);
        TargetViewType = ViewType.ListView;

        leadFilterAction = new SingleChoiceAction(
            this,"LeadFilterAction",PredefinedCategory.Filters
            )
        {
            ItemType=SingleChoiceActionItemType.ItemIsOperation,
            ShowItemsOnClick=true
        };

        leadFilterAction.ItemType = SingleChoiceActionItemType.ItemIsOperation;
        leadFilterAction.Items.Add(new ChoiceActionItem("My Open Leads", "My Open Leads"));
        leadFilterAction.Items.Add(new ChoiceActionItem("Closed Leads","Closed Leads"));
        leadFilterAction.Items.Add(new ChoiceActionItem("All Leads", "All Leads"));
        leadFilterAction.Items.Add(new ChoiceActionItem("Open Leads","Open Leads"));

        leadFilterAction.SelectedItem = leadFilterAction.Items[0];
        
        leadFilterAction.Execute += LeadFilterAction_Execute;
    }


    private void LeadFilterAction_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
    {
        var cs = View.CollectionSource;

        if (cs != null)
        {
            cs.Criteria.Clear();

            switch (e.SelectedChoiceActionItem.Data as string)
            {
                case "My Open Leads":
                    cs.Criteria["LeadFilter"] = CriteriaOperator.Parse("LeadStatus =? AND  Owner.Oid=?", LeadStatusEnum.Qualified, SecuritySystem.CurrentUserId);
                    break;

                case "Open Leads":
                    cs.Criteria["LeadFilter"] = CriteriaOperator.Parse("LeadStatus =?", LeadStatusEnum.Open);
                    break;


                case "Closed Leads":
                    cs.Criteria["LeadFilter"] = CriteriaOperator.Parse("LeadStatus =?", LeadStatusEnum.DisQualified);
                    break;

                case "All Leads":
                    cs.Criteria["LeadFilter"] = null;
                    break;
            }
            leadFilterAction.Caption = e.SelectedChoiceActionItem.Caption;
            leadFilterAction.SelectedItem = e.SelectedChoiceActionItem;
            cs.Reload();
        }
    }

    public void ResetToAllLeads()
    {
        var allLeadsItem = leadFilterAction.Items.FirstOrDefault(i => (string)i.Data == "All Leads");

        if (allLeadsItem != null)
        {
            View.CollectionSource.Criteria.Clear();
            leadFilterAction.SelectedItem = allLeadsItem;
            leadFilterAction.Caption = allLeadsItem.Caption;

            View.CollectionSource.Reload();
        }
    }

    protected override void OnActivated() {
        base.OnActivated();

        var allLeadsItem=leadFilterAction.Items.FirstOrDefault(i=>(string)i.Data=="All Leads");

        if (allLeadsItem != null)
        {
            leadFilterAction.SelectedItem = allLeadsItem;
            leadFilterAction.Caption = allLeadsItem.Caption;

            View.CollectionSource.Criteria.Clear();
            View.CollectionSource.Reload();
        }
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
