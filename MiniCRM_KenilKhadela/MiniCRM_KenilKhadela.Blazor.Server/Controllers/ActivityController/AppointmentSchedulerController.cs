using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Scheduler.Blazor.Editors;
using MiniCRM_KenilKhadela.Module.BusinessObjects;
using System;

namespace MiniCRM_KenilKhadela.Blazor.Server.Controllers.Activity
{
    public class AppointmentSchedulerController
        : ObjectViewController<ListView, Appointment>
    {
        private SimpleAction newAppointmentAction;

        public AppointmentSchedulerController()
        {
            newAppointmentAction = new SimpleAction(
                this,
                "NewSchedulerAppointment",
                DevExpress.Persistent.Base.PredefinedCategory.Edit)
            {
                Caption = "New Appointment",
                ImageName = "BO_Appointment",
                SelectionDependencyType = SelectionDependencyType.Independent
            };

            newAppointmentAction.Execute += NewAppointmentAction_Execute;
        }

        protected override void OnActivated()
        {
            base.OnActivated();

            if (View.Editor is not SchedulerListEditor)
            {
                newAppointmentAction.Active["NotScheduler"] = false;
            }
        }

        private void NewAppointmentAction_Execute(
            object sender,
            SimpleActionExecuteEventArgs e)
        {
            IObjectSpace os = Application.CreateObjectSpace(typeof(Appointment));

            var appointment = os.CreateObject<Appointment>();

            appointment.StartDate = DateTime.Now;
            appointment.EndDate = DateTime.Now.AddHours(1);
            appointment.Subject = "New Appointment";

            if (Frame.View is DetailView dv &&
                dv.CurrentObject is Lead lead)
            {
                appointment.Lead = os.GetObject(lead);
            }

            DetailView detailView =
                Application.CreateDetailView(os, appointment);

            detailView.ViewEditMode = ViewEditMode.Edit;

            Application.ShowViewStrategy
                .ShowViewInPopupWindow(detailView);
        }
    }
}
