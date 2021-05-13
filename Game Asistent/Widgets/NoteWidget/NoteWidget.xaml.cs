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

                nf.Rec1.Opacity = noteInf.BackgroundOpacity;

                nf.Rec1.Fill = new System.Windows.Media.SolidColorBrush(noteInf.BackgroundColor);

                nf.TextBox1.Opacity = noteInf.FontOpacity;

                nf.TextBox1.Foreground = new System.Windows.Media.SolidColorBrush(noteInf.FontColor);

                nf.TextBox1.FontFamily = noteInf.FontFamily;

                nf.TextBox1.FontSize = noteInf.FontSize;

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
                    sw.WriteLine(true.ToString());

                    sw.WriteLine(argNW.Left.ToString());
                    sw.WriteLine(argNW.Top.ToString());

                    sw.WriteLine(argNW.Width.ToString());
                    sw.WriteLine(argNW.Height.ToString());

                    sw.WriteLine(argNW.Rec1.Opacity);
                    sw.WriteLine(new System.Windows.Media.ColorConverter().ConvertToString(MainWindow.BrushToColorMedia(argNW.Rec1.Fill)));

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
                    #region DownloadInformationsOfFile

                    NoteInformation noteInf = DownloadWidgetInformationOfFile();
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
                    string _actuallyNotePath = noteInf.ActuallyNotePath;
                    bool _visibilityControlPanel = noteInf.VisibilityControlPanel;

                    #endregion

                    #region OverwriteFile

                    using (StreamWriter sw = File.CreateText(NoteWidget.NoteSettingsPath))
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
                        sw.WriteLine(_actuallyNotePath);
                        sw.WriteLine(_visibilityControlPanel);

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
