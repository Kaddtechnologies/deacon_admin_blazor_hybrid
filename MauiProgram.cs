using DeaconAdminHybrid.Components.Data;
using Microsoft.Extensions.Logging;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Popups;

namespace DeaconAdminHybrid
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzE3ODM2NUAzMjM1MmUzMDJlMzBVL05EeWJFaTdaS2ROQWVYdmJLQXpiMzNpUVVtazZKZ0tibVRTbko0c3JFPQ==");
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddSyncfusionBlazor();
            builder.Services.AddScoped<UserDataAccessLayer>();
            builder.Services.AddScoped<UserDataAdaptor>();
            builder.Services.AddScoped<SfDialogService>();



#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
