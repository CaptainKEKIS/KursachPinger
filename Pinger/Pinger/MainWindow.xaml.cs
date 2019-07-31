using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Xaml;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;

namespace Pinger
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Ip> SavedIps = new List<Ip>();
        public MainWindow()
        {
            InitializeComponent();
            savedDGPingResult.ItemsSource = SavedIps;
        }
        void MyTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]").IsMatch(e.Text);//TODO: запилить шаблон если реально
        }


        #region Range, Single
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            IPAddress FirstIp;
            IPAddress LastIp;
            
            try
            {
                FirstIp = IPAddress.Parse(firstTB.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Неправильно задан начальный IP-адрес диапазона", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                LastIp = IPAddress.Parse(lastTB.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Неправильно задан финальный IP-адрес диапазона", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
           
            //TODO:SDELAT PROVERKU DIAPAZONA FIRSTIP < LASTIP

            List<Ip> IPs = Ip.Range(FirstIp, LastIp);
            dgPingResult.ItemsSource = IPs;
            RangePing(IPs);
            
        }

        private void RangePing(List<Ip> IPs)
        {
            PingStatistics Statistics = new PingStatistics();
            foreach (Ip ip in IPs)
            {
                Statistics.Sent++;
                Task.Run(() => ip.PingSender());
            }
        }

        private void SingleIPButton_Click(object sender, RoutedEventArgs e)
        {
            IPAddress IPAddress;

            try
            {
                IPAddress = IPAddress.Parse(IpAddr.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Неправильно задан  IP-адрес", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Ip Addr = new Ip();
            Addr.Address = IPAddress;
            List<Ip> IPs = new List<Ip>();
            IPs.Add(Addr);
            dgPingTable.ItemsSource = IPs;
            IPs[0].PingSender();
        }
        #endregion
       
        #region Saved
        private void SavedButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddHost_Click(object sender, RoutedEventArgs e) //не обновляется таблица
        {
            Ip ip = new Ip();
            ip.Address = IPAddress.Parse("172.16.18.10");
            ip.HostName = "Host";
            SavedIps.Add(ip);
        }

        private void DeleteHost_Click(object sender, RoutedEventArgs e)
        {

        }


        #endregion
        private void jhghj(object sender, KeyEventArgs e)
        {
            if ((sender as TextBox).Text.Length >= 3)
            {
                lastTB.Focus();
            }
        }

        private void MenuSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.Owner = this;
            settingsWindow.Show();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
