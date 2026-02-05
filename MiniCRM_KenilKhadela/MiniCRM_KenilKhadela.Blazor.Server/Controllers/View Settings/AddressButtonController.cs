//using System;
//using DevExpress.ExpressApp;
//using DevExpress.ExpressApp.Editors;
//using DevExpress.ExpressApp.Blazor.Editors; 
//using MiniCRM_KenilKhadela.Module.BusinessObjects;

//namespace MiniCRM_KenilKhadela.Module.Blazor.Controllers
//{
//    public class BlazorAddressNewButtonController : ObjectViewController<DetailView, Lead>
//    {
//        protected override void OnActivated()
//        {
//            base.OnActivated();
//            View.ControlsCreated += ViewController_ControlsCreated;
//            if (View?.ObjectSpace != null)
//            {
//                View.ObjectSpace.ObjectChanged += ObjectSpace_ObjectChanged;
//            }
//            UpdateNewButtonVisibility();
//        }

//        private void ViewController_ControlsCreated(object sender, EventArgs e)
//        {
//            UpdateNewButtonVisibility();
//        }

//        private void ObjectSpace_ObjectChanged(object sender, ObjectChangedEventArgs e)
//        {
//            //if (e.Object is Lead && e.PropertyName == nameof(Lead.Address1))
//            //{
//            //    UpdateNewButtonVisibility();
//            //}
//            UpdateNewButtonVisibility();
//        }

//        private void UpdateNewButtonVisibility()
//        {
//            var lead = View?.CurrentObject as Lead;
//            if (lead == null) return;

//            View.CustomizeViewItemControl<LookupPropertyEditor>(this, editor => {
//                if (!string.Equals(editor.PropertyName, nameof(Lead.Address2)))
//                    return;

//                bool hasAddress1 = lead.Address2 != null;
//                if (hasAddress1)
//                {
//                    editor.HideNewButton(); 
//                }
//                else
//                {
//                    editor.ResetNewButtonVisibility(); 
//                }
//            });

//            View.CustomizeViewItemControl<LookupPropertyEditor>(this, editor => {
//                if (!string.Equals(editor.PropertyName, nameof(Lead.Address1)))
//                    return;

//                bool hasAddress1 = lead.Address1 != null;
//                if (hasAddress1)
//                {
//                    editor.HideNewButton(); 
//                }
//                else
//                {
//                    editor.ResetNewButtonVisibility();
//                }
//            });
//        }

//        protected override void OnDeactivated()
//        {
//            View.ControlsCreated -= ViewController_ControlsCreated;
//            if (View?.ObjectSpace != null)
//            {
//                View.ObjectSpace.ObjectChanged -= ObjectSpace_ObjectChanged;
//            }
//            base.OnDeactivated();
//        }
//    }
//}