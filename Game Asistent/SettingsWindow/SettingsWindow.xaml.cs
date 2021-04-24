using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace GameAssistant
{
    /// <summary>
    /// Logika interakcji dla klasy SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        #region ObjectsAndVaribles

        // Instation of widgets
        public ClockWidget clockWidget = null;
        public PictureWidget pictureWidget = null;
        public NoteWidget noteWidget = null;

        // Varibles
        /// <summary>
        /// If is true, The Application is close after end settings window
        /// </summary>
        public bool IsClosing = false;

        #endregion

        #region Constructor

        /// <summary>
        /// Construktor of SettingsWindow
        /// </summary>
        public SettingsWindow()
        {
            InitializeComponent();
            // Set Paths
            MainWindow.SetPaths();
            // Check dires system
            MainWindow.CheckProgramDiresArchitecture();
            LoadingWidgets();
        }

        private void LoadingWidgets()
        {
            #region ClockWidgetLoading

            ClockInformation clockInf = ClockWidget.DownloadWidgetInformationOfFile();

            if (clockInf != null)
            {
                if (clockInf.IsChosed == true)
                {
                    clockWidget = ClockWidget.CreateWidget();
                    clockWidget.Show();
                }
                else
                {
                    clockWidget = null;
                }

                switch (clockInf.ClockOpacity)
                {
                    case 1:
                        this.ClockOpacityComboBox.SelectedIndex = 0;
                        break;
                    case 0.75:
                        this.ClockOpacityComboBox.SelectedIndex = 1;
                        break;
                    case 0.5:
                        this.ClockOpacityComboBox.SelectedIndex = 2;
                        break;
                    case 0.25:
                        this.ClockOpacityComboBox.SelectedIndex = 3;
                        break;
                    case 0:
                        this.ClockOpacityComboBox.SelectedIndex = 4;
                        break;
                    default:
                        this.ClockOpacityComboBox.SelectedIndex = 1;
                        break;
                }

                switch (clockInf.BackgroundClockOpacity)
                {
                    case 1:
                        this.ClockBackgroundOpacityComboBox.SelectedIndex = 0;
                        break;
                    case 0.75:
                        this.ClockBackgroundOpacityComboBox.SelectedIndex = 1;
                        break;
                    case 0.5:
                        this.ClockBackgroundOpacityComboBox.SelectedIndex = 2;
                        break;
                    case 0.25:
                        this.ClockBackgroundOpacityComboBox.SelectedIndex = 3;
                        break;
                    case 0:
                        this.ClockBackgroundOpacityComboBox.SelectedIndex = 4;
                        break;
                    default:
                        this.ClockBackgroundOpacityComboBox.SelectedIndex = 1;
                        break;
                }

            }
            else
            {
                clockWidget = ClockWidget.CreateWidget();
                clockWidget.Show();
            }

            if (clockWidget != null)
            {
                clockWidget.IsAllowDrag = true;
                clockWidget.ResizeMode = ResizeMode.CanResizeWithGrip;
            }

            #endregion

            #region PictureWidgetLoading

            PictureInformation pictureInf = PictureWidget.DownloadWidgetInformationOfFile();

            if (pictureInf != null)
            {
                if (pictureInf.IsChosed == true)
                {
                    pictureWidget = PictureWidget.CreateWidget();
                    pictureWidget.Show();
                }
                else
                {
                    pictureWidget = null;
                }

                switch (pictureInf.PictureOpacity)
                {
                    case 1:
                        this.PictureOpacityComboBox.SelectedIndex = 0;
                        break;
                    case 0.75:
                        this.PictureOpacityComboBox.SelectedIndex = 1;
                        break;
                    case 0.5:
                        this.PictureOpacityComboBox.SelectedIndex = 2;
                        break;
                    case 0.25:
                        this.PictureOpacityComboBox.SelectedIndex = 3;
                        break;
                    case 0:
                        this.PictureOpacityComboBox.SelectedIndex = 4;
                        break;
                    default:
                        this.PictureOpacityComboBox.SelectedIndex = 0;
                        break;
                }

                this.PicturePathTextBox.Text = pictureInf.PicturePath;

            }
            else
            {
                pictureWidget = PictureWidget.CreateWidget();
                pictureWidget.Show();
            }
            if (pictureWidget != null)
            {
                pictureWidget.IsAllowDrag = true;
                pictureWidget.ResizeMode = ResizeMode.CanResizeWithGrip;
                pictureWidget.rec1.Visibility = Visibility.Visible;
            }

            #endregion

            #region NoteWidgetLoading

            NoteInformation noteInf = NoteWidget.DownloadWidgetInformationOfFile();

            if (noteInf != null)
            {
                if (noteInf.IsChosed == true)
                {
                    noteWidget = NoteWidget.CreateWidget();
                    noteWidget.Show();
                }
                else
                {
                    noteWidget = null;
                }

                switch (noteInf.BackgroundOpacity)
                {
                    case 1:
                        this.NoteBackgroundOpacityComboBox.SelectedIndex = 0;
                        break;
                    case 0.75:
                        this.NoteBackgroundOpacityComboBox.SelectedIndex = 1;
                        break;
                    case 0.5:
                        this.NoteBackgroundOpacityComboBox.SelectedIndex = 2;
                        break;
                    case 0.25:
                        this.NoteBackgroundOpacityComboBox.SelectedIndex = 3;
                        break;
                    case 0:
                        this.NoteBackgroundOpacityComboBox.SelectedIndex = 4;
                        break;
                    default:
                        this.NoteBackgroundOpacityComboBox.SelectedIndex = 0;
                        break;
                }

                switch (noteInf.FontOpacity)
                {
                    case 1:
                        this.NoteFontOpacityComboBox.SelectedIndex = 0;
                        break;
                    case 0.75:
                        this.NoteFontOpacityComboBox.SelectedIndex = 1;
                        break;
                    case 0.5:
                        this.NoteFontOpacityComboBox.SelectedIndex = 2;
                        break;
                    case 0.25:
                        this.NoteFontOpacityComboBox.SelectedIndex = 3;
                        break;
                    case 0:
                        this.NoteFontOpacityComboBox.SelectedIndex = 4;
                        break;
                    default:
                        this.NoteFontOpacityComboBox.SelectedIndex = 0;
                        break;
                }

            }
            else
            {
                noteWidget = NoteWidget.CreateWidget();
                noteWidget.Show();
            }

            if (noteWidget != null)
            {
                noteWidget.IsAllowDrag = true;
                noteWidget.ResizeMode = ResizeMode.CanResizeWithGrip;
            }

            #endregion

        }

        #endregion

        #region ControlsEvents

        #region ChosePicture

        /// <summary>
        /// Set picture in PictureWidget 
        /// </summary>
        private void ChosePictureButton_Click(object sender, RoutedEventArgs e)
        {
            if (pictureWidget != null)
            {
                using
                (
                System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog()
                {
                    Filter = "Png files (*.png)|*.png|Jpg files (*.jpg)|*.png|All files (*.*)|*.*",
                    FilterIndex = 3
                }
                )
                {
                    try
                    {
                        if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            pictureWidget.ImageBox.Source = new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, openFileDialog.FileName)));
                            PicturePathTextBox.Text = openFileDialog.FileName;
                            pictureWidget.ImagePath = openFileDialog.FileName;
                            PictureWidget.UpdateWidgetInformationOfFile(pictureWidget);
                        }
                    }
                    catch { }
                }
            }
            else
            {
                MessageBox.Show
                    (
                    "If you want to change the picture, you need to enable the Picture widget!",
                    "The picture cannot be changed:",
                    MessageBoxButton.OK,
                    MessageBoxImage.Hand,
                    MessageBoxResult.OK
                    );
            }
        }

        #endregion

        #region VisibleCheckBox

        #region ClockVisibleCheckBox

        /// <summary>
        /// It does when ClockVisibleCheckBox checked 
        /// </summary>
        private void ClockVisibleCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (clockWidget == null)
            {
                clockWidget = ClockWidget.CreateWidget();
                if (clockWidget != null)
                {
                    clockWidget.IsAllowDrag = true;
                    clockWidget.ResizeMode = ResizeMode.CanResizeWithGrip;
                }
                clockWidget.Show();
                ClockWidget.UpdateWidgetInformationOfFile(clockWidget);
            }
        }

        /// <summary>
        /// It does when ClockVisibleCheckBox unchecked 
        /// </summary>
        private void ClockVisibleCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (clockWidget != null)
            {
                ClockWidget.UpdateWidgetInformationOfFile(clockWidget);
                clockWidget.Close();
                clockWidget = null;
                ClockWidget.UpdateWidgetInformationOfFile(clockWidget);
            }
        }

        /// <summary>
        /// It do when ClockVisibleCheckBox Loaded
        /// </summary>
        private void ClockVisibleCheckBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (clockWidget != null)
            {
                this.ClockVisibleCheckBox.IsChecked = true;
            }
            else if (clockWidget == null)
            {
                this.ClockVisibleCheckBox.IsChecked = false;
            }
        }

        #endregion

        #region PictureVisibleCheckBox

        /// <summary>
        /// It does when PictureBoxVisibleCheckBox checked 
        /// </summary>
        private void PictureBoxVisibleCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (pictureWidget == null)
            {
                pictureWidget = PictureWidget.CreateWidget();
                if (pictureWidget != null)
                {
                    pictureWidget.IsAllowDrag = true;
                    pictureWidget.ResizeMode = ResizeMode.CanResizeWithGrip;
                    pictureWidget.rec1.Visibility = Visibility.Visible;
                }
                pictureWidget.Show();
                PictureWidget.UpdateWidgetInformationOfFile(pictureWidget);
            }
        }

        /// <summary>
        /// It does when PictureBoxVisibleCheckBox unchecked 
        /// </summary>
        private void PictureBoxVisibleCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (pictureWidget != null)
            {
                PictureWidget.UpdateWidgetInformationOfFile(pictureWidget);
                pictureWidget.Close();
                pictureWidget = null;
                PictureWidget.UpdateWidgetInformationOfFile(pictureWidget);
            }
        }

        /// <summary>
        /// It do when PictureBoxVisibleCheckBox Loaded
        /// </summary>
        private void PictureBoxVisibleCheckBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (pictureWidget != null)
            {
                this.PictureBoxVisibleCheckBox.IsChecked = true;
            }
            else if (pictureWidget == null)
            {
                this.PictureBoxVisibleCheckBox.IsChecked = false;
            }
        }

        #endregion

        #region NoteVisibleCheckBox

        /// <summary>
        /// It does when NoteVisibleCheckBox checked 
        /// </summary>
        private void NoteVisibleCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (noteWidget == null)
            {
                noteWidget = NoteWidget.CreateWidget();
                if (noteWidget != null)
                {
                    noteWidget.IsAllowDrag = true;
                    noteWidget.ResizeMode = ResizeMode.CanResizeWithGrip;
                }
                noteWidget?.Show();
                NoteWidget.UpdateWidgetInformationOfFile(noteWidget);
            }
        }

        /// <summary>
        /// It does when NoteVisibleCheckBox unchecked 
        /// </summary>
        private void NoteVisibleCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (noteWidget != null)
            {
                NoteWidget.UpdateWidgetInformationOfFile(noteWidget);
                noteWidget?.Close();
                noteWidget = null;
                NoteWidget.UpdateWidgetInformationOfFile(noteWidget);
            }
        }

        /// <summary>
        /// It do when NoteVisibleCheckBox Loaded
        /// </summary>
        private void NoteVisibleCheckBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (noteWidget != null)
            {
                this.NoteVisibleCheckBox.IsChecked = true;
            }
            else if (noteWidget == null)
            {
                this.NoteVisibleCheckBox.IsChecked = false;
            }
        }

        #endregion

        #endregion

        #region OpacitiesComboBoxes

        #region ClockWidgetOpacity

        private void ClockOpacityComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (clockWidget != null)
            {
                switch (ClockOpacityComboBox.SelectedIndex)
                {
                    case 0:
                        clockWidget.ClockLabel.Opacity = 1;
                        break;
                    case 1:
                        clockWidget.ClockLabel.Opacity = 0.75;
                        break;
                    case 2:
                        clockWidget.ClockLabel.Opacity = 0.5;
                        break;
                    case 3:
                        clockWidget.ClockLabel.Opacity = 0.25;
                        break;
                    case 4:
                        clockWidget.ClockLabel.Opacity = 0;
                        break;
                }
                ClockWidget.UpdateWidgetInformationOfFile(clockWidget);
            }
        }

        private void ClockBackgroundOpacityComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (clockWidget != null)
            {
                switch (ClockBackgroundOpacityComboBox.SelectedIndex)
                {
                    case 0:
                        clockWidget.rec1.Opacity = 1;
                        break;
                    case 1:
                        clockWidget.rec1.Opacity = 0.75;
                        break;
                    case 2:
                        clockWidget.rec1.Opacity = 0.5;
                        break;
                    case 3:
                        clockWidget.rec1.Opacity = 0.25;
                        break;
                    case 4:
                        clockWidget.rec1.Opacity = 0;
                        break;
                }
                ClockWidget.UpdateWidgetInformationOfFile(clockWidget);
            }
        }

        #endregion

        #region PictureWidgetOpacity

        private void PictureOpacityComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (pictureWidget != null)
            {
                switch (PictureOpacityComboBox.SelectedIndex)
                {
                    case 0:
                        pictureWidget.ImageBox.Opacity = 1;
                        break;
                    case 1:
                        pictureWidget.ImageBox.Opacity = 0.75;
                        break;
                    case 2:
                        pictureWidget.ImageBox.Opacity = 0.5;
                        break;
                    case 3:
                        pictureWidget.ImageBox.Opacity = 0.25;
                        break;
                    case 4:
                        pictureWidget.ImageBox.Opacity = 0;
                        break;
                }
                PictureWidget.UpdateWidgetInformationOfFile(pictureWidget);
            }
        }

        #endregion

        #region NoteOptions

        private void ChoseNoteFontSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            if (noteWidget != null)
            {
                System.Drawing.FontFamily font1 = MainWindow.FontFamilyMediaToDrawing(noteWidget.TextBox1.FontFamily);

                try
                {
                    using (System.Windows.Forms.FontDialog fontDialog = new System.Windows.Forms.FontDialog()
                    {
                        Color = MainWindow.ColorMediaToDrawing(MainWindow.BrushToColorMedia(noteWidget.TextBox1.Foreground)),
                        Font = new System.Drawing.Font(font1.Name, float.Parse(noteWidget.TextBox1.FontSize.ToString())),
                        ShowEffects = false
                    })
                    {
                        if (fontDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            // todo naprawić font family
                            noteWidget.TextBox1.FontFamily = (System.Windows.Media.FontFamily)new System.Windows.Media.FontFamilyConverter().ConvertFrom(fontDialog.Font.FontFamily.Name);
                            noteWidget.TextBox1.FontSize = fontDialog.Font.Size;
                        }
                        NoteWidget.UpdateWidgetInformationOfFile(noteWidget);
                    }
                }
                catch { }
            }
            else
            {
                MessageBox.Show
                    (
                    "If you want to change note's font settings, you need to enable the Note widget!",
                    "The color cannot be changed:",
                    MessageBoxButton.OK,
                    MessageBoxImage.Hand,
                    MessageBoxResult.OK
                    );
            }
        }

        #endregion

        #region NoteWidgetOpacity

        private void NoteBackgroundOpacityComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (noteWidget != null)
            {
                switch (NoteBackgroundOpacityComboBox.SelectedIndex)
                {
                    case 0:
                        noteWidget.Rec1.Opacity = 1;
                        break;
                    case 1:
                        noteWidget.Rec1.Opacity = 0.75;
                        break;
                    case 2:
                        noteWidget.Rec1.Opacity = 0.5;
                        break;
                    case 3:
                        noteWidget.Rec1.Opacity = 0.25;
                        break;
                    case 4:
                        noteWidget.Rec1.Opacity = 0;
                        break;
                }
                NoteWidget.UpdateWidgetInformationOfFile(noteWidget);
            }
        }

        private void NoteFontOpacityComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (noteWidget != null)
            {
                switch (NoteFontOpacityComboBox.SelectedIndex)
                {
                    case 0:
                        noteWidget.TextBox1.Opacity = 1;
                        break;
                    case 1:
                        noteWidget.TextBox1.Opacity = 0.75;
                        break;
                    case 2:
                        noteWidget.TextBox1.Opacity = 0.5;
                        break;
                    case 3:
                        noteWidget.TextBox1.Opacity = 0.25;
                        break;
                    case 4:
                        noteWidget.TextBox1.Opacity = 0;
                        break;
                }
                NoteWidget.UpdateWidgetInformationOfFile(noteWidget);
            }
        }

        #endregion

        #endregion

        #region ColorButtonsAndRectangles

        /// <summary>
        /// Chose clock's background color
        /// </summary>
        private void ChoseClockBackgroundColorButton_Click(object sender, RoutedEventArgs e)
        {
            if (clockWidget != null)
            {
                System.Drawing.Color colorDrawing = MainWindow.ColorMediaToDrawing(MainWindow.BrushToColorMedia(ClockBackgroundColorRectangle.Fill));

                try
                {
                    using (System.Windows.Forms.ColorDialog colorDialog = new System.Windows.Forms.ColorDialog()
                    {
                        FullOpen = true,
                        Color = colorDrawing
                    })
                    {
                        if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            clockWidget.rec1.Fill = new System.Windows.Media.SolidColorBrush(MainWindow.ColorDrawingToMedia(colorDialog.Color));
                            ClockBackgroundColorRectangle.Fill = new System.Windows.Media.SolidColorBrush(MainWindow.ColorDrawingToMedia(colorDialog.Color));
                        }
                        ClockWidget.UpdateWidgetInformationOfFile(clockWidget);
                    }
                }
                catch { }
            }
            else
            {
                MessageBox.Show
                    (
                    "If you want to change clock's background color, you need to enable the Clock widget!",
                    "The color cannot be changed:",
                    MessageBoxButton.OK,
                    MessageBoxImage.Hand,
                    MessageBoxResult.OK
                    );
            }
        }

        /// <summary>
        /// Loadnig clock's background color to rectangle 
        /// </summary>
        private void ClockBackgroundColorRectangle_Loaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists(ClockWidget.ClockSettingsPath))
            {
                ClockBackgroundColorRectangle.Fill = new System.Windows.Media.SolidColorBrush(ClockWidget.DownloadWidgetInformationOfFile().BackgroundColor);
            }
            else if (clockWidget != null)
            {
                ClockBackgroundColorRectangle.Fill = clockWidget.rec1.Fill;
            }
        }
        
        /// <summary>
        /// Chose clock's foreground color
        /// </summary>
        private void ChoseClockForegroundColorButton_Click(object sender, RoutedEventArgs e)
        {
            if (clockWidget != null)
            {
                System.Drawing.Color colorDrawing = MainWindow.ColorMediaToDrawing(MainWindow.BrushToColorMedia(ClockForegroundColorRectangle.Fill));

                try
                {
                    using (System.Windows.Forms.ColorDialog colorDialog = new System.Windows.Forms.ColorDialog()
                    {
                        FullOpen = true,
                        Color = colorDrawing
                    })
                    {
                        if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            clockWidget.ClockLabel.Foreground = new System.Windows.Media.SolidColorBrush(MainWindow.ColorDrawingToMedia(colorDialog.Color));
                            ClockForegroundColorRectangle.Fill = new System.Windows.Media.SolidColorBrush(MainWindow.ColorDrawingToMedia(colorDialog.Color));
                        }
                        ClockWidget.UpdateWidgetInformationOfFile(clockWidget);
                    }
                }
                catch { }
            }
            else
            {
                MessageBox.Show
                    (
                    "If you want to change clock's foreground color, you need to enable the Clock widget!",
                    "The color cannot be changed:",
                    MessageBoxButton.OK,
                    MessageBoxImage.Hand,
                    MessageBoxResult.OK
                    );
            }
        }

        /// <summary>
        /// Loadnig clock's foreground color to rectangle 
        /// </summary>
        private void ClockForegroundColorRectangle_Loaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists(ClockWidget.ClockSettingsPath))
            {
                ClockForegroundColorRectangle.Fill = new System.Windows.Media.SolidColorBrush(ClockWidget.DownloadWidgetInformationOfFile().ForegroundColor);
            }
            else if (clockWidget != null)
            {
                ClockForegroundColorRectangle.Fill = clockWidget.ClockLabel.Foreground;
            }
        }

        /// <summary>
        /// Chose note's background color
        /// </summary>
        private void ChoseNoteBackgroundColorButton_Click(object sender, RoutedEventArgs e)
        {
            if (noteWidget != null)
            {
                System.Drawing.Color colorDrawing = MainWindow.ColorMediaToDrawing(MainWindow.BrushToColorMedia(NoteBackgroundColorRectangle.Fill));

                try
                {
                    using (System.Windows.Forms.ColorDialog colorDialog = new System.Windows.Forms.ColorDialog()
                    {
                        FullOpen = true,
                        Color = colorDrawing
                    })
                    {
                        if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            noteWidget.Rec1.Fill = new System.Windows.Media.SolidColorBrush(MainWindow.ColorDrawingToMedia(colorDialog.Color));
                            NoteBackgroundColorRectangle.Fill = new System.Windows.Media.SolidColorBrush(MainWindow.ColorDrawingToMedia(colorDialog.Color));
                        }
                        NoteWidget.UpdateWidgetInformationOfFile(noteWidget);
                    }
                }
                catch { }
            }
            else
            {
                MessageBox.Show
                    (
                    "If you want to change note's background color, you need to enable the Note widget!",
                    "The color cannot be changed:",
                    MessageBoxButton.OK,
                    MessageBoxImage.Hand,
                    MessageBoxResult.OK
                    );
            }
        }

        /// <summary>
        /// Loadnig note's background color to rectangle 
        /// </summary>
        private void NoteBackgroundColorRectangle_Loaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists(NoteWidget.NoteSettingsPath))
            {
                NoteBackgroundColorRectangle.Fill = new System.Windows.Media.SolidColorBrush(NoteWidget.DownloadWidgetInformationOfFile().BackgroundColor);
            }
            else if (clockWidget != null)
            {
                NoteBackgroundColorRectangle.Fill = noteWidget.Rec1.Fill;
            }
        }

        /// <summary>
        /// Chose note's font color
        /// </summary>
        private void ChoseNoteFontColorButton_Click(object sender, RoutedEventArgs e)
        {
            if (noteWidget != null)
            {
                System.Drawing.Color colorDrawing = MainWindow.ColorMediaToDrawing(MainWindow.BrushToColorMedia(NoteFontColorRectangle.Fill));

                try
                {
                    using (System.Windows.Forms.ColorDialog colorDialog = new System.Windows.Forms.ColorDialog()
                    {
                        FullOpen = true,
                        Color = colorDrawing
                    })
                    {
                        if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            noteWidget.TextBox1.Foreground = new System.Windows.Media.SolidColorBrush(MainWindow.ColorDrawingToMedia(colorDialog.Color));
                            NoteFontColorRectangle.Fill = new System.Windows.Media.SolidColorBrush(MainWindow.ColorDrawingToMedia(colorDialog.Color));
                        }
                        NoteWidget.UpdateWidgetInformationOfFile(noteWidget);
                    }
                }
                catch { }
            }
            else
            {
                MessageBox.Show
                    (
                    "If you want to change note's font color, you need to enable the Note widget!",
                    "The color cannot be changed:",
                    MessageBoxButton.OK,
                    MessageBoxImage.Hand,
                    MessageBoxResult.OK
                    );
            }
        }

        /// <summary>
        /// Loadnig note's font color to rectangle 
        /// </summary>
        private void NoteFontColorRectangle_Loaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists(NoteWidget.NoteSettingsPath))
            {
                NoteFontColorRectangle.Fill = new System.Windows.Media.SolidColorBrush(NoteWidget.DownloadWidgetInformationOfFile().FontColor);
            }
            else if (clockWidget != null)
            {
                NoteFontColorRectangle.Fill = noteWidget.TextBox1.Foreground;
            }
        }

        #endregion

        #region ButtonResetSettingsToDefault

        /// <summary>
        /// Reset widgets settings
        /// </summary>
        private void ButtonResetSettingsToDefault_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(
                "Are you sure you want to restore the default settings? " +
                "These changes cannot be undone?",
                "Do you want reset settings?",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question,
                MessageBoxResult.No) == MessageBoxResult.Yes)
            {

                MainWindow.CloseWidgets(clockWidget, pictureWidget, noteWidget);

                if (File.Exists(ClockWidget.ClockSettingsPath))
                {
                    File.Delete(ClockWidget.ClockSettingsPath);
                }

                if (File.Exists(PictureWidget.PictureSettingsPath))
                {
                    File.Delete(PictureWidget.PictureSettingsPath);
                }

                if (File.Exists(NoteWidget.NoteSettingsPath))
                {
                    File.Delete(NoteWidget.NoteSettingsPath);
                }

                LoadingWidgets();

                ClockVisibleCheckBox_Loaded(sender, e);
                PictureBoxVisibleCheckBox_Loaded(sender, e);
                NoteVisibleCheckBox_Loaded(sender, e);

                ClockBackgroundColorRectangle_Loaded(sender, e);
                NoteBackgroundColorRectangle_Loaded(sender, e);
                NoteFontColorRectangle_Loaded(sender, e);

            }
        }

        #endregion

        #region ButtonCloseApp

        private void ButtonCloseApplication_Click(object sender, RoutedEventArgs e)
        {
            IsClosing = true;
            this.Close();
        }

        #endregion

        #endregion

        #region CloseMethod

        private void MainWindowElement_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow.SaveWidgetsSettings(clockWidget, pictureWidget, noteWidget);
            MainWindow.CloseWidgets(clockWidget, pictureWidget, noteWidget);
        }

        #endregion

    }
}

