using System.ComponentModel;

namespace NetworkScanner.Models
{
    public class NetworkDevice : INotifyPropertyChanged
    {
        private string _ipAddress;
        private string _hostname;
        private string _status;
        private string _macAddress;

        public string IPAddress
        {
            get => _ipAddress;
            set
            {
                _ipAddress = value;
                OnPropertyChanged(nameof(IPAddress));
            }
        }

        public string Hostname
        {
            get => _hostname;
            set
            {
                _hostname = value;
                OnPropertyChanged(nameof(Hostname));
            }
        }

        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        public string MacAddress
        {
            get => _macAddress;
            set
            {
                _macAddress = value;
                OnPropertyChanged(nameof(MacAddress));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}