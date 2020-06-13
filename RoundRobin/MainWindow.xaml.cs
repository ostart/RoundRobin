using System;
using System.IO;
using System.Windows;
using System.Text.Json;
using System.Windows.Controls;
using System.Windows.Threading;
using RoundRobin.BL;

namespace RoundRobin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Settings _appSettings;
        private readonly DispatcherTimer _timer;
        private bool _isPauseActivated;
        private Manager _manager;
        private readonly Random _random;

        public MainWindow()
        {
            InitializeComponent();
            _timer = new DispatcherTimer();
            _timer.Tick += TimerTick;
            _random = new Random();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            _manager.DoWork();
            if (_random.Next(0, 2) == 0)
                _manager.MethodB();
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            var settingsWindow = new SettingsWindow(_appSettings);
            settingsWindow.Show();
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            if (_isPauseActivated)
            {
                _timer.Start();
                _isPauseActivated = false;
            }
            else
            {
                _timer.Stop();
                _isPauseActivated = true;
            }
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            _manager = new Manager(_appSettings, _random);
            _manager.TaskFinished += AddMessageToTracing;
            _manager.MethodA();
            Executors.ItemsSource = _manager.ExecutorsList;
            _timer.Interval = new TimeSpan(0, 0, _appSettings.TimerTicksInSeconds);
            _timer.Start();
            _isPauseActivated = false;
        }

        private void AddMessageToTracing(Object sender, TracingEventArgs e)
        {
            Tracing.Text += $"{e.Message}{Environment.NewLine}";
        }

        private void MainWindow_OnClosed(object sender, EventArgs e)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            var jsonString = JsonSerializer.Serialize(_appSettings, options);
            File.WriteAllText("settings.json", jsonString);
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var jsonString = File.ReadAllText("settings.json");
                _appSettings = JsonSerializer.Deserialize<Settings>(jsonString);
            }
            catch
            {
                _appSettings = new Settings();
            }
        }

        private void Executors_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is Executor executor)
            {
                ExecutorTasks.ItemsSource = executor.TaskQueue;
            }
        }
    }
}
