using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.SystemModule;
using Microsoft.AspNetCore.Components;
using MiniCRM_KenilKhadela.Blazor.Server.Pages.Components;
using MiniCRM_KenilKhadela.Module.BusinessObjects;

namespace MiniCRM_KenilKhadela.Blazor.Server.Editors
{
    [ListEditor(typeof(Activity))]
    public class ActivityTimelineListEditor : ListEditor, IComponentContentHolder, IComplexListEditor
    {
        private RenderFragment componentContent;
        private CollectionSourceBase collectionSource;
        private XafApplication application;
        private readonly List<Activity> selectedActivities = new();

        private readonly ActivityListState _state = new();

        public ActivityTimelineListEditor(IModelListView model) : base(model) { }

        public void Setup(CollectionSourceBase collectionSource, XafApplication application)
        {
            this.collectionSource = collectionSource;
            this.application = application;

            if(this.collectionSource != null)
            {
                collectionSource.ObjectSpace.ObjectChanged += ObjectSpace_ObjectChanged;
                collectionSource.ObjectSpace.Committed += ObjectSpace_Committed;
            }
            this.collectionSource.CollectionChanged += OnCollectionSourceChanged;
        }

        private void OnCollectionSourceChanged(object sender, EventArgs e)
        {
                PushCurrentData();
        }

        private void ObjectSpace_Committed(object sender, EventArgs e)
        {
                PushCurrentData();
        }

        private void ObjectSpace_ObjectChanged(object sender, ObjectChangedEventArgs e)
        {
            if (e.Object is Activity)
            {
                PushCurrentData();
            }
        }

        public override void BreakLinksToControls()
        {
            if (collectionSource != null)
            {
                collectionSource.CollectionChanged-= OnCollectionSourceChanged;
                if (collectionSource?.ObjectSpace != null)
                {
                    collectionSource.ObjectSpace.ObjectChanged -= ObjectSpace_ObjectChanged;
                    collectionSource.ObjectSpace.Committed -= ObjectSpace_Committed;
                }
            }
            base.BreakLinksToControls();
        }

        protected override object CreateControlsCore()
        {
            componentContent = builder =>
            {
                builder.OpenComponent<ActivityTimeLineListView>(0);
                builder.AddAttribute(1, nameof(ActivityTimeLineListView.State), _state);
                builder.AddAttribute(2, nameof(ActivityTimeLineListView.SelectedActivities), selectedActivities);
                builder.AddAttribute(3, nameof(ActivityTimeLineListView.OnActivityClick), (Action<Activity>)OnActivityClick);
                builder.AddAttribute(4, nameof(ActivityTimeLineListView.OnActivitySelect), (Action<Activity, bool>)OnActivitySelect);
                builder.CloseComponent();
            };

            PushCurrentData();

            return this;
        }

        protected override void AssignDataSourceToControl(object dataSource)
        {
            PushCurrentData();
        }

        public override void Refresh()
        {
            PushCurrentData();
        }

        private void PushCurrentData()
        {
            var activities = new List<Activity>();

            if (DataSource is IEnumerable collection)
            {
                foreach (var item in collection)
                {
                    if (item is Activity activity)
                        activities.Add(activity);
                }
            }

            _state.Update(activities);
        }

        private void OnActivityClick(Activity activity)
        {
            if (activity == null) return;

            var os = application.CreateObjectSpace(typeof(Activity));
            var obj = os.GetObjectByKey<Activity>(activity.Oid);

            var detailView = application.CreateDetailView(os, obj, true);

            detailView.ViewEditMode = ViewEditMode.Edit;

            var svp = new ShowViewParameters(detailView){

                TargetWindow = TargetWindow.NewModalWindow
            }
            ;
            var dialogController = application.CreateController<DialogController>();
            dialogController.SaveOnAccept = false;

            dialogController.AcceptAction.Execute += (_, __) =>
            {
                os.CommitChanges();
                collectionSource.Reload();
                PushCurrentData();
                Refresh();
            };

            svp.Controllers.Add(dialogController);

            application.ShowViewStrategy.ShowView(svp, new ShowViewSource(null, null));
            selectedActivities.Clear();
            selectedActivities.Add(activity);
            OnSelectionChanged();
        }

        private void OnActivitySelect(Activity activity, bool isSelected)
        {
            if (activity == null) return;

            if (isSelected)
            {
                if (!selectedActivities.Contains(activity))
                    selectedActivities.Add(activity);
            }
            else
            {
                selectedActivities.Remove(activity);
            }

            OnSelectionChanged();
        }

        RenderFragment IComponentContentHolder.ComponentContent => componentContent;

        public override IList GetSelectedObjects() =>
            new System.Collections.ArrayList(selectedActivities.Cast<object>().ToArray());

        public override SelectionType SelectionType => SelectionType.Full;
    }
}
