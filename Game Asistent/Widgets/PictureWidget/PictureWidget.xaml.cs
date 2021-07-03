using System;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace GameAssistant
{
    /// <summary>
    /// Logika interakcji dla klasy PictureWidget.xaml
    /// </summary>
    public partial class PictureWidget : WidgetWindow
    {
        // Varibles:
        public static string PictureSettingsPath; // PATH //
        public string ImagePath = "Default";

        public PictureWidget()
        {
            InitializeComponent();
            rec1.Visibility = Visibility.Hidden;

            if (ImagePath == "Default")
            {
                ImageBox.Source = MainWindow.GetBitmapSource(Properties.Resources.DefaultImageToImageBox);
                ImagePath = "Default";
            }
        }

        #region StaticMethods

        /// <summary>
        /// Read and return save informaions about PictureWidget
        /// </summary>
        /// <returns>Informations about Save of PictureWidget</returns>
        public new static PictureInformation DownloadWidgetInformationOfFile()
        {
            if (File.Exists(PictureWidget.PictureSettingsPath))
            {
                PictureInformation pictureInformation = new PictureInformation();

                using (StreamReader sr = File.OpenText(PictureWidget.PictureSettingsPath))
                {
                    pictureInformation = (PictureInformation)GetWidgetInformationOfFile(pictureInformation, sr);
                    if (pictureInformation == null)
                    {
                        return null;
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
        public new static PictureWidget CreateWidget()
        {
            PictureInformation pictureInf = DownloadWidgetInformationOfFile();

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
                    pf.ImageBox.Source = MainWindow.GetBitmapSource(Properties.Resources.DefaultImageToImageBox);
                    pf.ImagePath = "Default";
                }
                else if (File.Exists(pictureInf.PicturePath))
                {
                    pf.ImageBox.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, pictureInf.PicturePath)));
                    pf.ImagePath = pictureInf.PicturePath;
                }
                else
                {
                    pf.ImageBox.Source = MainWindow.GetBitmapSource(Properties.Resources.DefaultImageToImageBox);
                    pf.ImagePath = "Default";
                }

                pf.SelectAnimation(pictureInf);
                if(pictureInf.Animation != Animations.NULL)
                {
                    if(pf.Grid1.Background is System.Windows.Media.SolidColorBrush scb)
                    {
                        Color cl = scb.Color;
                        cl.A = (byte)(pictureInf.PictureOpacity * 255);
                        pf.Grid1.Background = new SolidColorBrush(cl);
                    }
                }

                return pf;
            }
            else
            {
                return new PictureWidget();
            }
        }

        /// <summary>
        /// Write in file selected settings about PictureWidget
        /// </summary>
        /// <param name="argPW">PictureWidget to saving</param>
        public static void UpdateWidgetInformationOfFile(PictureWidget argPW)
        {
            MainWindow.CheckProgramDiresArchitecture();

            if (argPW != null && argPW.IsVisible)
            {
                #region CreateFileOfSettings NowSettings

                using (StreamWriter sw = File.CreateText(PictureWidget.PictureSettingsPath))
                {
                    UpdateWidgetInformationOfFileSaveObject(argPW, sw);

                    sw.WriteLine(argPW.ImageBox.Opacity.ToString());
                    sw.WriteLine(argPW.ImagePath.ToString());

                    sw.Close();
                }

                #endregion
            }
            else
            {
                PictureInformation pI = DownloadWidgetInformationOfFile();
                if (File.Exists(PictureWidget.PictureSettingsPath) && pI != null)
                {

                    #region OverwriteFile

                    PictureInformation pictureInf = DownloadWidgetInformationOfFile();

                    using (StreamWriter sw = File.CreateText(PictureWidget.PictureSettingsPath))
                    {
                        UpdateWidgetInformationOfFileSaveWidgetInformation(pictureInf, sw);

                        sw.WriteLine(pictureInf.PictureOpacity.ToString());
                        sw.WriteLine(pictureInf.PicturePath);

                        sw.Close();
                    }

                    #endregion

                }
                else
                {
                    #region CreateFileOfSettings DeffaultSettings

                    using (StreamWriter sw = File.CreateText(PictureWidget.PictureSettingsPath))
                    {
                        sw.WriteLine(true.ToString());

                        sw.WriteLine("100");
                        sw.WriteLine("100");

                        sw.WriteLine("733");
                        sw.WriteLine("413");

                        sw.WriteLine(Animations.NULL.ToString());

                        sw.WriteLine((1).ToString());
                        sw.WriteLine("Default");

                        sw.Close();
                    }

                    #endregion
                }
            }

        }

        #endregion

        #region Events

        private void PictureWidget_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateWidgetInformationOfFile(this);
        }

        private void PictureWidget_LocationChanged(object sender, EventArgs e)
        {
            UpdateWidgetInformationOfFile(this);
        }

        #endregion

        #region Animations

        protected override void Animation_ColorRainbowRGB_GoOn()
        {
            Grid1.Background = this.Animation_ColorRainbowRGB(Grid1.Background);
        }

        protected override void Animation_ColorRainbowRGB2_GoOn()
        {
            Grid1.Background = this.Animation_ColorRainbowRGB2(Grid1.Background);
        }

        #endregion
    }
}
