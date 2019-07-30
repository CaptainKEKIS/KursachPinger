using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Pinger
{
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            TBTimeOut.Text = Properties.Settings.Default.TimeOut.ToString();
            TBTtl.Text = Properties.Settings.Default.Ttl.ToString();
            TBDataSize.Text = Properties.Settings.Default.DataSize.ToString();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Проверка таймаута
                if (Convert.ToInt32(TBTimeOut.Text) <= 0)
                {
                    MessageBox.Show("Таймаут не может быть меньше или равным нулю!", "Ahtung!", MessageBoxButton.OK, MessageBoxImage.Error);
                    TBTimeOut.Text = Properties.Settings.Default.TimeOut.ToString();
                }
                else if (Convert.ToInt32(TBTimeOut.Text) > 60000)
                {
                    MessageBox.Show("This is madness!!!", "Ahtung!", MessageBoxButton.OK, MessageBoxImage.Error);
                    TBTimeOut.Text = Properties.Settings.Default.TimeOut.ToString();
                }
                else
                {
                    Properties.Settings.Default.TimeOut = Convert.ToInt32(TBTimeOut.Text);
                }
                //Проверка TTL
                if (Convert.ToInt32(TBTtl.Text) <= 0)
                {
                    MessageBox.Show("TTL не может быть меньше или равным нулю!", "Ahtung!", MessageBoxButton.OK, MessageBoxImage.Error);
                    TBTtl.Text = Properties.Settings.Default.Ttl.ToString();
                }
                else if (Convert.ToInt32(TBTtl.Text) > 255)
                {
                    MessageBox.Show("TTL не может быть больше 255!", "Ahtung!", MessageBoxButton.OK, MessageBoxImage.Error);
                    TBTtl.Text = Properties.Settings.Default.Ttl.ToString();
                }
                else
                {
                    Properties.Settings.Default.Ttl = Convert.ToInt32(TBTtl.Text);
                }
                //Проверка DataSize
                if (Convert.ToInt32(TBDataSize.Text) <= 0)
                {
                    MessageBox.Show("Data Size не может быть меньше или равным нулю!", "Ahtung!", MessageBoxButton.OK, MessageBoxImage.Error);
                    TBDataSize.Text = Properties.Settings.Default.DataSize.ToString();
                }
                else if (Convert.ToInt32(TBDataSize.Text) > 4096)
                {
                    MessageBox.Show("This is madness!!!", "Ahtung!", MessageBoxButton.OK, MessageBoxImage.Error);
                    TBDataSize.Text = Properties.Settings.Default.DataSize.ToString();
                }
                else
                {
                    Properties.Settings.Default.DataSize = Convert.ToInt32(TBDataSize.Text);
                }
                //Сохранение
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                TBTimeOut.Text = Properties.Settings.Default.TimeOut.ToString();
                TBTtl.Text = Properties.Settings.Default.Ttl.ToString();
                TBDataSize.Text = Properties.Settings.Default.DataSize.ToString();
            }
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
