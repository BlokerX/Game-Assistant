using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using static GameAssistant.WidgetWindow;

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
        public FPSCounterWidget fpsCounterWidget = null;

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
                
                switch (pictureInf.Animation)
                {
                    case Animations.ColorRainbowRGB:
                        this.PictureWidgetAnimationComboBox.SelectedIndex = 1;
                        break;

                    case Animations.ColorRainbowRGB2:
                        this.PictureWidgetAnimationComboBox.SelectedIndex = 2;
                        break;

                    case Animations.NULL:
                    default:
                        this.PictureWidgetAnimationComboBox.SelectedIndex = 0;
                        break;
                }

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

                switch (noteInf.Animation)
                {
                    case Animations.ColorRainbowRGB:
                        this.NoteWidgetAnimationComboBox.SelectedIndex = 1;
                        break;

                    case Animations.ColorRainbowRGB2:
                        this.NoteWidgetAnimationComboBox.SelectedIndex = 2;
                        break;

                    case Animations.NULL:
                    default:
                        this.NoteWidgetAnimationComboBox.SelectedIndex = 0;
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

                switch (clockInf.Animation)
                {
                    case Animations.ColorRainbowRGB:
                        this.ClockWidgetAnimationComboBox.SelectedIndex = 1;
                        break;

                    case Animations.ColorRainbowRGB2:
                        this.ClockWidgetAnimationComboBox.SelectedIndex = 2;
                        break;

                    case Animations.NULL:
                    default:
                        this.ClockWidgetAnimationComboBox.SelectedIndex = 0;
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

            #region FPSCounterWidgetLoading

            FPSCounterInformation fpsCounterInf = FPSCounterWidget.DownloadWidgetInformationOfFile();

            if (fpsCounterInf != null)
            {
                if (fpsCounterInf.IsChosed == true)
                {
                    fpsCounterWidget = FPSCounterWidget.CreateWidget();
                    fpsCounterWidget.Show();
                }
                else
                {
                    fpsCounterWidget = null;
                }

                switch (fpsCounterInf.FPSOpacity)
                {
                    case 1:
                        this.FPSCounterForegroundOpacityComboBox.SelectedIndex = 0;
                        break;
                    case 0.75:
                        this.FPSCounterForegroundOpacityComboBox.SelectedIndex = 1;
                        break;
                    case 0.5:
                        this.FPSCounterForegroundOpacityComboBox.SelectedIndex = 2;
                        break;
                    case 0.25:
                        this.FPSCounterForegroundOpacityComboBox.SelectedIndex = 3;
                        break;
                    case 0:
                        this.FPSCounterForegroundOpacityComboBox.SelectedIndex = 4;
                        break;
                    default:
                        this.FPSCounterForegroundOpacityComboBox.SelectedIndex = 1;
                        break;
                }

                switch (fpsCounterInf.BackgroundOpacity)
                {
                    case 1:
                        this.FPSCounterBackgroundOpacityComboBox.SelectedIndex = 0;
                        break;
                    case 0.75:
                        this.FPSCounterBackgroundOpacityComboBox.SelectedIndex = 1;
                        break;
                    case 0.5:
                        this.FPSCounterBackgroundOpacityComboBox.SelectedIndex = 2;
                        break;
                    case 0.25:
                        this.FPSCounterBackgroundOpacityComboBox.SelectedIndex = 3;
                        break;
                    case 0:
                        this.FPSCounterBackgroundOpacityComboBox.SelectedIndex = 4;
                        break;
                    default:
                        this.FPSCounterBackgroundOpacityComboBox.SelectedIndex = 1;
                        break;
                }

                switch (fpsCounterInf.Animation)
                {
                    case Animations.ColorRainbowRGB:
                        this.FPSCounterWidgetAnimationComboBox.SelectedIndex = 1;
                        break;

                    case Animations.ColorRainbowRGB2:
                        this.FPSCounterWidgetAnimationComboBox.SelectedIndex = 2;
                        break;

                    case Animations.NULL:
                    default:
                        this.FPSCounterWidgetAnimationComboBox.SelectedIndex = 0;
                        break;
                }

            }
            else
            {
                fpsCounterWidget = FPSCounterWidget.CreateWidget();
                fpsCounterWidget.Show();
            }

            if (fpsCounterWidget != null)
            {
                fpsCounterWidget.IsAllowDrag = true;
                fpsCounterWidget.ResizeMode = ResizeMode.CanResizeWithGrip;
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

        #region FPSCounterVisibleCheckBox

        /// <summary>
        /// It does when FPSCounterVisibleCheckBox checked 
        /// </summary>
        private void FPSCounterVisibleCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (fpsCounterWidget == null)
            {
                fpsCounterWidget = FPSCounterWidget.CreateWidget();
                if (fpsCounterWidget != null)
                {
                    fpsCounterWidget.IsAllowDrag = true;
                    fpsCounterWidget.ResizeMode = ResizeMode.CanResizeWithGrip;
                }
                fpsCounterWidget.Show();
                FPSCounterWidget.UpdateWidgetInformationOfFile(fpsCounterWidget);
            }
        }

        /// <summary>
        /// It does when FPSCounterVisibleCheckBox unchecked 
        /// </summary>
        private void FPSCounterVisibleCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (fpsCounterWidget != null)
            {
                FPSCounterWidget.UpdateWidgetInformationOfFile(fpsCounterWidget);
                fpsCounterWidget.Close();
                fpsCounterWidget = null;
                FPSCounterWidget.UpdateWidgetInformationOfFile(fpsCounterWidget);
            }
        }

        /// <summary>
        /// It do when FPSCounterVisibleCheckBox Loaded
        /// </summary>
        private void FPSCounterVisibleCheckBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (fpsCounterWidget != null)
            {
                this.FPSCounterVisibleCheckBox.IsChecked = true;
            }
            else if (fpsCounterWidget == null)
            {
                this.FPSCounterVisibleCheckBox.IsChecked = false;
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
                        noteWidget.rec1.Opacity = 1;
                        break;
                    case 1:
                        noteWidget.rec1.Opacity = 0.75;
                        break;
                    case 2:
                        noteWidget.rec1.Opacity = 0.5;
                        break;
                    case 3:
                        noteWidget.rec1.Opacity = 0.25;
                        break;
                    case 4:
                        noteWidget.rec1.Opacity = 0;
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

        #region FPSCounterWidgetOpacity

        private void FPSCounterForegroundOpacityComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (fpsCounterWidget != null)
            {
                switch (FPSCounterForegroundOpacityComboBox.SelectedIndex)
                {
                    case 0:
                        fpsCounterWidget.lab1.Opacity = 1;
                        break;
                    case 1:
                        fpsCounterWidget.lab1.Opacity = 0.75;
                        break;
                    case 2:
                        fpsCounterWidget.lab1.Opacity = 0.5;
                        break;
                    case 3:
                        fpsCounterWidget.lab1.Opacity = 0.25;
                        break;
                    case 4:
                        fpsCounterWidget.lab1.Opacity = 0;
                        break;
                }
                FPSCounterWidget.UpdateWidgetInformationOfFile(fpsCounterWidget);
            }
        }

        private void FPSCounterBackgroundOpacityComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (fpsCounterWidget != null)
            {
                switch (FPSCounterBackgroundOpacityComboBox.SelectedIndex)
                {
                    case 0:
                        fpsCounterWidget.rec1.Opacity = 1;
                        break;
                    case 1:
                        fpsCounterWidget.rec1.Opacity = 0.75;
                        break;
                    case 2:
                        fpsCounterWidget.rec1.Opacity = 0.5;
                        break;
                    case 3:
                        fpsCounterWidget.rec1.Opacity = 0.25;
                        break;
                    case 4:
                        fpsCounterWidget.rec1.Opacity = 0;
                        break;
                }
                FPSCounterWidget.UpdateWidgetInformationOfFile(fpsCounterWidget);
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
                            noteWidget.rec1.Fill = new System.Windows.Media.SolidColorBrush(MainWindow.ColorDrawingToMedia(colorDialog.Color));
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
                NoteBackgroundColorRectangle.Fill = noteWidget.rec1.Fill;
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

        /// <summary>
        /// Chose fpsCounter's background color
        /// </summary>
        private void ChoseFPSCounterBackgroundColorButton_Click(object sender, RoutedEventArgs e)
        {
            if (fpsCounterWidget != null)
            {
                System.Drawing.Color colorDrawing = MainWindow.ColorMediaToDrawing(MainWindow.BrushToColorMedia(FPSCounterBackgroundColorRectangle.Fill));

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
                            fpsCounterWidget.rec1.Fill = new System.Windows.Media.SolidColorBrush(MainWindow.ColorDrawingToMedia(colorDialog.Color));
                            FPSCounterBackgroundColorRectangle.Fill = new System.Windows.Media.SolidColorBrush(MainWindow.ColorDrawingToMedia(colorDialog.Color));
                        }
                        FPSCounterWidget.UpdateWidgetInformationOfFile(fpsCounterWidget);
                    }
                }
                catch { }
            }
            else
            {
                MessageBox.Show
                    (
                    "If you want to change FPS Counter's background color, you need to enable the FPS Counter widget!",
                    "The color cannot be changed:",
                    MessageBoxButton.OK,
                    MessageBoxImage.Hand,
                    MessageBoxResult.OK
                    );
            }
        }

        /// <summary>
        /// Loadnig fpsCounter's background color to rectangle 
        /// </summary>
        private void FPSCounterBackgroundColorRectangle_Loaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists(FPSCounterWidget.FPSCounterSettingsPath))
            {
                FPSCounterBackgroundColorRectangle.Fill = new System.Windows.Media.SolidColorBrush(FPSCounterWidget.DownloadWidgetInformationOfFile().BackgroundColor);
            }
            else if (fpsCounterWidget != null)
            {
                FPSCounterBackgroundColorRectangle.Fill = fpsCounterWidget.rec1.Fill;
            }
        }

        /// <summary>
        /// Chose fpsCounter's foreground color
        /// </summary>
        private void ChoseFPSCounterForegroundColorButton_Click(object sender, RoutedEventArgs e)
        {
            if (fpsCounterWidget != null)
            {
                System.Drawing.Color colorDrawing = MainWindow.ColorMediaToDrawing(MainWindow.BrushToColorMedia(FPSCounterForegroundColorRectangle.Fill));

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
                            fpsCounterWidget.lab1.Foreground = new System.Windows.Media.SolidColorBrush(MainWindow.ColorDrawingToMedia(colorDialog.Color));
                            FPSCounterForegroundColorRectangle.Fill = new System.Windows.Media.SolidColorBrush(MainWindow.ColorDrawingToMedia(colorDialog.Color));
                        }
                        FPSCounterWidget.UpdateWidgetInformationOfFile(fpsCounterWidget);
                    }
                }
                catch { }
            }
            else
            {
                MessageBox.Show
                    (
                    "If you want to change FPS Counter's foreground color, you need to enable the FPS Counter widget!",
                    "The color cannot be changed:",
                    MessageBoxButton.OK,
                    MessageBoxImage.Hand,
                    MessageBoxResult.OK
                    );
            }
        }

        /// <summary>
        /// Loadnig fpsCounter's foreground color to rectangle 
        /// </summary>
        private void FPSCounterForegroundColorRectangle_Loaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists(FPSCounterWidget.FPSCounterSettingsPath))
            {
                FPSCounterForegroundColorRectangle.Fill = new System.Windows.Media.SolidColorBrush(FPSCounterWidget.DownloadWidgetInformationOfFile().ForegroundColor);
            }
            else if (fpsCounterWidget != null)
            {
                FPSCounterForegroundColorRectangle.Fill = fpsCounterWidget.lab1.Foreground;
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

                MainWindow.CloseWidgets(clockWidget, pictureWidget, noteWidget, fpsCounterWidget);

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

                if (File.Exists(FPSCounterWidget.FPSCounterSettingsPath))
                {
                    File.Delete(FPSCounterWidget.FPSCounterSettingsPath);
                }

                LoadingWidgets();
                MainWindow.CloseWidgets(clockWidget, pictureWidget, noteWidget, fpsCounterWidget);
                LoadingWidgets();

                ClockVisibleCheckBox_Loaded(sender, e);
                PictureBoxVisibleCheckBox_Loaded(sender, e);
                NoteVisibleCheckBox_Loaded(sender, e);
                FPSCounterVisibleCheckBox_Loaded(sender, e);

                ClockBackgroundColorRectangle_Loaded(sender, e);
                ClockForegroundColorRectangle_Loaded(sender, e);
                NoteBackgroundColorRectangle_Loaded(sender, e);
                NoteFontColorRectangle_Loaded(sender, e);
                FPSCounterBackgroundColorRectangle_Loaded(sender, e);
                FPSCounterForegroundColorRectangle_Loaded(sender, e);
            }
        }

        #endregion

        #region ButtonSettingsDire

        /// <summary>
        /// Open settings dire in Explorer
        /// </summary>
        private void ButtonProgramDataDire_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(MainWindow.GameAssistantFolderPath);
        }

        #endregion

        #region ButtonCloseApp

        private void ButtonCloseApplication_Click(object sender, RoutedEventArgs e)
        {
            IsClosing = true;
            this.Close();
        }

        #endregion

        #region FPSCounterSettings

        private void FPSCounterProcessesComboBox_Initialized(object sender, EventArgs e)
        {
            GetActiveProcesses();
        }

        private void FPSCounterProcessesComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (fpsCounterWidget != null)
            {
                //fpsCounterWidget.SelectProcessFPS = FPSCounterProcessesComboBox.Items[FPSCounterProcessesComboBox.SelectedIndex].ToString();
                //FPSCounterWidget.UpdateWidgetInformationOfFile(fpsCounterWidget);
                //fpsCounterWidget.Close();
                //fpsCounterWidget = FPSCounterWidget.CreateWidget();
                //fpsCounterWidget.IsAllowDrag = true;
                //fpsCounterWidget.ResizeMode = ResizeMode.CanResizeWithGrip;
                //fpsCounterWidget.Show();

                fpsCounterWidget.lab1.Content = "0 FPS";
                if (FPSCounterProcessesComboBox.SelectedIndex < FPSCounterProcessesComboBox.Items.Count && FPSCounterProcessesComboBox.SelectedIndex > -1)
                {
                    fpsCounterWidget.SelectProcessFPS = FPSCounterProcessesComboBox.Items[FPSCounterProcessesComboBox.SelectedIndex].ToString();
                }
                FPSCounterWidget.UpdateWidgetInformationOfFile(fpsCounterWidget);
            }
        }

        private void GetActiveProcesses()
        {
            //FPSCounterWidget.LoadFPSReader();
            var frames = FPSCounterWidget.GetFrames();

            FPSCounterProcessesComboBox.SelectedIndex = -1;
            FPSCounterProcessesComboBox.Items.Clear();
            if (frames.Count > 0)
            {
                foreach (var item in frames)
                {
                    FPSCounterProcessesComboBox.Items.Add(item.Value.Name);
                }
            }
            else
            {
                FPSCounterProcessesComboBox.Items.Add(" - No items - ");
            }

            if (fpsCounterWidget != null)
            {
                for (int i = 0; i < FPSCounterProcessesComboBox.Items.Count; i++)
                {
                    if (FPSCounterProcessesComboBox.Items[i].ToString() == fpsCounterWidget.SelectProcessFPS)
                    {
                        FPSCounterProcessesComboBox.SelectedIndex = i;
                        break;
                    }
                }
            }
            else if (File.Exists(FPSCounterWidget.FPSCounterSettingsPath))
            {
                string nameOfProcess_ = FPSCounterWidget.DownloadWidgetInformationOfFile().SelectedProcess;
                for (int i = 0; i < FPSCounterProcessesComboBox.Items.Count; i++)
                {
                    if (FPSCounterProcessesComboBox.Items[i].ToString() == nameOfProcess_)
                    {
                        FPSCounterProcessesComboBox.SelectedIndex = i;
                        break;
                    }
                }
            }

            if (FPSCounterProcessesComboBox.SelectedIndex == -1)
            {
                if (fpsCounterWidget != null)
                {
                    FPSCounterProcessesComboBox.Items.Add(fpsCounterWidget.SelectProcessFPS);
                    FPSCounterProcessesComboBox.SelectedIndex = FPSCounterProcessesComboBox.Items.Count - 1;
                }
                else if (File.Exists(FPSCounterWidget.FPSCounterSettingsPath))
                {
                    FPSCounterProcessesComboBox.Items.Add(FPSCounterWidget.DownloadWidgetInformationOfFile()?.SelectedProcess);
                    FPSCounterProcessesComboBox.SelectedIndex = FPSCounterProcessesComboBox.Items.Count - 1;
                }
                else
                {
                    if (FPSCounterProcessesComboBox.Items[0].ToString() == " - No items - ")
                        FPSCounterProcessesComboBox.SelectedIndex = 0;
                }
            }

        }

        #endregion

        #endregion

        #region CloseMethod

        private void MainWindowElement_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow.SaveWidgetsSettings(clockWidget, pictureWidget, noteWidget, fpsCounterWidget);
            MainWindow.CloseWidgets(clockWidget, pictureWidget, noteWidget, fpsCounterWidget);
        }



        #endregion

        private void PictureWidgetAnimationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (pictureWidget != null)
            {
                ComboBox sender1 = (ComboBox)sender;
                Enum.TryParse(sender1.Items[sender1.SelectedIndex].ToString(), out Animations animations1);
                pictureWidget.ChosedAnimation = animations1;
            }
        }

        private void ClockWidgetAnimationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (clockWidget != null)
            {
                ComboBox sender1 = (ComboBox)sender;
                Enum.TryParse(sender1.Items[sender1.SelectedIndex].ToString(), out Animations animations1);
                clockWidget.ChosedAnimation = animations1;
            }
        }

        private void NoteWidgetAnimationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (noteWidget != null)
            {
                ComboBox sender1 = (ComboBox)sender;
                Enum.TryParse(sender1.Items[sender1.SelectedIndex].ToString(), out Animations animations1);
                noteWidget.ChosedAnimation = animations1;
            }
        }

        private void FPSCounterWidgetAnimationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (fpsCounterWidget != null)
            {
                ComboBox sender1 = (ComboBox)sender;
                Enum.TryParse(sender1.Items[sender1.SelectedIndex].ToString(), out Animations animations1);
                fpsCounterWidget.ChosedAnimation = animations1;
            }
        }
    }
}

