using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ManagerWPF
{
    public class MainVM : INotifyPropertyChanged
    {
        private string _exception;

        public MainVM()
        {
            AddMetricCommand = new Command(AddMetrics);
        }
        
        public string Exception { get => _exception; set => Set(ref _exception, value); } 
        public MetricsCardVM Cpu { get; set; } = new MetricsCardVM();

        public ICommand AddMetricCommand { get; }
        
        private async void AddMetrics()
        {
            try
            {
                var client = new HttpClient();
                var fromTime = Cpu.Last;
                var toTime = DateTimeOffset.UtcNow;
                var response = await client.GetAsync($"http://localhost:5000/api/metrics/cpu/cluster/from/{fromTime:O}/to/{toTime:O}");
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    var cluster = await JsonSerializer.DeserializeAsync<CpuGetMetricsFromAllClusterResponse>(stream, new JsonSerializerOptions(JsonSerializerDefaults.Web));
                    var metrics = cluster.Metrics.ToArray();
                    foreach (var metric in metrics)
                    {
                        Cpu.AddMetric(metric.Value);
                    }

                    Cpu.Last = metrics[^1].Time;
                }
            }
            catch (Exception e)
            {
                Exception = e.Message;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Set<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if(EqualityComparer<T>.Default.Equals(field,value)) return;
            field = value;
            OnPropertyChanged(propertyName);
        }
    }
    public class CpuGetMetricsFromAllClusterResponse
    {
        public IEnumerable<CpuMetricResponse> Metrics { get; init; }
    }

    public class CpuMetricResponse
    {
        public int Id { get; init; }
        public int AgentId { get; init; }
        public DateTimeOffset Time { get; init; }
        public int Value { get; init; }
    }
}