using System.Windows;
using System.Windows.Controls;

namespace ManagerWPF
{
    public partial class MetricsCard : UserControl
    {
        public MetricsCard() => InitializeComponent();
        
        private void UpdateOnСlick(object sender, RoutedEventArgs e)
        {
            TimePowerChart.Update(true);
        }
    }
}
