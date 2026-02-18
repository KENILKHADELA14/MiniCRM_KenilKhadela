using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Xpo;
using Microsoft.Extensions.DependencyInjection;
using MiniCRM_KenilKhadela.Module.BusinessObjects;

namespace MiniCRM_KenilKhadela.Module.DatabaseUpdate
{
    // For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Updating.ModuleUpdater
    public class Updater : ModuleUpdater
    {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
            base(objectSpace, currentDBVersion)
        {
        }
        public override void UpdateDatabaseAfterUpdateSchema()
        {
            base.UpdateDatabaseAfterUpdateSchema();
            var adminRole = ObjectSpace.FirstOrDefault<PermissionPolicyRole>(r => r.Name == "Administrators");
            var userRole = ObjectSpace.FirstOrDefault<PermissionPolicyRole>(r => r.Name == "Users");
            if (userRole == null)
            {
                userRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                userRole.Name = "Users";
            }
            if (adminRole == null)
            {
                adminRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                adminRole.Name = "Administrators";
                adminRole.IsAdministrative = true;
            }
            if (adminRole != null)
            {
                adminRole.AddTypePermission<DashboardData>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
            }
            var users = ObjectSpace.FirstOrDefault<PermissionPolicyUser>(o => o.UserName == "Kenil Khadela");
            if (users == null)
            {
                users = ObjectSpace.CreateObject<PermissionPolicyUser>();
                users.UserName = "Kenil Khadela";
                users.SetPassword("1234");
                users.Roles.Add(userRole);
            }
            userRole.PermissionPolicy = SecurityPermissionPolicy.AllowAllByDefault;
            userRole.AddTypePermission<DashboardData>(SecurityOperations.Read, SecurityPermissionState.Allow);
            userRole.AddTypePermission<DashboardData>(SecurityOperations.Write, SecurityPermissionState.Deny);
            userRole.AddTypePermission<DashboardData>(SecurityOperations.Delete, SecurityPermissionState.Deny);
            userRole.AddTypePermission<DashboardData>(SecurityOperations.Create, SecurityPermissionState.Deny);

            userRole.AddTypePermission<Lead>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
            userRole.AddTypePermission<Contact>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
            userRole.AddTypePermission<Appointment>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
            userRole.AddTypePermission<Activity>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
            userRole.AddTypePermission<LeadStatusStateMachine>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
            userRole.AddTypePermission<Country>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
            userRole.AddTypePermission<Accounts>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
            userRole.AddTypePermission<Address>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
            userRole.AddTypePermission<ChildrenAccounts>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
            userRole.AddTypePermission<TransactionCurrency>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
            userRole.AddTypePermission<ActivityPhone>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
            userRole.AddTypePermission<Opportunities>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
            var adminUser = ObjectSpace.FirstOrDefault<PermissionPolicyUser>(u => u.UserName == "Admin");
            if (adminUser == null)
            {
                adminUser = ObjectSpace.CreateObject<PermissionPolicyUser>();
                adminUser.UserName = "Admin";
                adminUser.SetPassword("Admin");
                adminUser.Roles.Add(adminRole);
            }
            //string name = "MyName";
            //DomainObject1 theObject = ObjectSpace.FirstOrDefault<DomainObject1>(u => u.Name == name);
            //if(theObject == null) {
            //    theObject = ObjectSpace.CreateObject<DomainObject1>();
            //    theObject.Name = name;
            //}

            ObjectSpace.CommitChanges(); //Uncomment this line to persist created object(s).
        }
        public override void UpdateDatabaseBeforeUpdateSchema()
        {
            base.UpdateDatabaseBeforeUpdateSchema();
            //if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
            //    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
            //}
        }
    }
}
