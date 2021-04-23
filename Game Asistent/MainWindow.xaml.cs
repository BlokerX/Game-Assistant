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
        ClockWidget clockWidget;
        PictureWidget pictureWidget;
        NoteWidget noteWidget;

        // Paths
        public static string SystemDriveName = "-";

        public static string GameAssistantFolderPath;

        public static string SettingsFolderPath;
        public static string NotesDirePath;

        public static string PictureSettingsPath;
        public static string NoteSettingsPath;
        public static string ClockSettingsPath;

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
            NotesDirePath = Path.Combine(GameAssistantFolderPath, "Notes");

            ClockSettingsPath = $@"{SettingsFolderPath}\ClockWidgetSettings.txt";
            PictureSettingsPath = $@"{SettingsFolderPath}\PictureWidgetSettings.txt";
            NoteSettingsPath = Path.Combine(SettingsFolderPath, "NoteWidgetSettings.txt");
        }

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

            if (!Directory.Exists(NotesDirePath))
            {
                Directory.CreateDirectory(NotesDirePath);
            }
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

            NotifyIconLoadingComponents();
            LoadingWidgets();

        }

        #endregion

        #region WidgetsMethods

        #region ClockWidgetStaticMethods

        /// <summary>
        /// Read and return save informaions about ClockWidget
        /// </summary>
        /// <returns>Informations about Save of ClockWidget</returns>
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
        public static ClockWidget CreateClockWidget()
        {
            ClockInformation clockInf = DownloadClockInformationOfFile();
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

                return cf;
            }
            else
            { return new ClockWidget(); }

        }

        /// <summary>
        /// Write in file selected settings about ClockWidget
        /// </summary>
        /// <param name="argCF">ClockWidget to saving</param>
        public static void UpdateClockInformationOfFile(ClockWidget argCF)
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

                    sw.WriteLine(new System.Windows.Media.ColorConverter().ConvertToString(BrushToColorMedia(argCF.rec1.Fill)));

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

                    string _backgroundColor = clockInf.BackgroundColor.ToString();

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
                        sw.WriteLine(_backgroundColor.ToString());

                        sw.Close();
                    }

                    #endregion

                }
                else
                {
                    #region CreateFileOfSettings DeffaultSettings

                    using (StreamWriter sw = File.CreateText(ClockSettingsPath))
                    {
                        sw.WriteLine(true.ToString());

                        sw.WriteLine("100");
                        sw.WriteLine("100");

                        sw.WriteLine("200");
                        sw.WriteLine("62");

                        sw.WriteLine((0.75).ToString());
                        sw.WriteLine((0.50).ToString());

                        sw.WriteLine("#FFFFFFB5");

                        sw.Close();
                    }

                    #endregion
                }
            }



        }
        #endregion

        #region PictureWidgetStaticMethods

        /// <summary>
        /// Read and return save informaions about PictureWidget
        /// </summary>
        /// <returns>Informations about Save of PictureWidget</returns>
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
        /// Create PictureWidget
        /// </summary>
        /// <returns>PictureWidget</returns>
        public static PictureWidget CreatePictureWidget()
        {
            PictureInformation pictureInf = DownloadPictureInformationOfFile();

            if (pictureInf != null)
            {
                PictureWidget pf = new PictureWidget()
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
            { return new PictureWidget(); }
        }

        /// <summary>
        /// Write in file selected settings about PictureWidget
        /// </summary>
        /// <param name="argPF">PictureWidget to saving</param>
        public static void UpdatePictureInformationOfFile(PictureWidget argPF)
        {
            CheckProgramDiresArchitecture();

            if (argPF != null && argPF.IsVisible)
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
                        sw.WriteLine(true.ToString());

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

        #region NoteWidgetStaticMethods

        /// <summary>
        /// Read and return save informaions about NoteWidget
        /// </summary>
        /// <returns>Informations about Save of NoteWidget</returns>
        public static NoteInformation DownloadNoteInformationOfFile()
        {
            if (File.Exists(NoteSettingsPath))
            {
                NoteInformation noteInformation = new NoteInformation();

                using (StreamReader sr = File.OpenText(NoteSettingsPath))
                {
                    if (true)
                    {
                        string a = sr.ReadLine();
                        if (bool.TryParse(a, out bool aBool))
                        {
                            noteInformation.IsChosed = aBool;
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
                            noteInformation.PositionX = aInt;
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
                            noteInformation.PositionY = aInt;
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
                            noteInformation.Width = aInt;
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
                            noteInformation.Heigth = aInt;
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
                            noteInformation.BackgroundOpacity = aDouble;
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
                                noteInformation.BackgroundColor = color;
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
                        if (double.TryParse(a, out double aDouble))
                        {
                            noteInformation.FontOpacity = aDouble;
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
                                noteInformation.FontColor = color;
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
                            System.Windows.Media.FontFamily fontFamily = (System.Windows.Media.FontFamily)new System.Windows.Media.FontFamilyConverter().ConvertFromString(a);
                            if (fontFamily != null)
                            {
                                noteInformation.FontFamily = fontFamily;
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
                        if (double.TryParse(a, out double aDouble))
                        {
                            noteInformation.FontSize = aDouble;
                        }
                        else
                        {
                            sr.Close();
                            return null;
                        }
                    }

                    sr.Close();

                    return noteInformation;
                }
            }
            else { return null; }
        }

        /// <summary>
        /// Create NoteWidget
        /// </summary>
        /// <returns>NoteWidget</returns>
        public static NoteWidget CreateNoteWidget()
        {
            NoteInformation noteInf = DownloadNoteInformationOfFile();

            if (noteInf != null)
            {
                NoteWidget nf = new NoteWidget()
                {
                    Left = noteInf.PositionX,
                    Top = noteInf.PositionY
                };
                nf.Width = noteInf.Width;
                nf.Height = noteInf.Heigth;

                nf.Rec1.Opacity = noteInf.BackgroundOpacity;

                nf.Rec1.Fill = new System.Windows.Media.SolidColorBrush(noteInf.BackgroundColor);

                nf.TextBox1.Opacity = noteInf.FontOpacity;

                nf.TextBox1.Foreground = new System.Windows.Media.SolidColorBrush(noteInf.FontColor);

                nf.TextBox1.FontFamily = noteInf.FontFamily;

                nf.TextBox1.FontSize = noteInf.FontSize;

                return nf;
            }
            else
            { return new NoteWidget(); }
        }

        /// <summary>
        /// Write in file selected settings about NoteWidget
        /// </summary>
        /// <param name="argNF">NoteWidget to saving</param>
        public static void UpdateNoteInformationOfFile(NoteWidget argNF)
        {
            CheckProgramDiresArchitecture();

            if (argNF != null && argNF.IsVisible)
            {
                #region CreateFileOfSettings NowSettings

                using (StreamWriter sw = File.CreateText(NoteSettingsPath))
                {
                    sw.WriteLine(true.ToString());

                    sw.WriteLine(argNF.Left.ToString());
                    sw.WriteLine(argNF.Top.ToString());

                    sw.WriteLine(argNF.Width.ToString());
                    sw.WriteLine(argNF.Height.ToString());

                    sw.WriteLine(argNF.Rec1.Opacity);
                    sw.WriteLine(new System.Windows.Media.ColorConverter().ConvertToString(BrushToColorMedia(argNF.Rec1.Fill)));

                    sw.WriteLine(argNF.TextBox1.Opacity.ToString());

                    sw.WriteLine(new System.Windows.Media.ColorConverter().ConvertToString(BrushToColorMedia(argNF.TextBox1.Foreground)));

                    sw.WriteLine(argNF.TextBox1.FontFamily.ToString());

                    sw.WriteLine(argNF.TextBox1.FontSize.ToString());

                    sw.Close();
                }

                #endregion
            }
            else
            {
                NoteInformation nI = DownloadNoteInformationOfFile();
                if (File.Exists(NoteSettingsPath) && nI != null)
                {
                    #region DownloadInformationsOfFile

                    NoteInformation noteInf = DownloadNoteInformationOfFile();
                    bool _isChosed = false;

                    int _positionX = noteInf.PositionX;
                    int _positionY = noteInf.PositionY;

                    int _width = noteInf.Width;
                    int _heigth = noteInf.Heigth;

                    double _backgroundOpacity = noteInf.BackgroundOpacity;
                    System.Windows.Media.Color _backgroundColor = noteInf.BackgroundColor;

                    double _fontOpacity = noteInf.FontOpacity;
                    System.Windows.Media.Color _fontColor = noteInf.FontColor;
                    System.Windows.Media.FontFamily _fontFamily = noteInf.FontFamily;
                    double _fontSize = noteInf.FontSize;

                    #endregion

                    #region OverwriteFile

                    using (StreamWriter sw = File.CreateText(NoteSettingsPath))
                    {
                        sw.WriteLine(_isChosed.ToString());

                        sw.WriteLine(_positionX.ToString());
                        sw.WriteLine(_positionY.ToString());

                        sw.WriteLine(_width.ToString());
                        sw.WriteLine(_heigth.ToString());

                        sw.WriteLine(_backgroundOpacity.ToString());
                        sw.WriteLine(_backgroundColor.ToString());

                        sw.WriteLine(_fontOpacity.ToString());
                        sw.WriteLine(_fontColor.ToString());
                        sw.WriteLine(new System.Windows.Media.FontFamilyConverter().ConvertToString(_fontFamily));
                        sw.WriteLine(_fontSize.ToString());

                        sw.Close();
                    }

                    #endregion

                }
                else
                {
                    #region CreateFileOfSettings DeffaultSettings

                    using (StreamWriter sw = File.CreateText(NoteSettingsPath))
                    {
                        sw.WriteLine(true.ToString());

                        sw.WriteLine("104");
                        sw.WriteLine("104");

                        sw.WriteLine("302");
                        sw.WriteLine("330");

                        sw.WriteLine((0.5).ToString());
                        sw.WriteLine("#FFF3E126");

                        sw.WriteLine((0.75).ToString());
                        sw.WriteLine("#FF000000");
                        sw.WriteLine("Century Gothic");
                        sw.WriteLine("20");

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
            notifyIcon.ContextMenuStrip.Items.Add("Note", null, NotifyIconContextMenu_Note_Click);
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
            SaveWidgetsSettings(clockWidget, pictureWidget, noteWidget);

            #region SettingsOn

            /*/ Notify icon /*/
            notifyIcon.Visible = false;

            /*/ Widgets /*/
            CloseWidgets(clockWidget, pictureWidget, noteWidget);

            #endregion

            #region SettingsFormRunRegion

            // Create SettingsWindow
            SettingsWindow settingsWindow = new SettingsWindow();

            // Show SettingsWindow
            settingsWindow.Show();

            while (settingsWindow.IsVisible) { DoEvents(); }

            // Check close proposition
            if (settingsWindow.IsClosing)
            {
                settingsWindow?.Close();
                this?.Close();
                goto End;
            }

            settingsWindow?.Close();

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
            OpenOrCloseClockWidget();
        }

        /// <summary>
        /// On click "Picture" button in NotifyIcon
        /// </summary>
        private void NotifyIconContextMenu_Picture_Click(object sender, EventArgs e)
        {
            OpenOrClosePictureWidget();
        }

        /// <summary>
        /// On click "Note" button in NotifyIcon
        /// </summary>
        private void NotifyIconContextMenu_Note_Click(object sender, EventArgs e)
        {
            OpenOrCloseNoteWidget();
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
                        if (clockWidget != null)
                        {
                            clockWidget.Close();
                        }
                        clockWidget = CreateClockWidget();
                        clockWidget.Show();

                        System.Windows.Forms.ToolStripMenuItem menuitem = (System.Windows.Forms.ToolStripMenuItem)notifyIcon.ContextMenuStrip.Items[1];
                        if (menuitem != null)
                            menuitem.Checked = true;
                    }
                    else
                    {
                        clockWidget = null;

                        System.Windows.Forms.ToolStripMenuItem menuitem = (System.Windows.Forms.ToolStripMenuItem)notifyIcon.ContextMenuStrip.Items[1];
                        if (menuitem != null)
                            menuitem.Checked = false;
                    }
                }
                else
                {
                    if (clockWidget != null)
                    {
                        clockWidget.Close();
                    }
                    clockWidget = CreateClockWidget();
                    clockWidget.Show();
                }

                PictureInformation pictureInf = DownloadPictureInformationOfFile();
                if (pictureInf != null)
                {
                    if (pictureInf.IsChosed == true)
                    {
                        if (pictureWidget != null)
                        {
                            pictureWidget.Close();
                        }

                        pictureWidget = CreatePictureWidget();
                        pictureWidget.Show();

                        System.Windows.Forms.ToolStripMenuItem menuitem = (System.Windows.Forms.ToolStripMenuItem)notifyIcon.ContextMenuStrip.Items[0];
                        if (menuitem != null)
                            menuitem.Checked = true;
                    }
                    else
                    {
                        pictureWidget = null;

                        System.Windows.Forms.ToolStripMenuItem menuitem = (System.Windows.Forms.ToolStripMenuItem)notifyIcon.ContextMenuStrip.Items[0];
                        if (menuitem != null)
                            menuitem.Checked = false;
                    }
                }
                else
                {
                    if (pictureWidget != null)
                    {
                        pictureWidget.Close();
                    }
                    pictureWidget = CreatePictureWidget();
                    pictureWidget.Show();
                }

                NoteInformation noteInf = DownloadNoteInformationOfFile();
                if (noteInf != null)
                {
                    if (noteInf.IsChosed == true)
                    {
                        if (noteWidget != null)
                        {
                            noteWidget.Close();
                        }

                        noteWidget = CreateNoteWidget();
                        noteWidget.Show();

                        System.Windows.Forms.ToolStripMenuItem menuitem = (System.Windows.Forms.ToolStripMenuItem)notifyIcon.ContextMenuStrip.Items[2];
                        if (menuitem != null)
                            menuitem.Checked = true;
                    }
                    else
                    {
                        noteWidget = null;

                        System.Windows.Forms.ToolStripMenuItem menuitem = (System.Windows.Forms.ToolStripMenuItem)notifyIcon.ContextMenuStrip.Items[2];
                        if (menuitem != null)
                            menuitem.Checked = false;
                    }
                }
                else
                {
                    if (noteWidget != null)
                    {
                        noteWidget.Close();
                    }
                    noteWidget = CreateNoteWidget();
                    noteWidget.Show();
                }
            }
        }

        #region OpenOrCloseWidgets

        /// <summary>
        /// Open or close clock widget
        /// </summary>
        public void OpenOrCloseClockWidget()
        {
            System.Windows.Forms.ToolStripMenuItem menuitem = (System.Windows.Forms.ToolStripMenuItem)notifyIcon.ContextMenuStrip.Items[1];

            if (clockWidget != null)
            {
                UpdateClockInformationOfFile(this.clockWidget);
                clockWidget.Close();
                clockWidget = null;
                UpdateClockInformationOfFile(this.clockWidget);
                if (menuitem != null)
                    menuitem.Checked = false;
            }
            else
            {
                clockWidget = CreateClockWidget();
                clockWidget.Show();
                UpdateClockInformationOfFile(this.clockWidget);
                if (menuitem != null)
                    menuitem.Checked = true;
            }
        }

        /// <summary>
        /// Open or close picture widget
        /// </summary>
        public void OpenOrClosePictureWidget()
        {
            System.Windows.Forms.ToolStripMenuItem menuitem = (System.Windows.Forms.ToolStripMenuItem)notifyIcon.ContextMenuStrip.Items[0];

            if (pictureWidget != null)
            {
                UpdatePictureInformationOfFile(this.pictureWidget);
                pictureWidget.Close();
                pictureWidget = null;
                UpdatePictureInformationOfFile(this.pictureWidget);
                pictureWidget = null;
                if (menuitem != null)
                    menuitem.Checked = false;
            }
            else
            {
                pictureWidget = CreatePictureWidget();
                pictureWidget.Show();
                UpdatePictureInformationOfFile(this.pictureWidget);
                if (menuitem != null)
                    menuitem.Checked = true;
            }
        }

        /// <summary>
        /// Open or close note widget
        /// </summary>
        public void OpenOrCloseNoteWidget()
        {
            System.Windows.Forms.ToolStripMenuItem menuitem = (System.Windows.Forms.ToolStripMenuItem)notifyIcon.ContextMenuStrip.Items[2];

            if (noteWidget != null)
            {
                UpdateNoteInformationOfFile(this.noteWidget);
                noteWidget.Close();
                noteWidget = null;
                UpdateNoteInformationOfFile(this.noteWidget);
                noteWidget = null;
                if (menuitem != null)
                    menuitem.Checked = false;
            }
            else
            {
                noteWidget = CreateNoteWidget();
                noteWidget.Show();
                UpdateNoteInformationOfFile(this.noteWidget);
                if (menuitem != null)
                    menuitem.Checked = true;
            }
        }

        #endregion

        public static void SaveWidgetsSettings(ClockWidget clockWidget, PictureWidget pictureWidget, NoteWidget noteWidget)
        {
            UpdateClockInformationOfFile(clockWidget);
            UpdatePictureInformationOfFile(pictureWidget);
            UpdateNoteInformationOfFile(noteWidget);
        }

        public static void CloseWidgets(ClockWidget clockWidget, PictureWidget pictureWidget, NoteWidget noteWidget)
        {
            /// Close ClockWidget
            clockWidget?.Close();

            /// Close PictureWidget
            pictureWidget?.Close();

            /// Close NoteWidget
            noteWidget?.Close();
        }

        private void CloseWidgetsWithNotifyIcon()
        {
            CloseWidgets(clockWidget, pictureWidget, noteWidget);

            /// Delete NotifyIcon
            notifyIcon?.Dispose();
            notifyIcon = null;
        }

        /// <summary>
        /// [Click Close Button] Save settings and close application
        /// </summary>
        private void CloseApplication_Click()
        {
            // Update select settngs in program's files
            SaveWidgetsSettings(clockWidget, pictureWidget, noteWidget);

            // Close application
            this?.Close();
        }

        /// <summary>
        /// On closing MainWindow
        /// </summary>
        private void MainWindowElement_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CloseWidgetsWithNotifyIcon();
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Check all events and do it in real time if the event is on
        /// </summary>
        public static void DoEvents()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background,
                                                  new Action(delegate { }));
        }

        /// <summary>
        /// Convert Brush to Color (System.Windowd.Media)
        /// </summary>
        /// <param name="brush">Brush to convert</param>
        /// <returns>Color of Brush</returns>
        public static System.Windows.Media.Color BrushToColorMedia(System.Windows.Media.Brush brush)
        {
            System.Windows.Media.SolidColorBrush newBrush = (System.Windows.Media.SolidColorBrush)brush;
            System.Windows.Media.Color myColorFromBrush = newBrush.Color;
            return myColorFromBrush;
        }

        /// <summary>
        /// Convert System.Windows.Media.Color to System.Drawing.Color
        /// </summary>
        /// <param name="colorMedia">Color to convert</param>
        /// <returns>Color of return</returns>
        public static System.Drawing.Color ColorMediaToDrawing(System.Windows.Media.Color colorMedia)
        {
            return System.Drawing.Color.FromArgb(colorMedia.A, colorMedia.R, colorMedia.G, colorMedia.B);
        }

        /// <summary>
        /// Convert System.Drawing.Color to System.Windows.Media.Color
        /// </summary>
        /// <param name="colorDrawing">Color to convert</param>
        /// <returns>Color of return</returns>
        public static System.Windows.Media.Color ColorDrawingToMedia(System.Drawing.Color colorDrawing)
        {
            return System.Windows.Media.Color.FromArgb(colorDrawing.A, colorDrawing.R, colorDrawing.G, colorDrawing.B);
        }

        /// <summary>
        /// Convert System.Drawing.Color to System.Windows.Media.Color
        /// </summary>
        /// <param name="fontFamilyMedia">Color to convert</param>
        /// <returns>Color of return</returns>
        public static System.Drawing.FontFamily FontFamilyMediaToDrawing(System.Windows.Media.FontFamily fontFamilyMedia)
        {
            return new System.Drawing.FontFamily(fontFamilyMedia.ToString());
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

        #endregion
    }
}
