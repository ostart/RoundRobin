using System.Windows;
using RoundRobin.BL;

namespace RoundRobin
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private readonly Settings _appSettings;

        public SettingsWindow(Settings appSettings)
        {
            _appSettings = appSettings;
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            _appSettings.TimerTicksInSeconds = int.Parse(tbTimerTicksInSec.Text);
            _appSettings.MinNumberOfExecutors = int.Parse(tbMinNumOfExec.Text);
            _appSettings.MaxNumberOfExecutors = int.Parse(tbMaxNumOfExec.Text);
            _appSettings.MinValueOfExecutorPerformance = int.Parse(tbMinExecPerform.Text);
            _appSettings.MaxValueOfExecutorPerformance = int.Parse(tbMaxExecPerform.Text);
            _appSettings.MinNumberOfTasks = int.Parse(tbMinNumOfTasks.Text);
            _appSettings.MaxNumberOfTasks = int.Parse(tbMaxNumOfTasks.Text);
            _appSettings.MinValueOfTaskComplexity = int.Parse(tbMinTaskComplex.Text);
            _appSettings.MaxValueOfTaskComplexity = int.Parse(tbMaxTaskComplex.Text);
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SettingsWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            tbTimerTicksInSec.Text = _appSettings.TimerTicksInSeconds.ToString();
            tbMinNumOfExec.Text = _appSettings.MinNumberOfExecutors.ToString();
            tbMaxNumOfExec.Text = _appSettings.MaxNumberOfExecutors.ToString();
            tbMinExecPerform.Text = _appSettings.MinValueOfExecutorPerformance.ToString();
            tbMaxExecPerform.Text = _appSettings.MaxValueOfExecutorPerformance.ToString();
            tbMinNumOfTasks.Text = _appSettings.MinNumberOfTasks.ToString();
            tbMaxNumOfTasks.Text = _appSettings.MaxNumberOfTasks.ToString();
            tbMinTaskComplex.Text = _appSettings.MinValueOfTaskComplexity.ToString();
            tbMaxTaskComplex.Text = _appSettings.MaxValueOfTaskComplexity.ToString();
        }
    }
}
