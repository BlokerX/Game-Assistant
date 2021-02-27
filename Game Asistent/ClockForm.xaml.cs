using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace GameAssistant
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class ClockForm : Window
    {
        // Varibles:
        public DispatcherTimer clockTimer = new DispatcherTimer(DispatcherPriority.Background);

        public ClockForm()
        {
            InitializeComponent();
        }

        private void ClockWindow_Activated(object sender, EventArgs e)
        {
            #region TimerStartProcedure
            clockTimer.Interval = TimeSpan.FromMilliseconds(50);
            clockTimer.Tick += OnClockTimer_Tick;
            clockTimer.Start();
            #endregion
        }
        private void OnClockTimer_Tick(object sender, EventArgs e)
        {
            this.ClockLabel.Content =
                (DateTime.Now.Hour / 10).ToString() +
                (DateTime.Now.Hour % 10).ToString() + ":" +
                (DateTime.Now.Minute / 10).ToString() +
                (DateTime.Now.Minute % 10).ToString() + ":" +
                (DateTime.Now.Second / 10).ToString() +
                (DateTime.Now.Second % 10).ToString();
        }

        private void ClockLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void ClockWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            clockTimer.Stop();
        }
    }
}
