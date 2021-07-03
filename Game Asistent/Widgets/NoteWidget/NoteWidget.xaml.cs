using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace GameAssistant
{
    /// <summary>
    /// Logika interakcji dla klasy NoteWidget.xaml
    /// </summary>
    public partial class NoteWidget : WidgetWindow
    {
        // Varibles:
        public static string NotesDirePath; // PATH //
        public static string NoteSettingsPath; // PATH //

        public List<string> NotePath = new List<string>();
        public int NotePathIndex = 0;
        public string ActuallyNotePath;

        public NoteWidget()
        {
            InitializeComponent();

            string[] tab1 = Directory.GetFiles(NotesDirePath);
            foreach (var path in tab1)
            {
                NotePath.Add(path);
            }
            if (!File.Exists(ActuallyNotePath))
            {
                if (NotePath.Count > 0 && File.Exists(this?.NotePath[0]))
                {
                    ActuallyNotePath = NotePath[0];
                }
                else
                {
                    NotePath.Add(Path.Combine(NoteWidget.NotesDirePath, "Note_1.txt"));
                    using (StreamWriter sw = File.CreateText(NotePath[0]))
                    {
                        sw.WriteLine("Tutaj wpisz swoją notatkę...");
                        sw.Dispose();
                    }
                    ActuallyNotePath = NotePath[0];
                }
            }
            else
            {
                int i = 0;
                foreach (var item in NotePath)
                {
                    if (item == ActuallyNotePath)
                    {
                        NotePathIndex = i;
                    }
                    i++;
                }
            }
            LoadActuallyText();
        }

        private void LoadActuallyText()
        {
            string textFromFile;
            if (File.Exists(ActuallyNotePath))
            {

                using (StreamReader sr = new StreamReader(ActuallyNotePath))
                {
                    textFromFile = sr.ReadToEnd();
                    sr.Dispose();
                }
                TextBox1.Text = textFromFile;
                OnChangeActuallyNotePath();
                UpdateWidgetInformationOfFile(this);
            }
        }

        private void OnChangeActuallyNotePath()
        {
            ActuallyFileLabel.Content = Path.GetFileName(ActuallyNotePath);
        }

        private void ShowControlsPanel()
        {
            this.ButtonsPanel.Visibility = Visibility.Visible;
            Thickness margin2 = TextBox1Grid.Margin;
            margin2.Top = 35;
            TextBox1Grid.Margin = margin2;
        }

        private void HideControlsPanel()
        {
            this.ButtonsPanel.Visibility = Visibility.Collapsed;
            Thickness margin1 = TextBox1Grid.Margin;
            margin1.Top = 0;
            TextBox1Grid.Margin = margin1;
        }

        #region StaticMethods

        /// <summary>
        /// Read and return save informaions about NoteWidget
        /// </summary>
        /// <returns>Informations about Save of NoteWidget</returns>
        public new static NoteInformation DownloadWidgetInformationOfFile()
        {
            if (File.Exists(NoteWidget.NoteSettingsPath))
            {
                NoteInformation noteInformation = new NoteInformation();

                using (StreamReader sr = File.OpenText(NoteWidget.NoteSettingsPath))
                {
                    noteInformation = (NoteInformation)GetWidgetInformationOfFile(noteInformation, sr);
                    if (noteInformation == null)
                    {
                        return null;
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

                    if (true)
                    {
                        noteInformation.ActuallyNotePath = sr.ReadLine();
                    }

                    if (true)
                    {
                        string a = sr.ReadLine();
                        if (bool.TryParse(a, out bool aBool))
                        {
                            noteInformation.VisibilityControlPanel = aBool;
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
        public new static NoteWidget CreateWidget()
        {
            NoteInformation noteInf = DownloadWidgetInformationOfFile();

            if (noteInf != null)
            {
                NoteWidget nf = new NoteWidget()
                {
                    Left = noteInf.PositionX,
                    Top = noteInf.PositionY,
                    ActuallyNotePath = noteInf.ActuallyNotePath
                };
                nf.Width = noteInf.Width;
                nf.Height = noteInf.Heigth;

                nf.rec1.Opacity = noteInf.BackgroundOpacity;

                nf.rec1.Fill = new System.Windows.Media.SolidColorBrush(noteInf.BackgroundColor);

                nf.TextBox1.Opacity = noteInf.FontOpacity;

                nf.TextBox1.Foreground = new System.Windows.Media.SolidColorBrush(noteInf.FontColor);

                nf.TextBox1.FontFamily = noteInf.FontFamily;

                nf.TextBox1.FontSize = noteInf.FontSize;

                nf.SelectAnimation(noteInf);

                switch (noteInf.VisibilityControlPanel)
                {
                    case true:
                        nf.ShowControlsPanel();
                        break;
                    case false:
                        nf.HideControlsPanel();
                        break;
                }

                return nf;
            }
            else
            { return new NoteWidget(); }
        }

        /// <summary>
        /// Write in file selected settings about NoteWidget
        /// </summary>
        /// <param name="argNW">NoteWidget to saving</param>
        public static void UpdateWidgetInformationOfFile(NoteWidget argNW)
        {
            MainWindow.CheckProgramDiresArchitecture();

            if (argNW != null && argNW.IsVisible)
            {
                #region CreateFileOfSettings NowSettings

                using (StreamWriter sw = File.CreateText(NoteWidget.NoteSettingsPath))
                {
                    UpdateWidgetInformationOfFileSaveObject(argNW, sw);

                    sw.WriteLine(argNW.rec1.Opacity);
                    sw.WriteLine(new System.Windows.Media.ColorConverter().ConvertToString(MainWindow.BrushToColorMedia(argNW.rec1.Fill)));

                    sw.WriteLine(argNW.TextBox1.Opacity.ToString());

                    sw.WriteLine(new System.Windows.Media.ColorConverter().ConvertToString(MainWindow.BrushToColorMedia(argNW.TextBox1.Foreground)));

                    sw.WriteLine(argNW.TextBox1.FontFamily.ToString());

                    sw.WriteLine(argNW.TextBox1.FontSize.ToString());

                    sw.WriteLine(argNW.ActuallyNotePath);

                    if (argNW.ButtonsPanel.Visibility == Visibility.Visible)
                    {
                        sw.WriteLine(true.ToString());
                    }
                    else
                    {
                        sw.WriteLine(false.ToString());
                    }

                    sw.Close();
                }

                #endregion
            }
            else
            {
                NoteInformation nI = DownloadWidgetInformationOfFile();
                if (File.Exists(NoteWidget.NoteSettingsPath) && nI != null)
                {
                    #region OverwriteFile
                    
                    NoteInformation noteInf = DownloadWidgetInformationOfFile();

                    using (StreamWriter sw = File.CreateText(NoteWidget.NoteSettingsPath))
                    {
                        UpdateWidgetInformationOfFileSaveWidgetInformation(noteInf, sw);

                        sw.WriteLine(noteInf.BackgroundOpacity.ToString());
                        sw.WriteLine(noteInf.BackgroundColor.ToString());

                        sw.WriteLine(noteInf.FontOpacity.ToString());
                        sw.WriteLine(noteInf.FontColor.ToString());
                        sw.WriteLine(new System.Windows.Media.FontFamilyConverter().ConvertToString(noteInf.FontFamily));
                        sw.WriteLine(noteInf.FontSize.ToString());
                        sw.WriteLine(noteInf.ActuallyNotePath);
                        sw.WriteLine(noteInf.VisibilityControlPanel.ToString());

                        sw.Close();
                    }

                    #endregion

                }
                else
                {
                    #region CreateFileOfSettings DeffaultSettings

                    using (StreamWriter sw = File.CreateText(NoteWidget.NoteSettingsPath))
                    {
                        sw.WriteLine(true.ToString());

                        sw.WriteLine("104");
                        sw.WriteLine("104");

                        sw.WriteLine("302");
                        sw.WriteLine("330");

                        sw.WriteLine(Animations.NULL.ToString());

                        sw.WriteLine((0.5).ToString());
                        sw.WriteLine("#FFF3E126");

                        sw.WriteLine((0.75).ToString());
                        sw.WriteLine("#FF000000");
                        sw.WriteLine("Century Gothic");
                        sw.WriteLine("20");
                        sw.WriteLine("");
                        sw.WriteLine(false.ToString());

                        sw.Close();
                    }

                    #endregion
                }
            }

        }

        #endregion

        #region Events

        private void TextBox1_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (this.IsActive == true)
            {
                if (!File.Exists(NotePath[NotePathIndex]))
                {
                    using (StreamWriter sw = File.CreateText(NotePath[0] = Path.Combine(NoteWidget.NotesDirePath, "Note_1.txt")))
                    {
                        sw.Write(TextBox1.Text);
                        sw.Dispose();
                    }
                }
                else
                {
                    using (StreamWriter sw = new StreamWriter(NotePath[NotePathIndex]))
                    {
                        sw.Write(TextBox1.Text);
                        sw.Dispose();
                    }
                }
            }
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string s = "";
                int ci = TextBox1.CaretIndex;
                if (TextBox1.Text.Length != 0)
                {
                    for (int i = 0; i < ci; i++)
                    {
                        s += TextBox1.Text[i];
                    }
                    s += "\n";
                    for (int i = ci; i < TextBox1.Text.Length; i++)
                    {
                        s += TextBox1.Text[i];
                    }
                }
                else
                {
                    s += "\n";
                }
                TextBox1.Text = s;
                TextBox1.CaretIndex = ci + 1;
            }
        }

        private void NoteWidget_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateWidgetInformationOfFile(this);
        }

        private void NoteWidget_LocationChanged(object sender, EventArgs e)
        {
            UpdateWidgetInformationOfFile(this);
        }

        private void BackFileButton_Click(object sender, RoutedEventArgs e)
        {
            if (NotePathIndex > 0)
            {
                NotePathIndex--;
                ActuallyNotePath = NotePath[NotePathIndex];
                LoadActuallyText();
            }
            NoteWidget.UpdateWidgetInformationOfFile(this);
        }

        private void NextFileButton_Click(object sender, RoutedEventArgs e)
        {
            if (NotePathIndex + 1 < NotePath.Count)
            {
                NotePathIndex++;
                ActuallyNotePath = NotePath[NotePathIndex];
                LoadActuallyText();
            }
            NoteWidget.UpdateWidgetInformationOfFile(this);
        }

        private void AddNewFileButton_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new System.Windows.Forms.SaveFileDialog()
            {
                InitialDirectory = NoteWidget.NotesDirePath,
                Filter = "*.txt|*.txt"
            };
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (File.CreateText(fileDialog.FileName)) { }
                this.NotePath.Add(fileDialog.FileName);
                NotePathIndex = NotePath.Count - 1;
                ActuallyNotePath = NotePath[NotePathIndex];
                OnChangeActuallyNotePath();
                LoadActuallyText();
            }
            //todo udostępnić możliwość kasowania notatek
            NoteWidget.UpdateWidgetInformationOfFile(this);
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

        private void ControlPanelViewChangeButton_Click(object sender, RoutedEventArgs e)
        {
            switch (this.ButtonsPanel.Visibility)
            {
                case Visibility.Visible:
                    HideControlsPanel();
                    break;
                case Visibility.Collapsed:
                default:
                    ShowControlsPanel();
                    break;
            }
            NoteWidget.UpdateWidgetInformationOfFile(this);
        }
    }
}
