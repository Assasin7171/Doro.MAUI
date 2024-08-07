using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Timer = System.Timers.Timer;

namespace Doro.MAUI.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty] private string _timeLeftInMinutes = "0";
    [ObservableProperty] private double _progressCount;
    private int _count;
    private bool _isChange = false;
    private readonly Timer _timer;
    private int _timeLeftInSeconds = 0;
    private int _totalTimeInSeconds = 0;

    public MainViewModel()
    {
        _timer = new Timer();
    }

    [ObservableProperty] private TimeSpan _result;

    [RelayCommand]
    private void CalculateTimeLeft(string param)
    {
        _count = 0;
        
        if (param == "Start")
        {
            //_timer.Dispose();
            //w minutach
            if (int.TryParse(TimeLeftInMinutes, out int result))
            {
                _timeLeftInSeconds = result * 60;
                _totalTimeInSeconds = result * 60;
                if (_timeLeftInSeconds > 0)
                {
                    _timer.Interval = 1000;
                    _timer.Elapsed += (sender, args) =>
                    {
                        _timeLeftInSeconds -= 1;
                        _count++;
                        if (_timeLeftInSeconds == 0)
                        {
                            _timer.Stop();
                        }

                        Result = new TimeSpan(0, 0, _timeLeftInSeconds);
                        ProgressCount = CalculateProgress(_count, _totalTimeInSeconds);
                    };
                    _timer.Start();
                }
            }
        }
        else if (param == "Stop")
        {
            _timer.Stop();
            ProgressCount = 0;
            _count = 0;
            Result = TimeSpan.Zero;
        }
        else if (param == "Pauza")
        {
            switch (_isChange)
            {
                case false:
                    _timer.Stop();
                    _isChange = true;
                    break;
                case true:
                    _timer.Start();
                    _isChange = false;
                    break;
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