using Microsoft.Diagnostics.Tracing.Session;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace GameAssistant
{
    /// <summary>
    /// Logika interakcji dla klasy FPSCounterWidget.xaml
    /// </summary>
    public partial class FPSCounterWidget : WidgetWindow
    {
        #region VariblesAndObjects
        public static string FPSCounterSettingsPath; // PATH //
        public string SelectProcessFPS { get; set; } = "Default";

        public DispatcherTimer fcwTimer = new DispatcherTimer(DispatcherPriority.Background);

        //event codes (https://github.com/GameTechDev/PresentMon/blob/40ee99f437bc1061a27a2fc16a8993ee8ce4ebb5/PresentData/PresentMonTraceConsumer.cpp)
        public const int EventID_D3D9PresentStart = 1;
        public const int EventID_DxgiPresentStart = 42;

        //ETW provider codes
        public static readonly Guid DXGI_provider = Guid.Parse("{CA11C036-0102-4A2D-A6AD-F03CFED5D3C9}");
        public static readonly Guid D3D9_provider = Guid.Parse("{783ACA0A-790E-4D7F-8451-AA850511C6B9}");

        static TraceEventSession m_EtwSession;
        static readonly Dictionary<int, TimestampCollection> frames = new Dictionary<int, TimestampCollection>();

        public static Dictionary<int, TimestampCollection> GetFrames()
        {
            return FPSCounterWidget.frames;
        }

        static Stopwatch watch = null;
        static readonly object sync = new object();

        #endregion

        #region StaticMethods

        /// <summary>
        /// Read and return save informaions about FPSCounterWidget
        /// </summary>
        /// <returns>Informations about Save of FPSCounterWidget</returns>
        public new static FPSCounterInformation DownloadWidgetInformationOfFile()
        {
            if (File.Exists(FPSCounterSettingsPath))
            {
                FPSCounterInformation fpsCounterInformation = new FPSCounterInformation();

                using (StreamReader sr = File.OpenText(FPSCounterSettingsPath))
                {
                    fpsCounterInformation = (FPSCounterInformation)GetWidgetInformationOfFile(fpsCounterInformation, sr);
                    if (fpsCounterInformation == null)
                    {
                        return null;
                    }

                    if (true)
                    {
                        string a = sr.ReadLine();
                        if (double.TryParse(a, out double aDouble))
                        {
                            fpsCounterInformation.FPSOpacity = aDouble;
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
                            fpsCounterInformation.BackgroundOpacity = aDouble;
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
                                fpsCounterInformation.BackgroundColor = color;
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
                                fpsCounterInformation.ForegroundColor = color;
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

                    fpsCounterInformation.SelectedProcess = sr.ReadLine(); //todo musi to być ostatnie na liście

                    sr.Close();
                    return fpsCounterInformation;
                }
            }
            else { return null; }
        }

        /// <summary>
        /// Create FPSCounterWidget
        /// </summary>
        /// <returns>FPSCounterWidget</returns>
        public new static FPSCounterWidget CreateWidget()
        {
            FPSCounterInformation fpsCounterInf = FPSCounterWidget.DownloadWidgetInformationOfFile();
            if (fpsCounterInf != null)
            {
                FPSCounterWidget fcw = new FPSCounterWidget()
                {
                    Left = fpsCounterInf.PositionX,
                    Top = fpsCounterInf.PositionY,
                    SelectProcessFPS = fpsCounterInf.SelectedProcess
                };

                fcw.Width = fpsCounterInf.Width;
                fcw.Height = fpsCounterInf.Heigth;

                fcw.lab1.Opacity = fpsCounterInf.FPSOpacity;
                fcw.rec1.Opacity = fpsCounterInf.BackgroundOpacity;

                fcw.rec1.Fill = new System.Windows.Media.SolidColorBrush(fpsCounterInf.BackgroundColor);
                fcw.lab1.Foreground = new System.Windows.Media.SolidColorBrush(fpsCounterInf.ForegroundColor);

                fcw.SelectAnimation(fpsCounterInf);

                return fcw;
            }
            else
            { return new FPSCounterWidget(); }

        }

        /// <summary>
        /// Write in file selected settings about FPSCounterWidget
        /// </summary>
        /// <param name="argFCW">FPSCounterWidget to saving</param>
        public static void UpdateWidgetInformationOfFile(FPSCounterWidget argFCW)
        {
            MainWindow.CheckProgramDiresArchitecture();

            if (argFCW != null && argFCW.IsVisible)
            {
                #region CreateFileOfSettings NowSettings

                using (StreamWriter sw = File.CreateText(FPSCounterWidget.FPSCounterSettingsPath))
                {
                    UpdateWidgetInformationOfFileSaveObject(argFCW, sw);

                    sw.WriteLine(argFCW.lab1.Opacity.ToString());
                    sw.WriteLine(argFCW.rec1.Opacity.ToString());

                    sw.WriteLine(new System.Windows.Media.ColorConverter().ConvertToString(MainWindow.BrushToColorMedia(argFCW.rec1.Fill)));
                    sw.WriteLine(new System.Windows.Media.ColorConverter().ConvertToString(MainWindow.BrushToColorMedia(argFCW.lab1.Foreground)));
                    sw.WriteLine(argFCW.SelectProcessFPS);

                    sw.Close();
                }
                #endregion
            }
            else
            {
                FPSCounterInformation fcI = FPSCounterWidget.DownloadWidgetInformationOfFile();
                if (File.Exists(FPSCounterWidget.FPSCounterSettingsPath) && fcI != null)
                {
                    #region OverwriteFile

                    FPSCounterInformation fpsCounterInf = FPSCounterWidget.DownloadWidgetInformationOfFile();

                    using (StreamWriter sw = File.CreateText(FPSCounterWidget.FPSCounterSettingsPath))
                    {
                        UpdateWidgetInformationOfFileSaveWidgetInformation(fpsCounterInf, sw);

                        sw.WriteLine(fpsCounterInf.FPSOpacity.ToString());
                        sw.WriteLine(fpsCounterInf.BackgroundOpacity.ToString());
                        sw.WriteLine(fpsCounterInf.BackgroundColor.ToString());
                        sw.WriteLine(fpsCounterInf.ForegroundColor.ToString());
                        sw.WriteLine(fpsCounterInf.SelectedProcess);

                        sw.Close();
                    }

                    #endregion

                }
                else
                {
                    #region CreateFileOfSettings DeffaultSettings

                    using (StreamWriter sw = File.CreateText(FPSCounterWidget.FPSCounterSettingsPath))
                    {
                        sw.WriteLine(true.ToString());

                        sw.WriteLine("70");
                        sw.WriteLine("60");

                        sw.WriteLine("200");
                        sw.WriteLine("62");

                        sw.WriteLine(Animations.NULL.ToString());

                        sw.WriteLine((0.75).ToString());
                        sw.WriteLine((0.50).ToString());

                        sw.WriteLine("#FFFFFFB5");
                        sw.WriteLine("#FF000000");

                        sw.WriteLine("dwm");

                        sw.Close();
                    }

                    #endregion
                }
            }



        }

        #endregion

        public FPSCounterWidget()
        {
            InitializeComponent();

            //LoadFPSReader();
            //todo to do naprawy

            watch = new Stopwatch();
            watch.Start();

            Thread thETW = new Thread(EtwThreadProc)
            {
                IsBackground = true
            };
            thETW.Start();

            #region TimerStartProcedure
            fcwTimer.Interval = TimeSpan.FromMilliseconds(25);
            fcwTimer.Tick += OnFPSTimer_Tick;
            fcwTimer.Start();
            #endregion

            //Thread thOutput = new Thread(OutputThreadProc);
            //thOutput.IsBackground = true;
            //thOutput.Start();
        }

        private void OnFPSTimer_Tick(object sender, EventArgs e)
        {
            OutputThreadProc();
        }

        static void EtwThreadProc()
        {
            //start tracing
            //m_EtwSession.Source.Process();
            //todo to do naprawy
        }

        void OutputThreadProc()
        {
            //console output loop
            long t1, t2;
            long dt = 2000;

            lock (sync)
            {
                t2 = watch.ElapsedMilliseconds;
                t1 = t2 - dt;
                if(frames.Count != 0)
                foreach (var x in frames.Values)
                {
                    if (x.Name == SelectProcessFPS)
                    {
                        //get the number of frames
                        int count = x.QueryCount(t1, t2);

                        //calculate FPS
                        lab1.Content = /*x.Name + ": " +*/ (double)count / dt * 1000.0 + " FPS";
                    }
                }

            }
        }

        public static void LoadFPSReader()
        {
            string mysessName = "GameAssistantFPSCounterSession";

            //create ETW session and register providers
            using (m_EtwSession = new TraceEventSession(mysessName)
            {
                StopOnDispose = true
            })
                m_EtwSession.EnableProvider("Microsoft-Windows-D3D9");
            m_EtwSession.EnableProvider("Microsoft-Windows-DXGI");

            //handle event
            m_EtwSession.Source.AllEvents += data =>
            {
                //filter out frame presentation events
                if (((int)data.ID == EventID_D3D9PresentStart && data.ProviderGuid == D3D9_provider) ||
            ((int)data.ID == EventID_DxgiPresentStart && data.ProviderGuid == DXGI_provider))
                {
                    int pid = data.ProcessID;
                    long t;

                    lock (sync)
                    {
                        t = watch.ElapsedMilliseconds;
                        
                        //if process is not yet in Dictionary, add it
                        if (!frames.ContainsKey(pid))
                        {
                            frames[pid] = new TimestampCollection();

                            string name = "";
                            var proc = Process.GetProcessById(pid);
                            proc.StartInfo.Verb = "runas";
                            if (proc != null)
                            {
                                using (proc)
                                {
                                    name = proc.ProcessName;
                                }
                            }
                            else name = pid.ToString();

                            frames[pid].Name = name;
                        }

                        //store frame timestamp in collection
                        frames[pid].Add(t);
                    }
                }
            };
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            m_EtwSession?.Dispose();
        }

        private void WindowComponent_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            FPSCounterWidget.UpdateWidgetInformationOfFile(this);
        }

        private void WindowComponent_LocationChanged(object sender, EventArgs e)
        {
            FPSCounterWidget.UpdateWidgetInformationOfFile(this);
        }


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

    }

    //helper class to store frame timestamps
    public class TimestampCollection
    {
        const int MAXNUM = 1000;

        public string Name { get; set; }

        readonly List<long> timestamps = new List<long>(MAXNUM + 1);
        readonly object sync = new object();

        //add value to the collection
        public void Add(long timestamp)
        {
            lock (sync)
            {
                timestamps.Add(timestamp);
                if (timestamps.Count > MAXNUM) timestamps.RemoveAt(0);
            }
        }

        //get the number of timestamps withing interval
        public int QueryCount(long from, long to)
        {
            int c = 0;

            lock (sync)
            {
                foreach (var ts in timestamps)
                {
                    if (ts >= from && ts <= to) c++;
                }
            }
            return c;
        }
    }

}
