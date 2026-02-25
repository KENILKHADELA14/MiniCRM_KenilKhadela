using System;
using System.ComponentModel;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Model.DomainLogics;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.Scheduler;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using Microsoft.CodeAnalysis.Operations;
using DevExpress.Persistent.Base.General;
using MiniCRM_KenilKhadela.Module.BusinessObjects;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.BaseImpl.AuditTrail.Services;
using DevExpress.Persistent.AuditTrail;

namespace MiniCRM_KenilKhadela.Module
{
    // For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.ModuleBase.
    public sealed class MiniCRM_KenilKhadelaModule : ModuleBase
    {
        private static readonly object lockObject = new object();
        private bool isInitialized = false;

        public MiniCRM_KenilKhadelaModule()
        {
            // 
            // MiniCRM_KenilKhadelaModule
            // 
            AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.BaseObject));
            AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.AuditDataItemPersistent));
            AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.AuditedObjectWeakReference));
            AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.FileData));
            AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.FileAttachmentBase));
            RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.SystemModule.SystemModule));
            RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.AuditTrail.AuditTrailModule));
            RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Chart.ChartModule));
            RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule));
            RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Notifications.NotificationsModule));
            RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Office.OfficeModule));
            RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.ReportsV2.ReportsModuleV2));
            RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Validation.ValidationModule));
            RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Scheduler.SchedulerModuleBase));
            RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Dashboards.DashboardsModule));
            //RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.DashboardView);
            RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.AuditTrail.AuditTrailModule));
        }

        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB)
        {
            ModuleUpdater updater = new DatabaseUpdate.Updater(objectSpace, versionFromDB);
            return new ModuleUpdater[] { updater };
        }

        public override void Setup(XafApplication application)
        {
            base.Setup(application);

            lock (lockObject)
            {
                if (!isInitialized)
                {
                    application.LoggedOn -= Application_LoggedOn;
                    application.LoggingOff -= Application_LoggedOff;

                    application.LoggedOn += Application_LoggedOn;
                    application.LoggingOff += Application_LoggedOff;

                    isInitialized = true;

                    // Add debug output to confirm setup
                }
            }
        }

        private void Application_LoggedOff(object sender, EventArgs e)
        {
            try
            {
                string currentUserName = SecuritySystem.CurrentUserName;
                if (string.IsNullOrEmpty(currentUserName)) return;

                var app = sender as XafApplication;
                if (app == null) return;
                using (IObjectSpace os = app.CreateObjectSpace(typeof(LoginHistory)))
                {
                    var lastLogin = os.GetObjectsQuery<LoginHistory>()
                        .Where(i => i.UserName == currentUserName && i.LogoutTime == null && i.Operation=="LoggedOn")
                        .OrderByDescending(p => p.LoginTime)
                        .FirstOrDefault();

                    if (lastLogin != null)
                    {
                        var logoffRecord =os.CreateObject<LoginHistory>();
                        logoffRecord.UserName = currentUserName;
                        logoffRecord.Operation = "LoggedOff";
                        logoffRecord.LoginTime = lastLogin.LoginTime;
                        logoffRecord.LogoutTime = DateTime.Now;
                        logoffRecord.IPAddress = lastLogin.IPAddress;
                        logoffRecord.HostName = lastLogin.HostName;

                        os.CommitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in LoggedOff handler: {ex.Message}");
            }
        }

        private void Application_LoggedOn(object sender, LogonEventArgs e)
        {
            try
            {
                var app = sender as XafApplication;
                if (app == null) { return; }

                if (e.LogonParameters is AuthenticationStandardLogonParameters logonParameters)
                {
                    string userName = logonParameters.UserName;
                    string password = logonParameters.Password;

                    if (!string.IsNullOrEmpty(password))
                    {
                        using (var os = app.CreateObjectSpace())
                        {
                            var recentLogin = os.GetObjectsQuery<LoginHistory>()
                                .Where(h => h.UserName == userName
                                           && h.Operation == "LoggedOn"
                                           && h.LoginTime > DateTime.Now.AddSeconds(-30))
                                .OrderByDescending(h => h.LoginTime)
                                .FirstOrDefault();

                            if (recentLogin == null)
                            {
                                var history = os.CreateObject<LoginHistory>();
                                history.UserName = userName;
                                history.Operation = "LoggedOn";
                                history.LoginTime = DateTime.Now;

                                try
                                {
                                    var ipProperty = history.GetType().GetProperty("IPAddress");
                                    if (ipProperty != null && ipProperty.CanWrite)
                                    {
                                        ipProperty.SetValue(history, GetLocalIPAddress());
                                    }

                                    var hostProperty = history.GetType().GetProperty("HostName");
                                    if (hostProperty != null && hostProperty.CanWrite)
                                    {
                                        hostProperty.SetValue(history, Environment.MachineName);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    System.Diagnostics.Debug.WriteLine($"{ex.Message}");
                                }

                                os.CommitChanges();
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine($"Duplicate login prevented for {userName} - Last login was at {recentLogin.LoginTime}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in LoggedOn handler: {ex.Message}");
            }
        }

        private string GetLocalIPAddress()
        {
            try
            {
                var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting IP address: {ex.Message}");
            }
            return "Unknown";
        }

        public override void CustomizeTypesInfo(ITypesInfo typesInfo)
        {
            base.CustomizeTypesInfo(typesInfo);
            CalculatedPersistentAliasHelper.CustomizeTypesInfo(typesInfo);
        }
    }
}
