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

                    if (true)
                    {
                        string a = sr.ReadLine();
                        if (bool.TryParse(a, out bool aBool))
                        {
                            clockInformation.IsChosed = aBool;
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
                        if (int.TryParse(a, out int aInt))
                        {
                            clockInformation.PositionX = aInt;
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
                        if (int.TryParse(a, out int aInt))
                        {
                            clockInformation.PositionY = aInt;
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
                        if (int.TryParse(a, out int aInt))
                        {
                            clockInformation.Width = aInt;
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
                        if (int.TryParse(a, out int aInt))
                        {
                            clockInformation.Heigth = aInt;
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

                    if (true)
                    {
                        string a = sr.ReadLine();
                        if (Animations.TryParse(a, out Animations aAnimations))
                        {
                            clockInformation.Animation = aAnimations;
                        }
                        else
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
                    sw.WriteLine(true.ToString());

                    sw.WriteLine(argCW.Left.ToString());
                    sw.WriteLine(argCW.Top.ToString());

                    sw.WriteLine(argCW.Width.ToString());
                    sw.WriteLine(argCW.Height.ToString());

                    sw.WriteLine(argCW.ClockLabel.Opacity.ToString());
                    sw.WriteLine(argCW.rec1.Opacity.ToString());

                    sw.WriteLine(new System.Windows.Media.ColorConverter().ConvertToString(MainWindow.BrushToColorMedia(argCW.rec1.Fill)));
                    sw.WriteLine(new System.Windows.Media.ColorConverter().ConvertToString(MainWindow.BrushToColorMedia(argCW.ClockLabel.Foreground)));
                    sw.WriteLine(argCW.ChosedAnimation.ToString());

                    sw.Close();
                }
                #endregion
            }
            else
            {
                ClockInformation cI = ClockWidget.DownloadWidgetInformationOfFile();
                if (File.Exists(ClockWidget.ClockSettingsPath) && cI != null)
                {
                    #region DownloadInformationsOfFile

                    ClockInformation clockInf = ClockWidget.DownloadWidgetInformationOfFile();
                    bool _isChosed = false;

                    int _positionX = clockInf.PositionX;
                    int _positionY = clockInf.PositionY;

                    int _width = clockInf.Width;
                    int _heigth = clockInf.Heigth;

                    double _clockOpacity = clockInf.ClockOpacity;
                    double _backgroundClockOpacity = clockInf.BackgroundClockOpacity;

                    string _backgroundColor = clockInf.BackgroundColor.ToString();
                    string _foregroundColor = clockInf.ForegroundColor.ToString();

                    Animations _chosedAnimation = clockInf.Animation;

                    #endregion

                    #region OverwriteFile

                    using (StreamWriter sw = File.CreateText(ClockWidget.ClockSettingsPath))
                    {
                        sw.WriteLine(_isChosed.ToString());

                        sw.WriteLine(_positionX.ToString());
                        sw.WriteLine(_positionY.ToString());

                        sw.WriteLine(_width.ToString());
                        sw.WriteLine(_heigth.ToString());

                        sw.WriteLine(_clockOpacity.ToString());
                        sw.WriteLine(_backgroundClockOpacity.ToString());
                        sw.WriteLine(_backgroundColor.ToString());
                        sw.WriteLine(_foregroundColor.ToString());
                        sw.WriteLine(_chosedAnimation.ToString());

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

                        sw.WriteLine((0.75).ToString());
                        sw.WriteLine((0.50).ToString());

                        sw.WriteLine("#FFFFFFB5");
                        sw.WriteLine("#FF000000");
                        sw.WriteLine(Animations.NULL.ToString());

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
