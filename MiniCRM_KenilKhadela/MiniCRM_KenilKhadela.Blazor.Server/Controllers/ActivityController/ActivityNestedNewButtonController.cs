using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using MiniCRM_KenilKhadela.Module.BusinessObjects;
using System;

namespace MiniCRM_KenilKhadela.Blazor.Server.Controllers.Activity;

public class ActivityNestedNewButtonController : ObjectViewController<ListView, MiniCRM_KenilKhadela.Module.BusinessObjects.Activity>
{
    private SingleChoiceAction _addDropdown;
    private Frame _masterFrame;

    public ActivityNestedNewButtonController()
    {
        TargetViewNesting = Nesting.Nested;

        _addDropdown = new SingleChoiceAction(this, "NestedActivityAdd", DevExpress.Persistent.Base.PredefinedCategory.Edit)
        {
            Caption = "New",
            ImageName = "Action_New",
            ItemType = SingleChoiceActionItemType.ItemIsOperation,
            PaintStyle = ActionItemPaintStyle.CaptionAndImage
        };

        _addDropdown.Items.Add(new ChoiceActionItem("ActivityPhone", "Phone Call", typeof(ActivityPhone)));
        _addDropdown.Items.Add(new ChoiceActionItem("Appointment", "Appointment", typeof(Appointment)));

        _addDropdown.Execute += OnNestedAddExecute;
    }

    public void AssignMasterFrame(Frame masterFrame)
    {
        _masterFrame = masterFrame;
    }

    protected override void OnActivated()
    {
        base.OnActivated();

        var newObjController = Frame.GetController<NewObjectViewController>();
        if (newObjController != null)
            newObjController.NewObjectAction.Active["OverriddenByQuickAdd"] = false;
    }

    private void OnNestedAddExecute(object sender, SingleChoiceActionExecuteEventArgs e)
    {
        var objectType = (Type)e.SelectedChoiceActionItem.Data;
        var parentController = _masterFrame?.GetController<ActivityQuickAddController>();
        parentController?.ShowAddActivityPopup(objectType);
        if(Application is BlazorApplication blazorApp)
        {
            blazorApp.InvokeAsync(() =>
            {
                _masterFrame?.View?.ObjectSpace?.Refresh();
            });
        }
    }

    protected override void OnDeactivated()
    {
        var newObjController = Frame?.GetController<NewObjectViewController>();
        if (newObjController != null)
            newObjController.NewObjectAction.Active.RemoveItem("OverriddenByQuickAdd");

        base.OnDeactivated();
    }
}