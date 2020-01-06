using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ICSharpCode.AvalonEdit.Highlighting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SpiderReborn_WPF.DiscordRPC;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using Microsoft.Win32;
using System.Security.Cryptography;
using System.Threading;
using System.Net;

namespace SpiderReborn_WPF
{
    public partial class MainWindow : Window
    {

        AES aes = new AES();

        public static string GenerateUserID() 
        {
            string guid = Guid.NewGuid().ToString(); 
            guid = guid.Replace("-", ""); 
            return guid; 
        }

        public static bool checkBlacklisted()
        {
            AES aes = new AES();
            string path = @".\Bin\Auth\userId.bin";
            string id;
            using(StreamReader sr = new StreamReader(path))
            {
                id = sr.ReadToEnd();
                sr.Close();
            }
            using(WebClient wc = new WebClient())
            {
               string BlacklistedId = wc.DownloadString("https://pastebin.com/raw/f9n9c82a");
                if (BlacklistedId.Contains(aes.Decrypt(id)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            string path = @".\Bin\Auth\userId.bin";
            if (File.Exists(path))
            {
                string text;
                using(StreamReader sr = new StreamReader(path))
                {
                    text = sr.ReadToEnd();
                    sr.Close();
                }
                if(text == "")
                {
                    using (StreamWriter sw = new StreamWriter(path))
                    {
                        sw.Write(aes.Encrypt(GenerateUserID()));
                        sw.Close();
                    }
                }
            }
            else
            {
                File.Create(path).Dispose();
                using(StreamWriter sw = new StreamWriter(path))
                {
                    sw.Write(aes.Encrypt(GenerateUserID()));
                    sw.Close();
                }
            }
        }

        private void ExBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            DiscordRPC.DiscordRPC.Shutdown();
            File.WriteAllText("./Bin/Auth/Text.bin", TextEditor.Text);
            System.Windows.Application.Current.Shutdown();
        }

        private void DragPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void DragPanel_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void DragPanel_MouseUp(object sender, MouseButtonEventArgs e)
        {
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
        }

        private void Execwindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void Execwindow_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void Execwindow_MouseUp(object sender, MouseButtonEventArgs e)
        {
        }

        private void NameLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }


        private Point start_point = new Point(0, 0);
        private DiscordRPC.DiscordRPC.EventHandlers handlers;
        private DiscordRPC.DiscordRPC.RichPresence presence;
        

        private void Execwindow_Loaded(object sender, RoutedEventArgs e)
        {
            if(checkBlacklisted() == true)
            {
                MessageBox.Show("Sorry you blacklisted!", "SpiderReborn", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0);
            }
            this.handlers = default(DiscordRPC.DiscordRPC.EventHandlers);
            DiscordRPC.DiscordRPC.Initialize("610846708271677440", ref this.handlers, true, null);
            this.handlers = default(DiscordRPC.DiscordRPC.EventHandlers);
            DiscordRPC.DiscordRPC.Initialize("610846708271677440", ref this.handlers, true, null);
            this.presence.details = "SpiderReborn [Full Lua Executor]";
            this.presence.state = "Made by HackerTa, Tony_Gamer, Gustas, SUMER, Chxse";
            this.presence.largeImageKey = "logo";
            this.presence.smallImageKey = "large";
            this.presence.largeImageText = "SpiderReborn";
            DiscordRPC.DiscordRPC.UpdatePresence(ref this.presence);
            DirectoryInfo di = new DirectoryInfo("./Bin/Scripts");
            FileInfo[] f = di.GetFiles("*.txt");
            foreach (FileInfo fi in f)
            {
                ScriptList.Items.Add(fi.Name);
            }
            DirectoryInfo din = new DirectoryInfo("./Bin/Scripts");
            FileInfo[] file = din.GetFiles("*.lua");
            foreach (FileInfo fil in file)
            {
                ScriptList.Items.Add(fil.Name);
            }
            using (StreamReader s = new StreamReader("./Bin/Languages/Lua.xml"))
            {
                using (XmlTextReader reader = new XmlTextReader(s))
                {
                    TextEditor.SyntaxHighlighting = ICSharpCode.AvalonEdit.Highlighting.Xshd.HighlightingLoader.Load(reader, HighlightingManager.Instance);
                }
            }
            string Text = File.ReadAllText("./Bin/Auth/Text.bin");
            if (Text == "")
            {
                TextEditor.Text = "----Thank you for using SpiderReborn!";
            }
            else
            {
                TextEditor.Text = Text;
            }
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            if (ScriptList.SelectedIndex != -1)
            {
                TextEditor.Text = File.ReadAllText($"./Bin/Scripts/{ScriptList.SelectedItem}");
            }
            else
            {
                MessageBox.Show("Select script please!", "SpiderReborn", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            ScriptList.Items.Clear();

            DirectoryInfo di = new DirectoryInfo("./Bin/Scripts");
            FileInfo[] f = di.GetFiles("*.txt");
            foreach (FileInfo fi in f)
            {
                ScriptList.Items.Add(fi.Name);
            }

            DirectoryInfo din = new DirectoryInfo("./Bin/Scripts");
            FileInfo[] file = din.GetFiles("*.lua");
            foreach (FileInfo fil in file)
            {
                ScriptList.Items.Add(fil.Name);
            }
        }


        private void ScriptHub_Click(object sender, RoutedEventArgs e)
        {
            ScriptHub sh = new ScriptHub();
            sh.Show();
            
        }

        private void ExecBtn_Click(object sender, RoutedEventArgs e)
        {
            Functions.Execute(TextEditor.Text);
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            TextEditor.Text = "";
        }

        private void OpenBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "SpiderReborn | Open script";
            openFileDialog.Filter = " Txt script (*.txt)|*.txt|Lua script (*.lua)|*.lua|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    TextEditor.Text = File.ReadAllText(openFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void InjectBtn_Click(object sender, RoutedEventArgs e)
        {
            Functions.Inject();
        }

        private void ExecuteL_Click(object sender, RoutedEventArgs e)
        {
            if(ScriptList.SelectedIndex != -1)
            {
                string script = File.ReadAllText($"./Bin/Scripts/{ScriptList.SelectedItem}");
                Functions.Execute(script);
            }
            else
            {
                MessageBox.Show("Select script please!", "SpiderReborn", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




    }
}
