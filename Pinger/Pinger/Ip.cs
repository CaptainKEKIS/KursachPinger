﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Net.NetworkInformation;
using System.Net;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Pinger
{
    class Ip : INotifyPropertyChanged
    {
        AutoResetEvent resetEvent = new AutoResetEvent(false);
        PingReply Reply;
        private IPAddress _address;
        private string _hostName;
        private string _status;
        private string _delay;

        public Ip()
        {
            _delay = "";
        }

        public IPAddress Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
                OnPropertyChanged();
            }
        }

        public string Delay
        {
            get
            {
                return _delay;
            }
            set
            {
                _delay = value;
                OnPropertyChanged("Delay");
            }
        }

        public string HostName
        {
            get
            {
                return _hostName;
            }
            set
            {
                _hostName = value;
                OnPropertyChanged("HostName");
            }
        }

        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                OnPropertyChanged("Status");
            }
        }

        public static List<Ip> Range(IPAddress FirstIPAddress, IPAddress LastIPAddress)
        {
            byte[] firstIPAddressAsBytesArray = FirstIPAddress.GetAddressBytes();

            byte[] lastIPAddressAsBytesArray = LastIPAddress.GetAddressBytes();

            Array.Reverse(firstIPAddressAsBytesArray);

            Array.Reverse(lastIPAddressAsBytesArray);

            Int32 firstIPAddressAsInt = BitConverter.ToInt32(firstIPAddressAsBytesArray, 0);

            Int32 lastIPAddressAsInt = BitConverter.ToInt32(lastIPAddressAsBytesArray, 0);

            List<Ip> IpRange = new List<Ip>();

            for (var i = firstIPAddressAsInt; i <= lastIPAddressAsInt; i++)
            {
                byte[] bytes = BitConverter.GetBytes(i);
                IPAddress newIp = new IPAddress(new[] { bytes[3], bytes[2], bytes[1], bytes[0] });
                Ip Addr = new Ip();
                Addr.Address = newIp;
                IpRange.Add(Addr);
            }

            return IpRange;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }


        public void PingSender()
        {

            int TimeOut = Properties.Settings.Default.TimeOut;
            int Ttl = Properties.Settings.Default.Ttl;
            int DataSize = Properties.Settings.Default.DataSize;
            PingOptions POptions = new PingOptions(Ttl, false);
            byte[] PingBuffer = new byte[DataSize];
            Random rnd = new Random();
            rnd.NextBytes(PingBuffer);
            Ping Piping = new Ping();
            /*
            try
            {
                Reply = Piping.SendAsync(_address, TimeOut, PingBuffer, POptions);
            }
            catch (Exception)
            {
                MessageBox.Show("Fatal error!!!");
                return;
            }
            
            */
            Piping.PingCompleted += new PingCompletedEventHandler(PingComplete);
            Piping.SendAsync(_address, TimeOut, PingBuffer, POptions);
            
        }

        private void PingComplete(object sender, PingCompletedEventArgs e)
        {
            if (e.Cancelled){
                MessageBox.Show("Ping Canceled");
                ((AutoResetEvent)e.UserState).Set();
            }
            else if (e.Error != null){
                MessageBox.Show("Fatal error!!!" + e.Error);
                ((AutoResetEvent)e.UserState).Set();
            }
            else {
                Reply = e.Reply;
                PingResult(Reply);
            }
        }

        private void PingResult(PingReply Reply)
        {
            Status = Reply.Status.ToString();
            if (Reply.RoundtripTime.ToString() == "0" && Reply.Status.ToString() == "Success")
            {
                Delay = "<1";
            }
            else
            {
                Delay = Reply.RoundtripTime.ToString();
            }

            try//TODO: Починить отображение имени хоста
            {
                HostName = Dns.GetHostEntry(_address).HostName;
            }
            catch (Exception)
            {
                //_hostName = "Mission Failed!";
                HostName = _address.ToString();
            }
        }
    }
}
