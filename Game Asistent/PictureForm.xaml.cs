using System.Windows;

namespace GameAssistant
{
    /// <summary>
    /// Logika interakcji dla klasy PictureForm.xaml
    /// </summary>
    public partial class PictureWidget : WidgetWindow
    {
        public string ImagePath = "Default";

        public PictureWidget()
        {
            InitializeComponent();
            rec1.Visibility = Visibility.Hidden;
        }

    }
}
