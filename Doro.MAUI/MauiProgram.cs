using Doro.MAUI.ViewModels;
using Doro.MAUI.Views;
using Microsoft.Extensions.Logging;

namespace Doro.MAUI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Honk-Regular.ttf", "HonkRegural");
            });
        
        builder.Services.AddTransient<MainView>(s=> new MainView()
        {
            BindingContext = s.GetRequiredService<MainViewModel>()
        });
        builder.Services.AddTransient<MainViewModel>();
        
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}