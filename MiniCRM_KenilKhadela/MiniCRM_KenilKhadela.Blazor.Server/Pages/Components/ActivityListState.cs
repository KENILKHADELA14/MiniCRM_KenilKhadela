using MiniCRM_KenilKhadela.Module.BusinessObjects;

namespace MiniCRM_KenilKhadela.Blazor.Server.Pages.Components
{
    public class ActivityListState
    {
        public List<Activity> Activities { get; private set; } = new();

        public event Action OnChanged;

        public void Update(List<Activity> activities)
        {
            Activities = activities ?? new List<Activity>();
            OnChanged?.Invoke();
        }
    }
}
