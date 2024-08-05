using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Timer = System.Timers.Timer;

namespace Doro.MAUI.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty] private string _timeLeftInMinutes = "0";
    [ObservableProperty] private double _progressCount;
    private int _count;

    [ObservableProperty] private TimeSpan _result;

    [RelayCommand]
    private void CalculateTimeLeft()
    {
        //w minutach
        if (int.TryParse(TimeLeftInMinutes, out int result))
        {
            var timeLeftInSeconds = result * 60;
            var totalTimeInSeconds = result * 60;
            if (timeLeftInSeconds > 0)
            {
                Timer timer = new Timer(1000);
                timer.Elapsed += (sender, args) =>
                {
                    timeLeftInSeconds -= 1;
                    _count++;
                    if (timeLeftInSeconds == 0)
                    {
                        timer.Stop();
                    }

                    Result = new TimeSpan(0, 0, timeLeftInSeconds);
                    ProgressCount = CalculateProgress(_count, totalTimeInSeconds);
                };
                timer.Start();
            }
        }
    }

    private double CalculateProgress(double howMuchSecondsHasPassed, double totalTimeInSeconds)
    {
        //wynik = (ile upłyneło już/ilość sekund łączność)*100;
        double result = (howMuchSecondsHasPassed / totalTimeInSeconds) * 100;

        return result / 100;
    }
}