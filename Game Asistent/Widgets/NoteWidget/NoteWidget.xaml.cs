using System;
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
        public static string NotesDirePath;
        public static string NoteSettingsPath;

        public string[] NotePath = new string[] { Path.Combine(NotesDirePath, "Note_1.txt") };


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

                        sw.Close();
                    }

                    #endregion
                }
            }

        }
        //todo !!! dynamic nie jest deklaratywna w przypadku stałego typu !!!

        // ---------------------------------------------------------------- //


        public NoteWidget()
        {
            InitializeComponent();
            if (!File.Exists(NotePath[0]))
            {
                using (StreamWriter sw = File.CreateText(NotePath[0] = Path.Combine(NoteWidget.NotesDirePath, "Note_1.txt")))
                {
                    sw.WriteLine("Tutaj wpisz swoją notatkę...");
                    sw.Dispose();
                }
            }

            string textFromFile;
            using (StreamReader sr = new StreamReader(NotePath[0]))
            {
                textFromFile = sr.ReadToEnd();
                sr.Dispose();
            }
            TextBox1.Text = textFromFile;
        }

        private void TextBox1_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (this.IsActive == true)
            {
                if (!File.Exists(NotePath[0]))
                {
                    using (StreamWriter sw = File.CreateText(NotePath[0] = Path.Combine(NoteWidget.NotesDirePath, "Note_1.txt")))
                    {
                        sw.Write(TextBox1.Text);
                        sw.Dispose();
                    }
                }
                else
                {
                    using (StreamWriter sw = new StreamWriter(NotePath[0]))
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

    }
}
