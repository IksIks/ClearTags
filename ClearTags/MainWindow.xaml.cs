using System.Text;
using System.Windows;
using WK.Libraries.SharpClipboardNS;
using static WK.Libraries.SharpClipboardNS.SharpClipboard;

namespace ClearTags
{
    public partial class MainWindow : Window
    {
        private readonly char[] separator = new[] { '-', '(', ')', ' ', ',', ':', ';', '!', '<', '>', '.', '/', '\\', '?', '#', '$', '$', '%',
                                                   '^', '*', '+', '`'};

        public MainWindow()
        {
            InitializeComponent();
            var clipboard = new SharpClipboard();
            Clipboard.Clear();
            clipboard.ClipboardChanged += ClipboardChanged;
        }

        private void ClipboardChanged(Object sender, ClipboardChangedEventArgs e)
        {
            if (e.ContentType == SharpClipboard.ContentTypes.Text)
            {
                string text = InputTeg.Text = e.Content.ToString();
                if (e.Content.ToString().Contains("@"))
                {
                    StringBuilder tegs = new StringBuilder();
                    var textSplit = e.Content.ToString().Split(separator);
                    foreach (var st in textSplit)
                    {
                        if (st.Contains("@"))
                        {
                            tegs.Append(st + " ");
                        }
                    }
                    OutputTeg.Text = tegs.ToString();
                    Clipboard.SetText(OutputTeg.Text);
                    InputTeg.Text = text;
                }
                else OutputTeg.Text = text;
            }
        }

        private void HideProgramm(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void ShowProgramm(object sender, RoutedEventArgs e)
        {
            this.Show();
        }

        private void ShowInfo(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Программа убирает ненужные ссылки в Никнеймах. " +
                            "Будьте внимательны, при копировании Email адресов " +
                            "программу лучше выключать, во избежание неправильной вставки", "Сreated by IKS");
        }
    }
}