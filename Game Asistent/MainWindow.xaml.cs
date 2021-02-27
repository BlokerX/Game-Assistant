using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace GameAssistant
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        #region ObjectsAndVaribles

        // NotifyIcon
        System.Windows.Forms.NotifyIcon notifyIcon = new System.Windows.Forms.NotifyIcon()
        {
            Visible = true,
            Text = "Game Assistant",
            Icon = GameAssistant.Properties.Resources.firstIcon
        };

        // Widgets instantions
        ClockForm clockForm;
        PictureForm pictureForm;

        // Paths
        public static string SystemDriveName = "-";
        public static string GameAssistantFolderPath;
        public static string SettingsFolderPath;
        public static string ClockSettingsPath;
        public static string PictureSettingsPath;

        /// <summary>
        /// Select main disk and set paths
        /// </summary>
        public static void SetPaths()
        {
            #region DriveName
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo d in allDrives)
            {
                if (d.IsReady == true)
                {
                    if (Directory.Exists($@"{d.Name}\Users\{Environment.UserName}") && Directory.Exists($@"{d.Name}\Windows"))
                    { SystemDriveName = d.Name; continue; }
                }
            }
            #endregion

            GameAssistantFolderPath = $@"{SystemDriveName}Users\{Environment.UserName}\GameAssistantFiles";
            SettingsFolderPath = $@"{GameAssistantFolderPath}\Settings";
            ClockSettingsPath = $@"{SettingsFolderPath}\ClockWidgetSettings.txt";
            PictureSettingsPath = $@"{SettingsFolderPath}\PictureWidgetSettings.txt";
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of MainWindow
        /// </summary>
        public MainWindow()
        {

            InitializeComponent();
            this.Visibility = Visibility.Collapsed;

            #region FileAndDirectoresSystemStart

            // Set Paths
            SetPaths();

            // Check dires system
            CheckProgramDiresArchitecture();

            #endregion

            #region ConfigureNotifyIcon

            NotifyIconLoadingComponents();

            #endregion

            LoadingWidgets();

        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Check program dires architecture (Create dires for program's files)
        /// </summary>
        public static void CheckProgramDiresArchitecture()
        {
            if (!Directory.Exists(GameAssistantFolderPath))
            {
                Directory.CreateDirectory(GameAssistantFolderPath);
            }

            if (!Directory.Exists(SettingsFolderPath))
            {
                Directory.CreateDirectory(SettingsFolderPath);
            }
        }

        /// <summary>
        /// Check all events and do it in real time if the event is on
        /// </summary>
        public static void DoEvents()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background,
                                                  new Action(delegate { }));
        }

        /// <summary>
        /// Convert Bitmap to BitmapSource
        /// </summary>
        /// <param name="bitmap">Bitmap</param>
        /// <returns>BitmapSource</returns>
        public static BitmapSource GetBitmapSource(Bitmap bitmap)
        {
            BitmapSource bitmapSource = Imaging.CreateBitmapSourceFromHBitmap
            (
                bitmap.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions()
            );

            return bitmapSource;
        }

        #region ClockFormStaticMethods

        /// <summary>
        /// Read and return save informaions about ClockForm
        /// </summary>
        /// <returns>Informations about Save of ClockForm</returns>
        public static ClockInformation DownloadClockInformationOfFile()
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

                    sr.Close();
                    return clockInformation;
                }
            }
            else { return null; }
        }

        /// <summary>
        /// Create ClockForm
        /// </summary>
        /// <returns>ClockForm</returns>
        public static ClockForm CreateClockForm()
        {
            ClockInformation clockInf = DownloadClockInformationOfFile();
            if (clockInf != null)
            {
                ClockForm cf = new ClockForm()
                {
                    Left = clockInf.PositionX,
                    Top = clockInf.PositionY
                };
                cf.Width = clockInf.Width;
                cf.Height = clockInf.Heigth;

                cf.ClockLabel.Opacity = clockInf.ClockOpacity;
                cf.rec1.Opacity = clockInf.BackgroundClockOpacity;
                return cf;
            }
            else
            { return new ClockForm(); }

        }

        /// <summary>
        /// Write in file selected settings about ClockForm
        /// </summary>
        /// <param name="argCF">ClockForm to saving</param>
        public static void UpdateClockInformationOfFile(ClockForm argCF)
        {
            CheckProgramDiresArchitecture();

            if (argCF != null && argCF.IsVisible)
            {
                #region CreateFileOfSettings NowSettings

                using (StreamWriter sw = File.CreateText(ClockSettingsPath))
                {
                    sw.WriteLine(true.ToString());

                    sw.WriteLine(argCF.Left.ToString());
                    sw.WriteLine(argCF.Top.ToString());

                    sw.WriteLine(argCF.Width.ToString());
                    sw.WriteLine(argCF.Height.ToString());

                    sw.WriteLine(argCF.ClockLabel.Opacity.ToString());
                    sw.WriteLine(argCF.rec1.Opacity.ToString());

                    sw.Close();
                }
                #endregion
            }
            else
            {
                ClockInformation cI = DownloadClockInformationOfFile();
                if (File.Exists(ClockSettingsPath) && cI != null)
                {
                    #region DownloadInformationsOfFile

                    ClockInformation clockInf = DownloadClockInformationOfFile();
                    bool _isChosed = false;

                    int _positionX = clockInf.PositionX;
                    int _positionY = clockInf.PositionY;

                    int _width = clockInf.Width;
                    int _heigth = clockInf.Heigth;

                    double _clockOpacity = clockInf.ClockOpacity;
                    double _backgroundClockOpacity = clockInf.BackgroundClockOpacity;

                    #endregion

                    #region OverwriteFile

                    using (StreamWriter sw = File.CreateText(ClockSettingsPath))
                    {
                        sw.WriteLine(_isChosed.ToString());

                        sw.WriteLine(_positionX.ToString());
                        sw.WriteLine(_positionY.ToString());

                        sw.WriteLine(_width.ToString());
                        sw.WriteLine(_heigth.ToString());

                        sw.WriteLine(_clockOpacity.ToString());
                        sw.WriteLine(_backgroundClockOpacity.ToString());

                        sw.Close();
                    }

                    #endregion

                }
                else
                {
                    #region CreateFileOfSettings DeffaultSettings

                    using (StreamWriter sw = File.CreateText(ClockSettingsPath))
                    {
                        sw.WriteLine(false.ToString());

                        sw.WriteLine("100");
                        sw.WriteLine("100");

                        sw.WriteLine("200");
                        sw.WriteLine("62");

                        sw.WriteLine((0.75).ToString());
                        sw.WriteLine((0.50).ToString());

                        sw.Close();

                    }

                    #endregion
                }
            }



        }

        #endregion

        #region PictureFormStaticMethods

        /// <summary>
        /// Read and return save informaions about PictureForm
        /// </summary>
        /// <returns>Informations about Save of PictureForm</returns>
        public static PictureInformation DownloadPictureInformationOfFile()
        {
            if (File.Exists(PictureSettingsPath))
            {
                PictureInformation pictureInformation = new PictureInformation();

                using (StreamReader sr = File.OpenText(PictureSettingsPath))
                {
                    if (true)
                    {
                        string a = sr.ReadLine();
                        if (bool.TryParse(a, out bool aBool))
                        {
                            pictureInformation.IsChosed = aBool;
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
                            pictureInformation.PositionX = aInt;
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
                            pictureInformation.PositionY = aInt;
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
                            pictureInformation.Width = aInt;
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
                            pictureInformation.Heigth = aInt;
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
                            pictureInformation.PictureOpacity = aDouble;
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
                        if (a == "Default")
                        {
                            pictureInformation.PicturePath = "Default";
                        }
                        else if (File.Exists(a))
                        {
                            pictureInformation.PicturePath = a;
                        }
                        else
                        {
                            sr.Close();
                            return null;
                        }
                    }

                    sr.Close();

                    return pictureInformation;
                }
            }
            else { return null; }
        }

        /// <summary>
        /// Create PictureForm
        /// </summary>
        /// <returns>PictureForm</returns>
        public static PictureForm CreatePictureForm()
        {
            PictureInformation pictureInf = DownloadPictureInformationOfFile();

            if (pictureInf != null)
            {
                PictureForm pf = new PictureForm()
                {
                    Left = pictureInf.PositionX,
                    Top = pictureInf.PositionY
                };
                pf.Width = pictureInf.Width;
                pf.Height = pictureInf.Heigth;

                pf.ImageBox.Opacity = pictureInf.PictureOpacity;

                pf.ImagePath = pictureInf.PicturePath;

                if (pf.ImagePath == "Default")
                {
                    pf.ImageBox.Source = GetBitmapSource(Properties.Resources.DefaultImageToImageBox);
                    pf.ImagePath = "Default";
                }
                else if (File.Exists(pictureInf.PicturePath))
                {
                    pf.ImageBox.Source = new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, pictureInf.PicturePath)));
                    pf.ImagePath = pictureInf.PicturePath;
                }
                else
                {
                    pf.ImageBox.Source = GetBitmapSource(Properties.Resources.DefaultImageToImageBox);
                    pf.ImagePath = "Default";
                }
                return pf;
            }
            else
            { return new PictureForm(); }
        }

        /// <summary>
        /// Write in file selected settings about PictureForm
        /// </summary>
        /// <param name="argPF">PictureForm to saving</param>
        public static void UpdatePictureInformationOfFile(PictureForm argPF)
        {
            CheckProgramDiresArchitecture();

            if (argPF != null && argPF.IsVisible) //todo sprawdzanie widoczności
            {
                #region CreateFileOfSettings NowSettings

                using (StreamWriter sw = File.CreateText(PictureSettingsPath))
                {
                    sw.WriteLine(true.ToString());

                    sw.WriteLine(argPF.Left.ToString());
                    sw.WriteLine(argPF.Top.ToString());

                    sw.WriteLine(argPF.Width.ToString());
                    sw.WriteLine(argPF.Height.ToString());

                    sw.WriteLine(argPF.ImageBox.Opacity.ToString());
                    sw.WriteLine(argPF.ImagePath.ToString());

                    sw.Close();
                }

                #endregion
            }
            else
            {
                PictureInformation pI = DownloadPictureInformationOfFile();
                if (File.Exists(PictureSettingsPath) && pI != null)
                {
                    #region DownloadInformationsOfFile

                    PictureInformation pictureInf = DownloadPictureInformationOfFile();
                    bool _isChosed = false;

                    int _positionX = pictureInf.PositionX;
                    int _positionY = pictureInf.PositionY;

                    int _width = pictureInf.Width;
                    int _heigth = pictureInf.Heigth;

                    double _pictureOpacity = pictureInf.PictureOpacity;
                    string _picturePath = pictureInf.PicturePath;

                    #endregion

                    #region OverwriteFile

                    using (StreamWriter sw = File.CreateText(PictureSettingsPath))
                    {
                        sw.WriteLine(_isChosed.ToString());

                        sw.WriteLine(_positionX.ToString());
                        sw.WriteLine(_positionY.ToString());

                        sw.WriteLine(_width.ToString());
                        sw.WriteLine(_heigth.ToString());

                        sw.WriteLine(_pictureOpacity.ToString());
                        sw.WriteLine(_picturePath);

                        sw.Close();
                    }

                    #endregion

                }
                else
                {
                    #region CreateFileOfSettings DeffaultSettings

                    using (StreamWriter sw = File.CreateText(PictureSettingsPath))
                    {
                        sw.WriteLine(false.ToString());

                        sw.WriteLine("100");
                        sw.WriteLine("100");

                        sw.WriteLine("733");
                        sw.WriteLine("413");

                        sw.WriteLine((1).ToString());
                        sw.WriteLine("Default");

                        sw.Close();
                    }

                    #endregion
                }
            }

        }

        #endregion

        #endregion

        #region NotifyIconMethods

        /// <summary>
        /// Loading NotifyIcon's components
        /// </summary>
        private void NotifyIconLoadingComponents()
        {
            notifyIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip
            {
                ShowImageMargin = false,
                ShowCheckMargin = true
            };

            notifyIcon.ContextMenuStrip.Items.Add("Picture", null, NotifyIconContextMenu_Picture_Click);
            notifyIcon.ContextMenuStrip.Items.Add("Clock", null, NotifyIconContextMenu_Clock_Click);
            notifyIcon.ContextMenuStrip.Items.Add("-");
            notifyIcon.ContextMenuStrip.Items.Add("Open Window", null, NotifyIconContextMenu_OpenWindow_Click);
            notifyIcon.ContextMenuStrip.Items.Add("Close", null, NotifyIconContextMenu_Close_Click);
        }

        /// <summary>
        /// On click "Close" button in NotifyIcon
        /// </summary>
        private void NotifyIconContextMenu_Close_Click(object sender, EventArgs e)
        {
            CloseApplication_Click();
        }

        /// <summary>
        /// On click "OpenWindow" button in NotifyIcon
        /// </summary>
        private void NotifyIconContextMenu_OpenWindow_Click(object sender, EventArgs e)
        {
            #region Save settings

            UpdateClockInformationOfFile(this.clockForm);
            UpdatePictureInformationOfFile(this.pictureForm);

            #endregion

            #region SettingsOn

            notifyIcon.Visible = false;
            if (clockForm != null)
            {
                clockForm.Close();
            }
            if (pictureForm != null)
            {
                pictureForm.Close();
            }

            #endregion

            #region SettingsFormRunRegion

            // Create SettingsWindow
            SettingsWindow settingsWindow = new SettingsWindow();

            // Show SettingsWindow
            settingsWindow.Show();

            while (settingsWindow.IsVisible) { DoEvents(); } //todo rozpatrzeć inne opcje badania stanu okna

            // Check close proposition
            if (settingsWindow.IsClosing)
            {
                settingsWindow.Close();
                this.Close();
                goto End;
            }

            settingsWindow.Close();

            #endregion

            #region SettingsOff

            if (notifyIcon != null)
            {
                notifyIcon.Visible = true;
            }
            LoadingWidgets();

        #endregion

        #region EndOfMethod
        End:;
            #endregion
        }

        #region WidgetsButtons

        /// <summary>
        /// On click "Clock" button in NotifyIcon
        /// </summary>
        private void NotifyIconContextMenu_Clock_Click(object sender, EventArgs e)
        {
            OpenOrCloseClockForm();
        }

        /// <summary>
        /// On click "Picture" button in NotifyIcon
        /// </summary>
        private void NotifyIconContextMenu_Picture_Click(object sender, EventArgs e)
        {
            OpenOrClosePictureForm();
        }

        #endregion

        #endregion

        #region UniwersalMethods

        /// <summary>
        /// Loading all wigdets
        /// </summary>
        private void LoadingWidgets()
        {
            if (true)
            {
                ClockInformation clockInf = DownloadClockInformationOfFile();
                if (clockInf != null)
                {
                    if (clockInf.IsChosed == true)
                    {
                        if (clockForm != null)
                        {
                            clockForm.Close();
                        }
                        clockForm = CreateClockForm();
                        clockForm.LocationChanged += ClockForm_LocationChanged;
                        clockForm.SizeChanged += ClockForm_SizeChanged;
                        clockForm.Show();

                        System.Windows.Forms.ToolStripMenuItem menuitem = (System.Windows.Forms.ToolStripMenuItem)notifyIcon.ContextMenuStrip.Items[1];
                        if (menuitem != null)
                            menuitem.Checked = true;
                    }
                    else
                    {
                        clockForm = null;

                        System.Windows.Forms.ToolStripMenuItem menuitem = (System.Windows.Forms.ToolStripMenuItem)notifyIcon.ContextMenuStrip.Items[1];
                        if (menuitem != null)
                            menuitem.Checked = false;
                    }
                }
                else
                {
                    if (clockForm != null)
                    {
                        clockForm.Close();
                    }
                    clockForm = CreateClockForm();
                    clockForm.LocationChanged += ClockForm_LocationChanged;
                    clockForm.SizeChanged += ClockForm_SizeChanged;
                    clockForm.Show();
                    //todo dodaj zapis ustawień okna
                }

                PictureInformation pictureInf = DownloadPictureInformationOfFile();
                if (pictureInf != null)
                {
                    if (pictureInf.IsChosed == true)
                    {
                        if (pictureForm != null)
                        {
                            pictureForm.Close();
                        }

                        pictureForm = CreatePictureForm();
                        pictureForm.LocationChanged += PictureForm_LocationChanged;
                        pictureForm.SizeChanged += PictureForm_SizeChanged;
                        pictureForm.Show();

                        System.Windows.Forms.ToolStripMenuItem menuitem = (System.Windows.Forms.ToolStripMenuItem)notifyIcon.ContextMenuStrip.Items[0];
                        if (menuitem != null)
                            menuitem.Checked = true;
                    }
                    else
                    {
                        pictureForm = null;

                        System.Windows.Forms.ToolStripMenuItem menuitem = (System.Windows.Forms.ToolStripMenuItem)notifyIcon.ContextMenuStrip.Items[0];
                        if (menuitem != null)
                            menuitem.Checked = false;
                    }
                }
                else
                {
                    if (pictureForm != null)
                    {
                        pictureForm.Close();
                    }
                    pictureForm = CreatePictureForm();
                    pictureForm.LocationChanged += PictureForm_LocationChanged;
                    pictureForm.SizeChanged += PictureForm_SizeChanged;
                    pictureForm.Show();
                    //todo dodaj zapis ustawień okna
                }
            }
        }

        #region OpenOrCloseWidgets

        /// <summary>
        /// Open or close clock widget
        /// </summary>
        public void OpenOrCloseClockForm()
        {
            System.Windows.Forms.ToolStripMenuItem menuitem = (System.Windows.Forms.ToolStripMenuItem)notifyIcon.ContextMenuStrip.Items[1];

            if (clockForm != null)
            {
                UpdateClockInformationOfFile(this.clockForm);
                clockForm.Close();
                clockForm = null;
                UpdateClockInformationOfFile(this.clockForm);
                if (menuitem != null)
                    menuitem.Checked = false;
            }
            else
            {
                clockForm = CreateClockForm();
                clockForm.LocationChanged += ClockForm_LocationChanged;
                clockForm.SizeChanged += ClockForm_SizeChanged;
                clockForm.Show();
                UpdateClockInformationOfFile(this.clockForm);
                if (menuitem != null)
                    menuitem.Checked = true;
            }
        }

        /// <summary>
        /// Open or close picture widget
        /// </summary>
        public void OpenOrClosePictureForm()
        {
            System.Windows.Forms.ToolStripMenuItem menuitem = (System.Windows.Forms.ToolStripMenuItem)notifyIcon.ContextMenuStrip.Items[0];

            if (pictureForm != null)
            {
                UpdatePictureInformationOfFile(this.pictureForm);
                pictureForm.Close();
                pictureForm = null;
                UpdatePictureInformationOfFile(this.pictureForm);
                pictureForm = null;
                if (menuitem != null)
                    menuitem.Checked = false;
            }
            else
            {
                pictureForm = CreatePictureForm();
                pictureForm.LocationChanged += PictureForm_LocationChanged;
                pictureForm.SizeChanged += PictureForm_SizeChanged;
                pictureForm.Show();
                UpdatePictureInformationOfFile(this.pictureForm);
                if (menuitem != null)
                    menuitem.Checked = true;
            }
        }

        #endregion

        /// <summary>
        /// [Click Close Button] Save settings and close application
        /// </summary>
        private void CloseApplication_Click()
        {
            // Update select settngs in program's files
            UpdateClockInformationOfFile(this.clockForm);
            UpdatePictureInformationOfFile(this.pictureForm);

            // Close application
            this.Close();
        }

        #endregion

        #region EventsMethods

        private void ClockForm_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateClockInformationOfFile(clockForm);
        }

        private void ClockForm_LocationChanged(object sender, EventArgs e)
        {
            UpdateClockInformationOfFile(clockForm);
        }

        private void PictureForm_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdatePictureInformationOfFile(pictureForm);
        }

        private void PictureForm_LocationChanged(object sender, EventArgs e)
        {
            UpdatePictureInformationOfFile(pictureForm);
        }

        #endregion

        #region InTheEnd

        /// <summary>
        /// On closing MainWindow
        /// </summary>
        private void MainWindowElement_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            #region CloseWidgetsForms

            /// Close ClockForm
            if (clockForm != null)
            {
                this.clockForm.Close();
            }

            /// Close PictureForm
            if (pictureForm != null)
            {
                this.pictureForm.Close();
            }

            #endregion

            /// Delete NotifyIcon
            if (notifyIcon != null)
            {
                notifyIcon.Dispose();
                notifyIcon = null;
            }

        }

        #endregion
    }
}
