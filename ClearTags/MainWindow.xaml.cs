using System.Text;
using System.Windows;
using WK.Libraries.SharpClipboardNS;
using static WK.Libraries.SharpClipboardNS.SharpClipboard;

namespace ClearTags
{
    public partial class MainWindow : Window
    {
        private readonly char[] separator = ['-', '(', ')', ' ', ',', ':', ';', '!', '<', '>', '.', '/', '\\', '?', '#', '$', '$', '%', '^', '*', '+', '`'];

        private const int maxLengthOutputString = 300;
        private readonly SharpClipboard clipboard;

        public MainWindow()
        {
            InitializeComponent();
            clipboard = new SharpClipboard();
            Clipboard.Clear();
            clipboard.ClipboardChanged += ClipboardChanged;
        }

        private void ClipboardChanged(Object sender, ClipboardChangedEventArgs e)
        {
            if (e.ContentType == SharpClipboard.ContentTypes.Text)
            {
                string? text = InputTeg.Text = e.Content.ToString();
                if (text.IndexOf('@') >= 0)
                {
                    StringBuilder tegs = new StringBuilder();
                    var textSplit = text.Split(separator).Where(str => str.Contains('@'));
                    foreach (var item in textSplit)
                    {
                        tegs.Append(item + " ");
                    }
                    OutputTeg.Text = tegs.ToString();
                    Clipboard.SetText(OutputTeg.Text);
                    InputTeg.Text = text;
                }
                else OutputTeg.Text = (text.Length < maxLengthOutputString) ? text : "Скопированный текст очень большой";
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
            MessageBox.Show("Программа убирает ненужные ссылки в Никнеймах. \n" +
                            "Будьте внимательны, при копировании Email адресов \n" +
                            "программу лучше выключать, во избежание \nнеправильной вставки"
                            , "Сreated by IKS");
        }
    }
}