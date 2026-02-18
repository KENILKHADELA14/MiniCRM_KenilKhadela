//using DevExpress.ExpressApp;
//using DevExpress.ExpressApp.Actions;
//using MiniCRM_KenilKhadela.Module.BusinessObjects;

//namespace MiniCRM_KenilKhadela.Module.Controllers
//{
//    public class LeadAppointmentController : ObjectViewController<DetailView, Lead>
//    {
//        public LeadAppointmentController()
//        {
//            SimpleAction createAppointment = new SimpleAction(this, "CreateAppointment", "View")
//            {
//                Caption = "Create Appointment",
//                ImageName = "BO_Scheduler"
//            };
//            createAppointment.Execute += CreateAppointment_Execute;
//        }

//        private void CreateAppointment_Execute(object sender, SimpleActionExecuteEventArgs e)
//        {
//            var lead = ViewCurrentObject as Lead;
//            if (lead != null)
//            {
//                var appointment = ObjectSpace.CreateObject<Appointment>();
//                appointment.Subject = $"Meeting regarding {lead.Subject}";
//                appointment.Lead = lead;
//                appointment.StartDate = DateTime.Now;
//                appointment.EndDate = DateTime.Now.AddHours(1);

//                ObjectSpace.CommitChanges();

//                ObjectSpace.Refresh();
//            }
//        }
//    }
//}

