using System.Windows;
using System.Windows.Input;

namespace GameAssistant
{
    public class WidgetWindow : Window
    {
        // Varibles
        public bool IsAllowDrag { get; set; } = false;

        public WidgetWindow()
        {
            this.Topmost = true;
            this.ShowInTaskbar = false;
            this.WindowStyle = WindowStyle.None;
        }

        // Methods
        public static dynamic DownloadWidgetInformationOfFile() { return 0; }

        public static dynamic CreateWidget() { return 0; }
        /*/ TODO public static void UpdateWidgetInformationOfFile(w) { } (Pomyśleć o interfejsach) /*/

        protected void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if (IsAllowDrag && e.LeftButton == MouseButtonState.Pressed)
                this?.DragMove();
        }

    }
}
