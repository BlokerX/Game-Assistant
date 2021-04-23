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
        public ClockWidget clockForm = null;
        public PictureWidget pictureForm = null;
        public NoteWidget noteForm = null;

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

            #region FileAndDirectoresSystemStart

            // Set Paths
            MainWindow.SetPaths();

            // Check dires system
            MainWindow.CheckProgramDiresArchitecture();

            #endregion

            LoadingWidgets();
        }

        private void LoadingWidgets()
        {
            #region ClockFormLoading

            ClockInformation clockInf = MainWindow.DownloadClockInformationOfFile();

            if (clockInf != null)
            {
                if (clockInf.IsChosed == true)
                {
                    clockForm = MainWindow.CreateClockWidget();
                    clockForm.LocationChanged += ClockForm_LocationChanged;
                    clockForm.SizeChanged += ClockForm_SizeChanged;
                    clockForm.Show();
                }
                else
                {
                    clockForm = null;
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
                clockForm = MainWindow.CreateClockWidget();
                clockForm.LocationChanged += ClockForm_LocationChanged;
                clockForm.SizeChanged += ClockForm_SizeChanged;
                clockForm.Show();
            }

            if (clockForm != null)
            {
                clockForm.IsAllowDrag = true;
                clockForm.ResizeMode = ResizeMode.CanResizeWithGrip;
            }

            #endregion

            #region PictureFormLoading

            PictureInformation pictureInf = MainWindow.DownloadPictureInformationOfFile();

            if (pictureInf != null)
            {
                if (pictureInf.IsChosed == true)
                {
                    pictureForm = MainWindow.CreatePictureWidget();
                    pictureForm.LocationChanged += PictureForm_LocationChanged;
                    pictureForm.SizeChanged += PictureForm_SizeChanged;
                    pictureForm.Show();
                }
                else
                {
                    pictureForm = null;
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
                pictureForm = MainWindow.CreatePictureWidget();
                pictureForm.LocationChanged += PictureForm_LocationChanged;
                pictureForm.SizeChanged += PictureForm_SizeChanged;
                pictureForm.Show();
            }
            if (pictureForm != null)
            {
                pictureForm.IsAllowDrag = true;
                pictureForm.ResizeMode = ResizeMode.CanResizeWithGrip;
                pictureForm.rec1.Visibility = Visibility.Visible;
            }

            #endregion

            #region NoteFormLoading

            NoteInformation noteInf = MainWindow.DownloadNoteInformationOfFile();

            if (noteInf != null)
            {
                if (noteInf.IsChosed == true)
                {
                    noteForm = MainWindow.CreateNoteWidget();
                    noteForm.LocationChanged += NoteForm_LocationChanged;
                    noteForm.SizeChanged += NoteForm_SizeChanged;
                    noteForm.Show();
                }
                else
                {
                    noteForm = null;
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
                noteForm = MainWindow.CreateNoteWidget();
                noteForm.LocationChanged += NoteForm_LocationChanged;
                noteForm.SizeChanged += NoteForm_SizeChanged;
                noteForm.Show();
            }

            if (noteForm != null)
            {
                noteForm.IsAllowDrag = true;
                noteForm.ResizeMode = ResizeMode.CanResizeWithGrip;
            }

            #endregion

        }

        #endregion

        #region ControlsEvents

        #region ChosePicture

        /// <summary>
        /// Set picture in PictureForm 
        /// </summary>
        private void ChosePictureButton_Click(object sender, RoutedEventArgs e)
        {
            if (pictureForm != null)
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
                            pictureForm.ImageBox.Source = new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, openFileDialog.FileName)));
                            PicturePathTextBox.Text = openFileDialog.FileName;
                            pictureForm.ImagePath = openFileDialog.FileName;
                            MainWindow.UpdatePictureInformationOfFile(pictureForm);
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
            if (clockForm == null)
            {
                clockForm = MainWindow.CreateClockWidget();
                clockForm.LocationChanged += ClockForm_LocationChanged;
                clockForm.SizeChanged += ClockForm_SizeChanged;
                if (clockForm != null)
                {
                    clockForm.IsAllowDrag = true;
                    clockForm.ResizeMode = ResizeMode.CanResizeWithGrip;
                }
                clockForm.Show();
                MainWindow.UpdateClockInformationOfFile(clockForm);
            }
        }

        /// <summary>
        /// It does when ClockVisibleCheckBox unchecked 
        /// </summary>
        private void ClockVisibleCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (clockForm != null)
            {
                MainWindow.UpdateClockInformationOfFile(clockForm);
                clockForm.Close();
                clockForm = null;
                MainWindow.UpdateClockInformationOfFile(clockForm);
            }
        }

        /// <summary>
        /// It do when ClockVisibleCheckBox Loaded
        /// </summary>
        private void ClockVisibleCheckBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (clockForm != null)
            {
                this.ClockVisibleCheckBox.IsChecked = true;
            }
            else if (clockForm == null)
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
            if (pictureForm == null)
            {
                pictureForm = MainWindow.CreatePictureWidget();
                pictureForm.LocationChanged += PictureForm_LocationChanged;
                pictureForm.SizeChanged += PictureForm_SizeChanged;
                if (pictureForm != null)
                {
                    pictureForm.IsAllowDrag = true;
                    pictureForm.ResizeMode = ResizeMode.CanResizeWithGrip;
                    pictureForm.rec1.Visibility = Visibility.Visible;
                }
                pictureForm.Show();
                MainWindow.UpdatePictureInformationOfFile(pictureForm);
            }
        }

        /// <summary>
        /// It does when PictureBoxVisibleCheckBox unchecked 
        /// </summary>
        private void PictureBoxVisibleCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (pictureForm != null)
            {
                MainWindow.UpdatePictureInformationOfFile(pictureForm);
                pictureForm.Close();
                pictureForm = null;
                MainWindow.UpdatePictureInformationOfFile(pictureForm);
            }
        }

        /// <summary>
        /// It do when PictureBoxVisibleCheckBox Loaded
        /// </summary>
        private void PictureBoxVisibleCheckBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (pictureForm != null)
            {
                this.PictureBoxVisibleCheckBox.IsChecked = true;
            }
            else if (pictureForm == null)
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
            if (noteForm == null)
            {
                noteForm = MainWindow.CreateNoteWidget();
                noteForm.LocationChanged += NoteForm_LocationChanged;
                noteForm.SizeChanged += NoteForm_SizeChanged;
                if (noteForm != null)
                {
                    noteForm.IsAllowDrag = true;
                    noteForm.ResizeMode = ResizeMode.CanResizeWithGrip;
                }
                noteForm?.Show();
                MainWindow.UpdateNoteInformationOfFile(noteForm);
            }
        }

        /// <summary>
        /// It does when NoteVisibleCheckBox unchecked 
        /// </summary>
        private void NoteVisibleCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (noteForm != null)
            {
                MainWindow.UpdateNoteInformationOfFile(noteForm);
                noteForm?.Close();
                noteForm = null;
                MainWindow.UpdateNoteInformationOfFile(noteForm);
            }
        }

        /// <summary>
        /// It do when NoteVisibleCheckBox Loaded
        /// </summary>
        private void NoteVisibleCheckBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (noteForm != null)
            {
                this.NoteVisibleCheckBox.IsChecked = true;
            }
            else if (noteForm == null)
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
            if (clockForm != null)
            {
                switch (ClockOpacityComboBox.SelectedIndex)
                {
                    case 0:
                        clockForm.ClockLabel.Opacity = 1;
                        break;
                    case 1:
                        clockForm.ClockLabel.Opacity = 0.75;
                        break;
                    case 2:
                        clockForm.ClockLabel.Opacity = 0.5;
                        break;
                    case 3:
                        clockForm.ClockLabel.Opacity = 0.25;
                        break;
                    case 4:
                        clockForm.ClockLabel.Opacity = 0;
                        break;
                }
                MainWindow.UpdateClockInformationOfFile(clockForm);
            }
        }

        private void ClockBackgroundOpacityComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (clockForm != null)
            {
                switch (ClockBackgroundOpacityComboBox.SelectedIndex)
                {
                    case 0:
                        clockForm.rec1.Opacity = 1;
                        break;
                    case 1:
                        clockForm.rec1.Opacity = 0.75;
                        break;
                    case 2:
                        clockForm.rec1.Opacity = 0.5;
                        break;
                    case 3:
                        clockForm.rec1.Opacity = 0.25;
                        break;
                    case 4:
                        clockForm.rec1.Opacity = 0;
                        break;
                }
                MainWindow.UpdateClockInformationOfFile(clockForm);
            }
        }

        #endregion

        #region PictureWidgetOpacity

        private void PictureOpacityComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (pictureForm != null)
            {
                switch (PictureOpacityComboBox.SelectedIndex)
                {
                    case 0:
                        pictureForm.ImageBox.Opacity = 1;
                        break;
                    case 1:
                        pictureForm.ImageBox.Opacity = 0.75;
                        break;
                    case 2:
                        pictureForm.ImageBox.Opacity = 0.5;
                        break;
                    case 3:
                        pictureForm.ImageBox.Opacity = 0.25;
                        break;
                    case 4:
                        pictureForm.ImageBox.Opacity = 0;
                        break;
                }
                MainWindow.UpdatePictureInformationOfFile(pictureForm);
            }
        }

        #endregion

        #region NoteOptions

        private void ChoseNoteFontSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            if (noteForm != null)
            {
                System.Drawing.FontFamily font1 = MainWindow.FontFamilyMediaToDrawing(noteForm.TextBox1.FontFamily);

                try
                {
                    using (System.Windows.Forms.FontDialog fontDialog = new System.Windows.Forms.FontDialog()
                    {
                        Color = MainWindow.ColorMediaToDrawing(MainWindow.BrushToColorMedia(noteForm.TextBox1.Foreground)),
                        Font = new System.Drawing.Font(font1.Name, float.Parse(noteForm.TextBox1.FontSize.ToString())),
                        ShowEffects = false
                    })
                    {
                        if (fontDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            // todo naprawić font family
                            noteForm.TextBox1.FontFamily = (System.Windows.Media.FontFamily)new System.Windows.Media.FontFamilyConverter().ConvertFrom(fontDialog.Font.FontFamily.Name);
                            noteForm.TextBox1.FontSize = fontDialog.Font.Size;
                        }
                        MainWindow.UpdateNoteInformationOfFile(noteForm);
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
            if (noteForm != null)
            {
                switch (NoteBackgroundOpacityComboBox.SelectedIndex)
                {
                    case 0:
                        noteForm.Rec1.Opacity = 1;
                        break;
                    case 1:
                        noteForm.Rec1.Opacity = 0.75;
                        break;
                    case 2:
                        noteForm.Rec1.Opacity = 0.5;
                        break;
                    case 3:
                        noteForm.Rec1.Opacity = 0.25;
                        break;
                    case 4:
                        noteForm.Rec1.Opacity = 0;
                        break;
                }
                MainWindow.UpdateNoteInformationOfFile(noteForm);
            }
        }

        private void NoteFontOpacityComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (noteForm != null)
            {
                switch (NoteFontOpacityComboBox.SelectedIndex)
                {
                    case 0:
                        noteForm.TextBox1.Opacity = 1;
                        break;
                    case 1:
                        noteForm.TextBox1.Opacity = 0.75;
                        break;
                    case 2:
                        noteForm.TextBox1.Opacity = 0.5;
                        break;
                    case 3:
                        noteForm.TextBox1.Opacity = 0.25;
                        break;
                    case 4:
                        noteForm.TextBox1.Opacity = 0;
                        break;
                }
                MainWindow.UpdateNoteInformationOfFile(noteForm);
            }
        }

        #endregion

        #endregion

        #region ClockBackgroundColor

        /// <summary>
        /// Chose clock's background color
        /// </summary>
        private void ChoseClockBackgroundColorButton_Click(object sender, RoutedEventArgs e)
        {
            if (clockForm != null)
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
                            clockForm.rec1.Fill = new System.Windows.Media.SolidColorBrush(MainWindow.ColorDrawingToMedia(colorDialog.Color));
                            ClockBackgroundColorRectangle.Fill = new System.Windows.Media.SolidColorBrush(MainWindow.ColorDrawingToMedia(colorDialog.Color));
                        }
                        MainWindow.UpdateClockInformationOfFile(clockForm);
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
            if (File.Exists(MainWindow.ClockSettingsPath))
            {
                ClockBackgroundColorRectangle.Fill = new System.Windows.Media.SolidColorBrush(MainWindow.DownloadClockInformationOfFile().BackgroundColor);
            }
            else if (clockForm != null)
            {
                ClockBackgroundColorRectangle.Fill = clockForm.rec1.Fill;
            }
        }

        /// <summary>
        /// Chose note's background color
        /// </summary>
        private void ChoseNoteBackgroundColorButton_Click(object sender, RoutedEventArgs e)
        {
            if (noteForm != null)
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
                            noteForm.Rec1.Fill = new System.Windows.Media.SolidColorBrush(MainWindow.ColorDrawingToMedia(colorDialog.Color));
                            NoteBackgroundColorRectangle.Fill = new System.Windows.Media.SolidColorBrush(MainWindow.ColorDrawingToMedia(colorDialog.Color));
                        }
                        MainWindow.UpdateNoteInformationOfFile(noteForm);
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
            if (File.Exists(MainWindow.NoteSettingsPath))
            {
                NoteBackgroundColorRectangle.Fill = new System.Windows.Media.SolidColorBrush(MainWindow.DownloadNoteInformationOfFile().BackgroundColor);
            }
            else if (clockForm != null)
            {
                NoteBackgroundColorRectangle.Fill = noteForm.Rec1.Fill;
            }
        }

        /// <summary>
        /// Chose note's font color
        /// </summary>
        private void ChoseNoteFontColorButton_Click(object sender, RoutedEventArgs e)
        {
            if (noteForm != null)
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
                            noteForm.TextBox1.Foreground = new System.Windows.Media.SolidColorBrush(MainWindow.ColorDrawingToMedia(colorDialog.Color));
                            NoteFontColorRectangle.Fill = new System.Windows.Media.SolidColorBrush(MainWindow.ColorDrawingToMedia(colorDialog.Color));
                        }
                        MainWindow.UpdateNoteInformationOfFile(noteForm);
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
            if (File.Exists(MainWindow.NoteSettingsPath))
            {
                NoteFontColorRectangle.Fill = new System.Windows.Media.SolidColorBrush(MainWindow.DownloadNoteInformationOfFile().FontColor);
            }
            else if (clockForm != null)
            {
                NoteFontColorRectangle.Fill = noteForm.TextBox1.Foreground;
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

                clockForm?.Close();
                clockForm = null;

                pictureForm?.Close();
                pictureForm = null;

                noteForm?.Close();
                noteForm = null;

                if (File.Exists(MainWindow.ClockSettingsPath))
                {
                    File.Delete(MainWindow.ClockSettingsPath);
                }

                if (File.Exists(MainWindow.PictureSettingsPath))
                {
                    File.Delete(MainWindow.PictureSettingsPath);
                }

                if (File.Exists(MainWindow.NoteSettingsPath))
                {
                    File.Delete(MainWindow.NoteSettingsPath);
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

        #region EventsMethods

        private void ClockForm_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            MainWindow.UpdateClockInformationOfFile(clockForm);
        }

        private void ClockForm_LocationChanged(object sender, EventArgs e)
        {
            MainWindow.UpdateClockInformationOfFile(clockForm);
        }

        private void PictureForm_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            MainWindow.UpdatePictureInformationOfFile(pictureForm);
        }

        private void PictureForm_LocationChanged(object sender, EventArgs e)
        {
            MainWindow.UpdatePictureInformationOfFile(pictureForm);
        }

        private void NoteForm_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            MainWindow.UpdateNoteInformationOfFile(noteForm);
        }

        private void NoteForm_LocationChanged(object sender, EventArgs e)
        {
            MainWindow.UpdateNoteInformationOfFile(noteForm);
        }

        #endregion

        #region CloseMethod

        private void MainWindowElement_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow.UpdateClockInformationOfFile(clockForm);
            MainWindow.UpdatePictureInformationOfFile(pictureForm);
            MainWindow.UpdateNoteInformationOfFile(noteForm);

            clockForm?.Close();
            clockForm = null;

            pictureForm?.Close();
            pictureForm = null;

            noteForm?.Close();
            noteForm = null;
        }

        #endregion

    }
}

