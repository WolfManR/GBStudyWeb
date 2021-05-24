using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ManagerWPF
{
    public class MainVM
    {
        public MainVM()
        {
            AddMetricCommand = new Command(AddMetric);
        }

        private static Random rand = new Random();
        public ICommand AddMetricCommand { get; }

        public MetricsCardVM Cpu { get; set; } = new MetricsCardVM();

        private void AddMetric()
        {
            for (int i = 0; i < 10; i++)
            {
                Cpu.AddMetric(rand.Next(0,300)); 
            }
        }
    }
}