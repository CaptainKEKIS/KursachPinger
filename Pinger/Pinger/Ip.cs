﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Net;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Pinger
{
    class Ip : INotifyPropertyChanged
    {
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
                OnPropertyChanged("Address");
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
            int timeout = 9999;
            Ping Piping = new Ping();
            try
            {
                Reply = Piping.Send(_address, timeout);
                _status = Reply.Status.ToString();

                if (Reply.RoundtripTime.ToString() == "0" && Reply.Status.ToString() == "Success")
                {
                    _delay = "<1";
                }
                else
                {
                    _delay = Reply.RoundtripTime.ToString();
                }

                try
                {
                    _hostName = Dns.GetHostEntry(_address).HostName;
                }
                catch (Exception)
                {
                    //_hostName = "Mission Failed!";
                    _hostName = _address.ToString();
                }
            }
            catch (Exception)
            {
                _hostName = "Mission Failed!";
            }
        }
    }
}
