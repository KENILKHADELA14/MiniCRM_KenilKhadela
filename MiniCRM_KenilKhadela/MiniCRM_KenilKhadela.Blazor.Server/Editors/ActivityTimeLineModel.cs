using DevExpress.ExpressApp.Blazor.Components.Models;
using MiniCRM_KenilKhadela.Module.BusinessObjects;
using System;
using System.Collections.Generic;

namespace MiniCRM_KenilKhadela.Blazor.Server.Editors
{
    public class ActivityTimelineModel : ComponentModelBase
    {
        public IEnumerable<Activity> Activities
        {
            get => GetPropertyValue<IEnumerable<Activity>>();
            set => SetPropertyValue(value);
        }

        public List<Activity> SelectedActivities
        {
            get => GetPropertyValue<List<Activity>>();
            set => SetPropertyValue(value);
        }

        public Action<Activity> OnActivityClick { get; set; }
        public Action<Activity> OnActivitySelect { get; set; }

        public override Type ComponentType => typeof(Pages.Components.ActivityTimeLineListView);
    }
}
