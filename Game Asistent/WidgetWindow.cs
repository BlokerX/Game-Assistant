using System.Windows;
using System.Windows.Input;

namespace GameAssistant
{
    public class WidgetWindow : Window
    {
        public bool IsAllowDrag { get; set; } = false;

        protected void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if (IsAllowDrag && e.LeftButton == MouseButtonState.Pressed)
                this?.DragMove();
        }
        
    }
}
