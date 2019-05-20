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

        public MainWindow()
        {
            InitializeComponent();
        }

        void MyTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]").IsMatch(e.Text);//TODO: запилить шаблон если реально
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IPAddress FirstIp;
            IPAddress LastIp;
            
            try
            {
                FirstIp = IPAddress.Parse(FirstIpAddr.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Неправильно задан начальный IP-адрес диапазона", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                LastIp = IPAddress.Parse(LastIpAddr.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Неправильно задан финальный IP-адрес диапазона", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            /*
            byte[] firstIPAddressAsBytesArray = FirstIPAddress.GetAddressBytes();

            byte[] lastIPAddressAsBytesArray = LastIPAddress.GetAddressBytes();

            Array.Reverse(firstIPAddressAsBytesArray);

            Array.Reverse(lastIPAddressAsBytesArray);

            Int32 firstIPAddressAsInt = BitConverter.ToInt32(firstIPAddressAsBytesArray, 0);

            Int32 lastIPAddressAsInt = BitConverter.ToInt32(lastIPAddressAsBytesArray, 0);
            *///PEREMESTIT SUDA IZ CLASSA IP
            //TODO:SDELAT PROVERKU DIAPAZONA FIRSTIP < LASTIP

            List<Ip> IPs = Ip.Range(FirstIp, LastIp);
            dgPingResult.ItemsSource = IPs;

            foreach(Ip ip in IPs)
            {
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

            foreach (Ip ip in IPs)
            {
                Task.Run(() => ip.PingSender());
            }
        }

        private void SavedButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void jhghj(object sender, KeyEventArgs e)
        {
            if ((sender as TextBox).Text.Length >= 3)
            {
                LastIpAddr.Focus();
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
