using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Lyuba_Markova_employees
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string separator = ",";

        private ObservableCollection<CoWorkers> obsCoWorkers = new ObservableCollection<CoWorkers>();
        public MainWindow()
        {
            InitializeComponent();

            LongestWorkedTogether.ItemsSource = obsCoWorkers;
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Csv files (*.csv)|*.csv|All files (*.*)|*.*";

            obsCoWorkers.Clear();

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    List<EmpProj> values = File.ReadAllLines(path: openFileDialog.FileName)
                                                  .Select(v => EmpProj.FromCsv(v, separator))
                                                  .ToList();

                    if (values.Count > 0)
                    {
                        IEnumerable<CoWorkers> coWorkers = CoWorkerFinder.FindLongestWorkedCoWorkers(values);

                        foreach (CoWorkers worker in coWorkers)
                        {
                            obsCoWorkers.Add(worker);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Selected file doesn't contain records.", "Message", MessageBoxButton.OK);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK);
                }
            }
        }
    }
}
