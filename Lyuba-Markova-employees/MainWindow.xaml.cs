using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace Lyuba_Markova_employees
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<CoWorkers> obsCoWorkers = new ObservableCollection<CoWorkers>();
        public MainWindow()
        {
            InitializeComponent();

            LongestWorkedTogether.ItemsSource = obsCoWorkers;
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            obsCoWorkers.Clear();

            if (openFileDialog.ShowDialog() == true)
            {
                List<EmpProj> values = File.ReadAllLines(path: openFileDialog.FileName)
                                              .Select(v => EmpProj.FromCsv(v, ","))
                                              .ToList();
                CoWorkerFinder coWorkerFinder = new CoWorkerFinder();
                List<CoWorkers> coWorkers = coWorkerFinder.FindCoWorkers(values);

                for (int i = 0; i < coWorkers.Count; i++)
                {
                    obsCoWorkers.Add(coWorkers[i]);
                }
            }
        }
    }
}
