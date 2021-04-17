using System.IO;
using System.Windows;
using System.Windows.Input;

namespace GameAssistant
{
    /// <summary>
    /// Logika interakcji dla klasy NoteForm.xaml
    /// </summary>
    public partial class NoteForm : Window
    {
        public bool IsAllowDrag = false;

        public string[] NotePath = new string[] { Path.Combine(MainWindow.NotesDirePath, "Note_1.txt") };

        public NoteForm()
        {
            InitializeComponent();
            if (!File.Exists(NotePath[0]))
            {
                using (StreamWriter sw = File.CreateText(NotePath[0] = Path.Combine(MainWindow.NotesDirePath, "Note_1.txt")))
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

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragWindow(e);
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragWindow(e);
        }

        private void Rec1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragWindow(e);
        }

        private void DragWindow(MouseButtonEventArgs e)
        {
            if (IsAllowDrag && e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void TextBox1_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (this.IsActive == true)
            {
                if (!File.Exists(NotePath[0]))
                {
                    using (StreamWriter sw = File.CreateText(NotePath[0] = Path.Combine(MainWindow.NotesDirePath, "Note_1.txt")))
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
    }
}
