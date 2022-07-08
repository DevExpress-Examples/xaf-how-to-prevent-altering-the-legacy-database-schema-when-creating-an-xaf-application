using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ApplicationBuilder;
using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Xpo;
using AspNetCore.Blazor.Server.Services;
using AspNetCore.Module.Services;

namespace AspNetCore.Blazor.Server;

public class AspNetCoreBlazorApplication : BlazorApplication {
    public AspNetCoreBlazorApplication() {
        ApplicationName = "AspNetCore";
        CheckCompatibilityType = DevExpress.ExpressApp.CheckCompatibilityType.DatabaseSchema;
        DatabaseVersionMismatch += AspNetCoreBlazorApplication_DatabaseVersionMismatch;
    }
    protected override void OnSetupStarted() {
        base.OnSetupStarted();
#if DEBUG
        if(System.Diagnostics.Debugger.IsAttached && CheckCompatibilityType == CheckCompatibilityType.DatabaseSchema) {
            DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
        }
#endif
    }

    protected override void OnCustomCheckCompatibility(CustomCheckCompatibilityEventArgs args) {
        base.OnCustomCheckCompatibility(args);
        var configuration = ServiceProvider.GetRequiredService<IConfiguration>();
        var provider = ServiceProvider.GetRequiredService<XpoDataStoreProxyProvider>();
        if(provider != null && !provider.IsInitialized) {
            provider.Initialize(((XPObjectSpaceProvider)args.ObjectSpaceProvider).XPDictionary,
                configuration.GetConnectionString("LegacyDatabaseConnectionString"),
                configuration.GetConnectionString("TempDatabaseConnectionString"));
        }
    }

    private void AspNetCoreBlazorApplication_DatabaseVersionMismatch(object sender, DatabaseVersionMismatchEventArgs e) {
#if EASYTEST
        e.Updater.Update();
        e.Handled = true;
#else
        if(System.Diagnostics.Debugger.IsAttached) {
            e.Updater.Update();
            e.Handled = true;
        }
        else {
            string message = "The application cannot connect to the specified database, " +
                "because the database doesn't exist, its version is older " +
                "than that of the application or its schema does not match " +
                "the ORM data model structure. To avoid this error, use one " +
                "of the solutions from the https://www.devexpress.com/kb=T367835 KB Article.";

            if(e.CompatibilityError != null && e.CompatibilityError.Exception != null) {
                message += "\r\n\r\nInner exception: " + e.CompatibilityError.Exception.Message;
            }
            throw new InvalidOperationException(message);
        }
#endif
    }
}
