using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Base;
using MiniCRM_KenilKhadela.Module.BusinessObjects;
using System;
using System.Linq;

namespace MiniCRM_KenilKhadela.Blazor.Server.Controllers.Activity;
public class ActivityQuickAddController : ViewController<DetailView>
{
    public ActivityQuickAddController()
    {
        
    }

    protected override void OnActivated()
    {
        base.OnActivated();

        if (View.ObjectTypeInfo.Type != typeof(Lead) && View.ObjectTypeInfo.Type != typeof(Contact))
            return;

        var listEditor = GetActivitiesListEditor();
        if (listEditor == null) return;

        if (listEditor.Control != null)
            AssignMasterFrameToNestedController(listEditor);
        else
            listEditor.ControlCreated += OnListEditorControlCreated;

        if (listEditor.ListView?.ObjectSpace != null)
        {
            listEditor.ListView.ObjectSpace.Committed += NestedObjectSpace_Committed;
        }

        ObjectSpace.ObjectReloaded += ObjectSpace_ObjectReloaded;
    }

    private void NestedObjectSpace_Committed(object sender, EventArgs e)
    {
        RefreshActivitiesListView();
    }

    private void OnListEditorControlCreated(object sender, EventArgs e)
    {
        if (sender is ListPropertyEditor lpe)
        {
            lpe.ControlCreated -= OnListEditorControlCreated;
            AssignMasterFrameToNestedController(lpe);
        }
    }

    private void AssignMasterFrameToNestedController(ListPropertyEditor lpe)
    {
        var nestedController = lpe.Frame?.GetController<ActivityNestedNewButtonController>();
        nestedController?.AssignMasterFrame(Frame);
    }

    private void ObjectSpace_ObjectReloaded(object sender, ObjectManipulatingEventArgs e)
    {
        var activity = e.Object as MiniCRM_KenilKhadela.Module.BusinessObjects.Activity;
        if (activity != null)
        {
            bool isCurrentLead = activity.Lead != null && ReferenceEquals(activity.Lead, View.CurrentObject);
            bool isCurrentContact = activity.Contact != null && ReferenceEquals(activity.Contact, View.CurrentObject);

            if (isCurrentLead || isCurrentContact)
            {
                View.Refresh();
                View.RefreshDataSource();
            }
        }
    }

    public void ShowAddActivityPopup(Type objectType)
    {
        var activityOs = Application.CreateObjectSpace(objectType);
        var newActivity = (MiniCRM_KenilKhadela.Module.BusinessObjects.Activity)activityOs.CreateObject(objectType);

        var masterOid = View.ObjectSpace.GetKeyValue(View.CurrentObject);

        if (View.CurrentObject is Lead)
        {
            newActivity.Lead = activityOs.GetObjectByKey<Lead>(masterOid);
        }
        else if (View.CurrentObject is Contact)
        {
            newActivity.Contact = activityOs.GetObjectByKey<Contact>(masterOid);
        }

        var detailView = Application.CreateDetailView(activityOs, newActivity, false);
        detailView.ViewEditMode = ViewEditMode.Edit;

        var showViewParams = new ShowViewParameters(detailView)
        {
            TargetWindow = TargetWindow.NewModalWindow
        };

        var dialogController = Application.CreateController<DialogController>();
        dialogController.SaveOnAccept = false;

        dialogController.AcceptAction.Execute += (s, args) =>
        {
            try
            {
                activityOs.CommitChanges();
                args.ShowViewParameters.TargetWindow = TargetWindow.Current;
            }
            catch (Exception ex)
            {
                Application.ShowViewStrategy.ShowMessage($"Failed to save: {ex.Message}", InformationType.Error);
                return;
            }

            if (Application is BlazorApplication blazorApp)
            {
                blazorApp.InvokeAsync(() =>
                {
                    RefreshActivitiesListViewInternal();
                    View.ObjectSpace.Refresh();
                });
            }
            else
            {
                RefreshActivitiesListViewInternal();
            }
        };

        dialogController.CancelAction.Execute += (_, __) => activityOs.Rollback();

        showViewParams.Controllers.Add(dialogController);
        Application.ShowViewStrategy.ShowView(showViewParams, new ShowViewSource(Frame, null));
    }

    private void RefreshActivitiesListViewInternal()
    {
        View?.ObjectSpace?.Refresh();
        var listEditor = GetActivitiesListEditor();

        if (listEditor?.ListView != null)
        {
            listEditor.ListView.CollectionSource.Reload();
            listEditor.ListView.RefreshDataSource();
        }
    }

    public void RefreshActivitiesListView()
    {
        if (Application is BlazorApplication blazorApp)
            blazorApp.InvokeAsync(RefreshActivitiesListViewInternal);
        else
            RefreshActivitiesListViewInternal();
    }

    private ListPropertyEditor GetActivitiesListEditor()
    {
        return View?.GetItems<ListPropertyEditor>()
            .FirstOrDefault(lpe => lpe.MemberInfo?.Name != null &&
                                   lpe.MemberInfo.Name.IndexOf("activit", StringComparison.OrdinalIgnoreCase) >= 0);
    }

    protected override void OnDeactivated()
    {
        var nestedEditor = GetActivitiesListEditor();
        if (nestedEditor?.ListView?.ObjectSpace != null)
        {
            nestedEditor.ListView.ObjectSpace.Committed -= NestedObjectSpace_Committed;
        }
        ObjectSpace.ObjectReloaded -= ObjectSpace_ObjectReloaded;
        base.OnDeactivated();
    }
}
