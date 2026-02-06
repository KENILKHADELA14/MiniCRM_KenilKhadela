using System;
using System.Collections.Generic;
using System.Drawing.Text;
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
using DevExpress.PivotGrid.Criteria.Validation;
using MiniCRM_KenilKhadela.Module.BusinessObjects;

namespace MiniCRM_KenilKhadela.Blazor.Server.Controllers.Activity;

// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.ViewController.
public class ActivityDropdownPopUpController : ObjectViewController<ListView, MiniCRM_KenilKhadela.Module.BusinessObjects.Activity> {
    // Use CodeRush to create Controllers and Actions with a few keystrokes.
    // https://docs.devexpress.com/CodeRushForRoslyn/403133/
    private SingleChoiceAction addActivityAction;
    public ActivityDropdownPopUpController() {
        // Target required Views (via the TargetXXX properties) and create their Actions.
        // TargetViewType = ViewType.ListView;
        // SimpleAction simpleAction1 = new SimpleAction(this, "SimpleAction1", PredefinedCategory.View);
        // simpleAction1.Execute += (s, e) {
        //    // Implement business logic: https://docs.devexpress.com/eXpressAppFramework/113711.
        // }
        addActivityAction = new SingleChoiceAction(this, "AddActivityDropdown", PredefinedCategory.Edit)
        {
            Caption = "Add",
            ImageName = "Action_New",
            ItemType = SingleChoiceActionItemType.ItemIsOperation,
            PaintStyle = ActionItemPaintStyle.CaptionAndImage
        };

        addActivityAction.Items.Add(new ChoiceActionItem("ActivityPhone", "Activity Phone", typeof(ActivityPhone)));
        addActivityAction.Items.Add(new ChoiceActionItem("Appointment", "Appointment", typeof(Appointment)));

        addActivityAction.Execute += AddActivityAction_Execute;

    }
    protected override void OnActivated() {
        base.OnActivated();
        var newController=Frame.GetController<NewObjectViewController>();
        if (newController != null)
        {
            newController.Active["DisableDefaultNew"] = false;
            //CreateDropdownAction();

        }
        // Perform various tasks depending on the target View,
        // customize view items: https://docs.devexpress.com/eXpressAppFramework/120092.
    }

    private void CreateDropdownAction()
    {
        addActivityAction = new SingleChoiceAction(this, "AddActivityDropdown", PredefinedCategory.Edit)
        {
            Caption = "Add",
            ImageName = "Action_New",
            ItemType = SingleChoiceActionItemType.ItemIsOperation,
            PaintStyle = ActionItemPaintStyle.CaptionAndImage
        };

        addActivityAction.Items.Add(new ChoiceActionItem("ActivityPhone","Activity Phone",typeof(ActivityPhone)));
        addActivityAction.Items.Add(new ChoiceActionItem("Appointment","Appointment",typeof(Appointment)));

        addActivityAction.Execute += AddActivityAction_Execute;
    }

    private void AddActivityAction_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
    {
        var objectType =(Type)e.SelectedChoiceActionItem.Data;

        var os= Application.CreateObjectSpace(objectType);

        var obj=os.CreateObject(objectType);

        var dv=Application.CreateDetailView(os, obj);

        dv.ViewEditMode = ViewEditMode.Edit;

        var svp = new ShowViewParameters(dv)
        {
            TargetWindow = TargetWindow.NewModalWindow
        };

        var dialogController = Application.CreateController<DialogController>();
        dialogController.SaveOnAccept = true;

        svp.Controllers.Add(dialogController);

        Application.ShowViewStrategy.ShowView(svp, new ShowViewSource(Frame, null));
    }

    protected override void OnViewControlsCreated() {
        base.OnViewControlsCreated();
        // Access and customize the target View control.
    }
    protected override void OnDeactivated() {
        if(addActivityAction != null)
        {
            addActivityAction.Execute -= AddActivityAction_Execute;
        }
        // Unsubscribe from previously subscribed events and release other references and resources.
        base.OnDeactivated();
    }
}
