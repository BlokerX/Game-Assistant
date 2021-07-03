using System;
using System.IO;
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
        public static string ClockSettingsPath; // PATH //
        public DispatcherTimer clockTimer = new DispatcherTimer(DispatcherPriority.Background);

        public ClockWidget()
        {
            InitializeComponent();
            UpdateTimeToNow();
        }

        #region StaticMethods

        /// <summary>
        /// Read and return save informaions about ClockWidget
        /// </summary>
        /// <returns>Informations about Save of ClockWidget</returns>
        public new static ClockInformation DownloadWidgetInformationOfFile()
        {
            if (File.Exists(ClockSettingsPath))
            {
                ClockInformation clockInformation = new ClockInformation();

                using (StreamReader sr = File.OpenText(ClockSettingsPath))
                {
                    clockInformation = (ClockInformation)GetWidgetInformationOfFile(clockInformation, sr);
                    if (clockInformation == null)
                    {
                        return null;
                    }

                    if (true)
                    {
                        string a = sr.ReadLine();
                        if (double.TryParse(a, out double aDouble))
                        {
                            clockInformation.ClockOpacity = aDouble;
                        }
                        else
                        {
                            sr.Close();
                            return null;
                        }
                    }

                    if (true)
                    {
                        string a = sr.ReadLine();

                        if (double.TryParse(a, out double aDouble))
                        {
                            clockInformation.BackgroundClockOpacity = aDouble;
                        }
                        else
                        {
                            sr.Close();
                            return null;
                        }
                    }

                    if (true)
                    {
                        string a = sr.ReadLine();

                        if (a == null || a == "")
                        {
                            sr.Close();
                            return null;
                        }
                        try
                        {

                            System.Windows.Media.Color color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(a);
                            if (color != null)
                            {
                                clockInformation.BackgroundColor = color;
                            }
                            else
                            {
                                sr.Close();
                                return null;
                            }
                        }
                        catch
                        {
                            sr.Close();
                            return null;
                        }
                    }

                    if (true)
                    {
                        string a = sr.ReadLine();

                        if (a == null || a == "")
                        {
                            sr.Close();
                            return null;
                        }
                        try
                        {

                            System.Windows.Media.Color color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(a);
                            if (color != null)
                            {
                                clockInformation.ForegroundColor = color;
                            }
                            else
                            {
                                sr.Close();
                                return null;
                            }
                        }
                        catch
                        {
                            sr.Close();
                            return null;
                        }
                    }

                    sr.Close();
                    return clockInformation;
                }
            }
            else { return null; }
        }

        /// <summary>
        /// Create ClockWidget
        /// </summary>
        /// <returns>ClockWidget</returns>
        public new static ClockWidget CreateWidget()
        {
            ClockInformation clockInf = ClockWidget.DownloadWidgetInformationOfFile();
            if (clockInf != null)
            {
                ClockWidget cf = new ClockWidget()
                {
                    Left = clockInf.PositionX,
                    Top = clockInf.PositionY
                };
                cf.Width = clockInf.Width;
                cf.Height = clockInf.Heigth;

                cf.ClockLabel.Opacity = clockInf.ClockOpacity;
                cf.rec1.Opacity = clockInf.BackgroundClockOpacity;

                cf.rec1.Fill = new System.Windows.Media.SolidColorBrush(clockInf.BackgroundColor);
                cf.ClockLabel.Foreground = new System.Windows.Media.SolidColorBrush(clockInf.ForegroundColor);

                cf.SelectAnimation(clockInf);

                return cf;
            }
            else
            { return new ClockWidget(); }

        }

        /// <summary>
        /// Write in file selected settings about ClockWidget
        /// </summary>
        /// <param name="argCW">ClockWidget to saving</param>
        public static void UpdateWidgetInformationOfFile(ClockWidget argCW)
        {
            MainWindow.CheckProgramDiresArchitecture();

            if (argCW != null && argCW.IsVisible)
            {
                #region CreateFileOfSettings NowSettings

                using (StreamWriter sw = File.CreateText(ClockWidget.ClockSettingsPath))
                {
                    UpdateWidgetInformationOfFileSaveObject(argCW, sw);

                    sw.WriteLine(argCW.ClockLabel.Opacity.ToString());
                    sw.WriteLine(argCW.rec1.Opacity.ToString());

                    sw.WriteLine(new System.Windows.Media.ColorConverter().ConvertToString(MainWindow.BrushToColorMedia(argCW.rec1.Fill)));
                    sw.WriteLine(new System.Windows.Media.ColorConverter().ConvertToString(MainWindow.BrushToColorMedia(argCW.ClockLabel.Foreground)));

                    sw.Close();
                }
                #endregion
            }
            else
            {
                ClockInformation cI = ClockWidget.DownloadWidgetInformationOfFile();
                if (File.Exists(ClockWidget.ClockSettingsPath) && cI != null)
                {
                    #region OverwriteFile

                    ClockInformation clockInf = ClockWidget.DownloadWidgetInformationOfFile();

                    using (StreamWriter sw = File.CreateText(ClockWidget.ClockSettingsPath))
                    {
                        UpdateWidgetInformationOfFileSaveWidgetInformation(clockInf, sw);

                        sw.WriteLine(clockInf.ClockOpacity);
                        sw.WriteLine(clockInf.BackgroundClockOpacity);

                        sw.WriteLine(clockInf.BackgroundColor.ToString());
                        sw.WriteLine(clockInf.ForegroundColor.ToString());

                        sw.Close();
                    }

                    #endregion

                }
                else
                {
                    #region CreateFileOfSettings DeffaultSettings

                    using (StreamWriter sw = File.CreateText(ClockWidget.ClockSettingsPath))
                    {
                        sw.WriteLine(true.ToString());

                        sw.WriteLine("100");
                        sw.WriteLine("100");

                        sw.WriteLine("200");
                        sw.WriteLine("62");

                        sw.WriteLine(Animations.NULL.ToString());

                        sw.WriteLine((0.75).ToString());
                        sw.WriteLine((0.50).ToString());

                        sw.WriteLine("#FFFFFFB5");
                        sw.WriteLine("#FF000000");

                        sw.Close();
                    }

                    #endregion
                }
            }



        }

        #endregion

        #region Events

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

        private void ClockWidget_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ClockWidget.UpdateWidgetInformationOfFile(this);
        }

        private void ClockWidget_LocationChanged(object sender, EventArgs e)
        {
            ClockWidget.UpdateWidgetInformationOfFile(this);
        }

        private void ClockWidget_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            clockTimer.Stop();
        }

        #endregion

        #region Animations

        protected override void Animation_ColorRainbowRGB_GoOn()
        {
            rec1.Fill = this.Animation_ColorRainbowRGB(rec1.Fill);
        }

        protected override void Animation_ColorRainbowRGB2_GoOn()
        {
            rec1.Fill = this.Animation_ColorRainbowRGB2(rec1.Fill);
        }

        #endregion

        #region Helpers

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

        #endregion
    }
}
