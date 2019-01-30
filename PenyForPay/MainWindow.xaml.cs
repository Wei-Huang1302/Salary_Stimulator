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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PenyForPay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SalaryVM svm = new SalaryVM();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = svm;
        }
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            svm.ResetAll();
        }
        private void HistoryEstimate_Click(object sender, RoutedEventArgs e)
        {
            svm.OpenEstimateFile();
        }
    }
}
