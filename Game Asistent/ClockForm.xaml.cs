using System;
using System.Windows;
using System.Windows.Threading;

namespace GameAssistant
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class ClockWidget : WidgetWindow
    {
        // Varibles:
        public DispatcherTimer clockTimer = new DispatcherTimer(DispatcherPriority.Background);

        public ClockWidget()
        {
            InitializeComponent();
            UpdateTimeToNow();
        }

        private void ClockWidget_Activated(object sender, EventArgs e)
        {
            #region TimerStartProcedure
            clockTimer.Interval = TimeSpan.FromMilliseconds(50);
            clockTimer.Tick += OnClockTimer_Tick;
            clockTimer.Start();
            #endregion
        }

        private void OnClockTimer_Tick(object sender, EventArgs e)
        {
            UpdateTimeToNow();
        }

        private void UpdateTimeToNow()
        {
            this.ClockLabel.Content =
                            (DateTime.Now.Hour / 10).ToString() +
                            (DateTime.Now.Hour % 10).ToString() + ":" +
                            (DateTime.Now.Minute / 10).ToString() +
                            (DateTime.Now.Minute % 10).ToString() + ":" +
                            (DateTime.Now.Second / 10).ToString() +
                            (DateTime.Now.Second % 10).ToString();
        }

        private void ClockWidget_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            clockTimer.Stop();
        }

        private void ClockWidget_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            MainWindow.UpdateClockInformationOfFile(this);
        }
        
        private void ClockWidget_LocationChanged(object sender, EventArgs e)
        {
            MainWindow.UpdateClockInformationOfFile(this);
        }
    }
}
