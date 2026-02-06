using DevExpress.ExpressApp;
using MiniCRM_KenilKhadela.Blazor.Server.Editors.Lead_StatusBar;
using MiniCRM_KenilKhadela.Module.BusinessObjects;

namespace MiniCRM_KenilKhadela.Blazor.Server.Controllers.Activity;

// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.ViewController.
public class LeadStatusBarController : ObjectViewController<DetailView,Lead> {
    private LeadStatusBarViewItem statusBarViewItem;
    // Use CodeRush to create Controllers and Actions with a few keystrokes.
    // https://docs.devexpress.com/CodeRushForRoslyn/403133/
    public LeadStatusBarController() {
    }
    protected override void OnActivated() {
        base.OnActivated();
        statusBarViewItem=View.FindItem("LeadStatusBar") as  LeadStatusBarViewItem;  
    }
    protected override void OnViewControlsCreated() {
        base.OnViewControlsCreated();
    }
    protected override void OnDeactivated() {
        statusBarViewItem=null;
        base.OnDeactivated();

    }
}
