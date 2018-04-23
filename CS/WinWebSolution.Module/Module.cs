using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using System.Reflection;
using DevExpress.ExpressApp.Xpo;
using System.Configuration;

namespace WinWebSolution.Module {
    public sealed partial class WinWebSolutionModule : ModuleBase {
        private static XpoDataStoreProxyProvider provider;
        public WinWebSolutionModule() {
            InitializeComponent();
        }
        public override void Setup(XafApplication application) {
            base.Setup(application);
            application.CustomCheckCompatibility += new EventHandler<CustomCheckCompatibilityEventArgs>(application_CustomCheckCompatibility);
            application.CreateCustomObjectSpaceProvider += new EventHandler<CreateCustomObjectSpaceProviderEventArgs>(application_CreateCustomObjectSpaceProvider);
        }
        void application_CreateCustomObjectSpaceProvider(object sender, CreateCustomObjectSpaceProviderEventArgs e) {
            if(provider == null) {
                provider = new XpoDataStoreProxyProvider();
            }
            e.ObjectSpaceProvider = new XPObjectSpaceProvider(provider);
        }
        void application_CustomCheckCompatibility(object sender, CustomCheckCompatibilityEventArgs e) {
            if(provider != null && !provider.IsInitialized) {
                provider.Initialize(((XPObjectSpaceProvider)e.ObjectSpaceProvider).XPDictionary,
                    ConfigurationManager.ConnectionStrings["LegacyDatabaseConnectionString"].ConnectionString,
                    ConfigurationManager.ConnectionStrings["TempDatabaseConnectionString"].ConnectionString);
            }
        }
    }
}
