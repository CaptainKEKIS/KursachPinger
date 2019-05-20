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
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.TimeOut = Convert.ToInt32(TBTimeOut.Text);
            Properties.Settings.Default.Save();
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
