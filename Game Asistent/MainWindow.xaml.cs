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
            NoteWidget.NotesDirePath = Path.Combine(GameAssistantFolderPath, "Notes");

            ClockWidget.ClockSettingsPath = $@"{SettingsFolderPath}\ClockWidgetSettings.txt";
            PictureWidget.PictureSettingsPath = $@"{SettingsFolderPath}\PictureWidgetSettings.txt";
            NoteWidget.NoteSettingsPath = Path.Combine(SettingsFolderPath, "NoteWidgetSettings.txt");
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

            if (!Directory.Exists(NoteWidget.NotesDirePath))
            {
                Directory.CreateDirectory(NoteWidget.NotesDirePath);
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
                ClockInformation clockInf = ClockWidget.DownloadWidgetInformationOfFile();
                if (clockInf != null)
                {
                    if (clockInf.IsChosed == true)
                    {
                        if (clockWidget != null)
                        {
                            clockWidget.Close();
                        }
                        clockWidget = ClockWidget.CreateWidget();
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
                    clockWidget = ClockWidget.CreateWidget();
                    clockWidget.Show();
                }

                PictureInformation pictureInf = PictureWidget.DownloadWidgetInformationOfFile();
                if (pictureInf != null)
                {
                    if (pictureInf.IsChosed == true)
                    {
                        if (pictureWidget != null)
                        {
                            pictureWidget.Close();
                        }

                        pictureWidget = PictureWidget.CreateWidget();
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
                    pictureWidget = PictureWidget.CreateWidget();
                    pictureWidget.Show();
                }

                NoteInformation noteInf = NoteWidget.DownloadWidgetInformationOfFile();
                if (noteInf != null)
                {
                    if (noteInf.IsChosed == true)
                    {
                        if (noteWidget != null)
                        {
                            noteWidget.Close();
                        }

                        noteWidget = NoteWidget.CreateWidget();
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
                    noteWidget = NoteWidget.CreateWidget();
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
                ClockWidget.UpdateWidgetInformationOfFile(this.clockWidget);
                clockWidget.Close();
                clockWidget = null;
                ClockWidget.UpdateWidgetInformationOfFile(this.clockWidget);
                if (menuitem != null)
                    menuitem.Checked = false;
            }
            else
            {
                clockWidget = ClockWidget.CreateWidget();
                clockWidget.Show();
                ClockWidget.UpdateWidgetInformationOfFile(this.clockWidget);
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
                PictureWidget.UpdateWidgetInformationOfFile(this.pictureWidget);
                pictureWidget.Close();
                pictureWidget = null;
                PictureWidget.UpdateWidgetInformationOfFile(this.pictureWidget);
                pictureWidget = null;
                if (menuitem != null)
                    menuitem.Checked = false;
            }
            else
            {
                pictureWidget = PictureWidget.CreateWidget();
                pictureWidget.Show();
                PictureWidget.UpdateWidgetInformationOfFile(this.pictureWidget);
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
                NoteWidget.UpdateWidgetInformationOfFile(this.noteWidget);
                noteWidget.Close();
                noteWidget = null;
                NoteWidget.UpdateWidgetInformationOfFile(this.noteWidget);
                noteWidget = null;
                if (menuitem != null)
                    menuitem.Checked = false;
            }
            else
            {
                noteWidget = NoteWidget.CreateWidget();
                noteWidget.Show();
                NoteWidget.UpdateWidgetInformationOfFile(this.noteWidget);
                if (menuitem != null)
                    menuitem.Checked = true;
            }
        }

        #endregion

        public static void SaveWidgetsSettings(ClockWidget clockWidget, PictureWidget pictureWidget, NoteWidget noteWidget)
        {
            ClockWidget.UpdateWidgetInformationOfFile(clockWidget);
            PictureWidget.UpdateWidgetInformationOfFile(pictureWidget);
            NoteWidget.UpdateWidgetInformationOfFile(noteWidget);
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
