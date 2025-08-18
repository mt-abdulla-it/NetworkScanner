using NetworkScanner.Models;
using NetworkScanner.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace NetworkScanner.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _baseIp;
        private string _status;
        private bool _isScanning;
        private string _scanButtonText = "Start Scan";
        private string _selectedIpRange = "192.168.1.";

        public ObservableCollection<NetworkDevice> Devices { get; } = new ObservableCollection<NetworkDevice>();
        public ICollectionView DevicesView { get; }

        public ICommand ScanCommand { get; }
        public ICommand ClearCommand { get; }

        public MainViewModel()
        {
            DevicesView = CollectionViewSource.GetDefaultView(Devices);
            ScanCommand = new RelayCommand(async _ => await ScanNetworkAsync());
            ClearCommand = new RelayCommand(_ => ClearResults());

            // Common IP ranges for quick selection
            IpRanges = new ObservableCollection<string>
            {
                "192.168.0.",
                "192.168.1.",
                "10.0.0.",
                "172.16.0."
            };
        }

        public string BaseIp
        {
            get => _baseIp;
            set
            {
                _baseIp = value;
                OnPropertyChanged(nameof(BaseIp));
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

        public bool IsScanning
        {
            get => _isScanning;
            set
            {
                _isScanning = value;
                OnPropertyChanged(nameof(IsScanning));
                ScanButtonText = _isScanning ? "Scanning..." : "Start Scan";
            }
        }

        public string ScanButtonText
        {
            get => _scanButtonText;
            set
            {
                _scanButtonText = value;
                OnPropertyChanged(nameof(ScanButtonText));
            }
        }

        public string SelectedIpRange
        {
            get => _selectedIpRange;
            set
            {
                _selectedIpRange = value;
                BaseIp = _selectedIpRange;
                OnPropertyChanged(nameof(SelectedIpRange));
            }
        }

        public ObservableCollection<string> IpRanges { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task ScanNetworkAsync()
        {
            if (IsScanning) return;

            IsScanning = true;
            Status = "Scanning network...";

            try
            {
                Devices.Clear();
                string baseIp = BaseIp;

                if (string.IsNullOrWhiteSpace(baseIp))
                {
                    Status = "Please enter a valid IP range (e.g., 192.168.1.)";
                    IsScanning = false;
                    return;
                }

                var tasks = new Task[255];
                for (int i = 1; i <= 255; i++)
                {
                    string ip = baseIp + i;
                    tasks[i - 1] = Task.Run(() => CheckDevice(ip));
                }

                await Task.WhenAll(tasks);
                Status = $"Scan completed. Found {Devices.Count} devices.";
            }
            catch (Exception ex)
            {
                Status = $"Error: {ex.Message}";
            }
            finally
            {
                IsScanning = false;
            }
        }

        private void CheckDevice(string ipAddress)
        {
            try
            {
                using (var ping = new Ping())
                {
                    var reply = ping.Send(ipAddress, 250);
                    if (reply?.Status == IPStatus.Success)
                    {
                        string hostname = GetHostName(ipAddress);
                        string macAddress = GetMacAddress(ipAddress);

                        App.Current.Dispatcher.Invoke(() =>
                        {
                            Devices.Add(new NetworkDevice
                            {
                                IPAddress = ipAddress,
                                Hostname = hostname,
                                Status = "Online",
                                MacAddress = macAddress
                            });
                        });
                    }
                }
            }
            catch { }
        }

        private string GetHostName(string ipAddress)
        {
            try
            {
                var hostEntry = Dns.GetHostEntry(ipAddress);
                return hostEntry.HostName;
            }
            catch
            {
                return "Unknown";
            }
        }

        private string GetMacAddress(string ipAddress)
        {
            try
            {
                // This is a simplified approach - in a real app you'd need proper ARP table lookup
                return "00:00:00:00:00:00"; // Placeholder
            }
            catch
            {
                return "Unknown";
            }
        }

        private void ClearResults()
        {
            Devices.Clear();
            Status = "Ready";
        }
    }
}