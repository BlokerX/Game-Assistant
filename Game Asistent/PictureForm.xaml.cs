using System;
using System.Windows;

namespace GameAssistant
{
    /// <summary>
    /// Logika interakcji dla klasy PictureWidget.xaml
    /// </summary>
    public partial class PictureWidget : WidgetWindow
    {
        public string ImagePath = "Default";

        public PictureWidget()
        {
            InitializeComponent();
            rec1.Visibility = Visibility.Hidden;
        }

        private void PictureWidget_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            MainWindow.UpdatePictureInformationOfFile(this);
        }

        private void PictureWidget_LocationChanged(object sender, EventArgs e)
        {
            MainWindow.UpdatePictureInformationOfFile(this);
        }
    }
}
