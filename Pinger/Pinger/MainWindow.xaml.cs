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
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);//TODO: запилить шаблон если реально
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

            List<Ip> IPs = Ip.Range(FirstIp, LastIp);
            dgPingResult.ItemsSource = IPs;

            foreach(Ip ip in IPs)
            {
                Task.Run(() => ip.PingSender());
            }
        }
    }
}
