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

namespace MiniCRM_KenilKhadela.Module.Controllers;

// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.ViewController.
public class ResetViewSettingsActionController : ViewController<ListView> {
    // Use CodeRush to create Controllers and Actions with a few keystrokes.
    // https://docs.devexpress.com/CodeRushForRoslyn/403133/
    private SimpleAction resetViewSettingsAction;
    public ResetViewSettingsActionController() {
        TargetObjectType = typeof(Lead);
        TargetViewType = ViewType.ListView;

        resetViewSettingsAction = new SimpleAction(
           this, "ResetViewSettingsAction", PredefinedCategory.View
            )
        {
            Caption = "Reset View Settings",
            ConfirmationMessage = "Are you sure you want to reset the current view's settings to default",
            ImageName = "Action_Reset"
        };

        resetViewSettingsAction.Execute += ResetViewSettingsAction_Execute; ;

    }

    private void ResetViewSettingsAction_Execute(object sender, SimpleActionExecuteEventArgs e)
    {
        //if (View != null && View is ListView listView)
        //{
        //    listView.CollectionSource.Criteria.Clear();
        //    var leadFilterController = Frame.GetController<FilterActionController>();
        //    if (leadFilterController != null)
        //    {
        //        leadFilterController.ResetToAllLeads();
        //    }
        //    View.LoadModel();
        //    View.RefreshDataSource();
        //}
        var resetController=Frame.GetController<ResetViewSettingsController>();
        if (resetController != null)
        {
            resetController.ResetViewSettingsAction.DoExecute();
        }
    }

    protected override void OnActivated() {
        base.OnActivated();
        
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
