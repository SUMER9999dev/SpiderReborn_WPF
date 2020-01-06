using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SpiderReborn_WPF
{
    public partial class ScriptHub : Window
    {
      

        public ScriptHub()
        {
            InitializeComponent();

        }

        private void Scriptwindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void ScriptHub_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void MinBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void RoseFarm_Click(object sender, RoutedEventArgs e)
        {
            using (WebClient wc = new WebClient())
            {
                string script = wc.DownloadString("https://pastebin.com/raw/1BJj0fB4");
                Functions.Execute(script);
            }
        }

        private void JailFarm_Click(object sender, RoutedEventArgs e)
        {
            using (WebClient wc = new WebClient())
            {
                string script = wc.DownloadString("https://pastebin.com/raw/FWWe6cXr");
                Functions.Execute(script);
            }
        }

        private void MadFarm_Click(object sender, RoutedEventArgs e)
        {
            using (WebClient wc = new WebClient())
            {
                string script = wc.DownloadString("https://pastebin.com/raw/LbDXghTe");
                Functions.Execute(script);
            }
        }

        private void FEkill_Click(object sender, RoutedEventArgs e)
        {
            using (WebClient wc = new WebClient())
            {
                string script = wc.DownloadString("https://pastebin.com/raw/GcC7mLdr");
                Functions.Execute(script);
            }
        }

        private void TopKek_Click(object sender, RoutedEventArgs e)
        {
            using (WebClient wc = new WebClient())
            {
                string script = wc.DownloadString("https://pastebin.com/raw/LgBc0pqx");
                Functions.Execute(script);
            }
        }
    }
}
